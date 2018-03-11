using System;
using Heron.Utility;
using Heron.Data;

namespace Heron.Strategy
{
    class Inertance : StrategyModel
    {
        FloatingMonitor _rmLong;
        FloatingMonitor _rmShort;

        RecentQueue _rq;

        public Inertance(DataService source)
        {
            init(source);
        }

        public override void Feedback(PosStatus status)
        {
            feedbackInfo(status);
        }

        Direction tend(decimal range, decimal bar)
        {
            Direction x = _rq.CheckTend(_time.AddSeconds(-15), _time, range, 3m);
            Direction y = _rq.CheckTend(_time.AddSeconds(-30), _time.AddSeconds(-15), range, bar);
            Direction z = _rq.CheckTend(_time.AddSeconds(-45), _time.AddSeconds(-30), range, bar);

            if ((z == y) && (y == x) && (x == Direction.Long))
                return Direction.Long;
            if ((z == y) && (y == x) && (x == Direction.Short))
                return Direction.Short;

            return Direction.Null;
        }

        bool checkPos(Direction tend, decimal ratio)
        {
            decimal range = _basePrice * 0.003m * ratio;

            if (tend == Direction.Long)
                if (_price - _rq.GetLowest(_time.AddSeconds(-120), _time) > range)
                    return false;

            if (tend == Direction.Short)
                if (_rq.GetHighest(_time.AddSeconds(-120), _time) - _price > range)
                    return false;

            return true;
        }

        void analyse()
        {
            decimal range = _basePrice * 0.15m / 100;
            decimal ratio = _rq.GetTotalVol(_time.AddSeconds(-60), _time) / (_baseVol / 14400 * 60m);

            if (ratio > 3m)
            {
                if (_status.longLots == 0)
                    if (_price > _basePrice)
                        if (tend(range, 3m) == Direction.Long)
                            if (checkPos(Direction.Long, ratio))
                            {
                                appendSignal(Actions.OpenLong, 1);
                                _rmLong = new FloatingMonitor(Direction.Long, _price);
                            }

                if (_status.shortLots == 0)
                    if (_price < _basePrice)
                        if (tend(range, 3m) == Direction.Short)
                            if (checkPos(Direction.Short, ratio))
                            {
                                appendSignal(Actions.OpenShort, 1);
                                _rmShort = new FloatingMonitor(Direction.Short, _price);
                            }
            }
        }

        void holding()
        {
            decimal range = _basePrice * 0.1m / 100;

            if (_status.longLots > 0)
            {
                _rmLong.Update(_price);
                if (_rmLong.Warning(0.5m)
                    || (_rq.CheckTend(_time.AddSeconds(-45), _time, range, 1.5m) == Direction.Short))
                    appendSignal(Actions.CloseLong, 1);
            }

            if (_status.shortLots > 0)
            {
                _rmShort.Update(_price);
                if (_rmShort.Warning(0.5m)
                    || (_rq.CheckTend(_time.AddSeconds(-45), _time, range, 1.5m) == Direction.Long))
                    appendSignal(Actions.CloseShort, 1);
            }
        }

        public override Signal[] Update(DataNode item)
        {
            if (item.time.TimeOfDay < TimeSpan.FromMinutes(9 * 60 + 28))
                return new Signal[0];

            if (item.time.Date != _time.Date)
                _rq = new RecentQueue(120);

            updateData(item, true);

            _rq.Update(item);

            if ((_time.TimeOfDay > TimeSpan.FromMinutes(9 * 60 + 31)) &&
                (_time.TimeOfDay < TimeSpan.FromMinutes(14 * 60 + 56)))
                analyse();

            if (_time.TimeOfDay > TimeSpan.FromMinutes(14 * 60 + 56))
                clear();
            else
                holding();

            return getResult();
        }
    }
}
