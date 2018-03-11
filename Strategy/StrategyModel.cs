using System;
using System.Collections.Generic;
using Heron.Utility;
using Heron.Data;

namespace Heron.Strategy
{
    public abstract class StrategyModel
    {
        abstract public Signal[] Update(DataNode item);
        abstract public void Feedback(PosStatus status);

        protected DataService _provider;
        protected PosStatus _status;
        protected List<Signal> _signals;

        protected decimal _basePrice;
        protected int _baseVol;

        protected decimal _price;
        protected int _vol;
        protected DateTime _time;

        protected decimal _highest;
        protected decimal _lowest;

        protected void init(DataService source)
        {
            _provider = source;
        }

        protected Signal[] getResult()
        {
            return _signals.ToArray();
        }

        protected void feedbackInfo(PosStatus status)
        {
            _status = status;
        }

        protected void updateData(DataNode item, bool reset)
        {
            if (item.time.Date != _time.Date)
            {
                _basePrice = _provider.GetPreClose(item.time);
                _baseVol = _provider.GetRecentAvgVol(item.time);

                if (reset)
                {
                    _lowest = _basePrice * 0.985m;
                    _highest = _basePrice * 1.015m;
                }
            }

            _price = item.price;
            _vol = item.volume;
            _time = item.time;

            if (_price > _highest)
                _highest = _price;
            if (_price < _lowest)
                _lowest = _price;

            _signals = new List<Signal>();
        }

        protected void appendSignal(Actions action, int amt)
        {
            _signals.Add(new Signal()
            {
                actInfo = action,
                code = _provider.Code,
                name = _provider.Name,
                lots = amt,
                price = _price,
                time = _time,
            });
        }

        protected void clear()
        {
            if (_status.longLots > 0)
                appendSignal(Actions.CloseLong, 1);

            if (_status.shortLots > 0)
                appendSignal(Actions.CloseShort, 1);
        }
    }
}
