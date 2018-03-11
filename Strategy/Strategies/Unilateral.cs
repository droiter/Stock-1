using System;
using System.Linq;
using Heron.Utility;
using Heron.Data;

namespace Heron.Strategy
{
    class Unilateral : StrategyModel
    {
        const decimal Bar = 0.5m / 100;

        FloatingMonitor _rmLong;
        FloatingMonitor _rmShort;

        bool _longLock;
        bool _shortLock;

        public Unilateral(DataService service)
        {
            init(service);
        }

        public override void Feedback(PosStatus status)
        {
            feedbackInfo(status);
        }

        public override Signal[] Update(DataNode item)
        {
            if (item.time.Date != _time.Date)
            {
                _longLock = false;
                _shortLock = false;
            }

            updateData(item, false);

            performLong();
            performShort();

            if (Common.DeliveryDay.Contains(_time.Date))
                if (_time.TimeOfDay > TimeSpan.FromMinutes(14 * 60 + 57))
                {
                    _longLock = true;
                    _shortLock = true;
                    clear();
                }

            return getResult();
        }

        void performLong()
        {
            if (_status.longLots > 0)
            {
                _rmLong.Update(_price);

                if (_rmLong.Warning(0.3m))
                {
                    _longLock = true;
                    appendSignal(Actions.CloseLong, 1);
                }
            }

            if ((!_longLock) && (_status.longLots == 0))
                if (_price > _basePrice * (1 + Bar))
                {
                    appendSignal(Actions.OpenLong, 1);
                    _rmLong = new FloatingMonitor(Direction.Long, _price);
                }
        }

        void performShort()
        {
            if (_status.shortLots > 0)
            {
                _rmShort.Update(_price);

                if (_rmShort.Warning(0.3m))
                {
                    _shortLock = true;
                    appendSignal(Actions.CloseShort, 1);
                }
            }

            if ((!_shortLock) && (_status.shortLots == 0))
                if (_price < _basePrice * (1 - Bar))
                {
                    appendSignal(Actions.OpenShort, 1);
                    _rmShort = new FloatingMonitor(Direction.Short, _price);
                }
        }
    }
}
