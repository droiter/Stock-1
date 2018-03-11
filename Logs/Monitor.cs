using Heron.Utility;

namespace Heron.Logs
{
    class Monitor
    {
        PosStatus _pos;

        public Monitor(string code)
        {
            _pos.code = code;
        }

        void openLong(Signal signal)
        {
            var weight = (decimal)signal.lots / (_pos.longLots + signal.lots);
            _pos.longCost = signal.price * weight + _pos.longCost * (1 - weight);

            _pos.longLots += signal.lots;
            _pos.longTime = signal.time;
        }

        void openShort(Signal signal)
        {
            var weight = (decimal)signal.lots / (_pos.shortLots + signal.lots);
            _pos.shortCost = signal.price * weight + _pos.shortCost * (1 - weight);

            _pos.shortLots += signal.lots;
            _pos.shortTime = signal.time;
        }

        decimal closeLong(Signal signal)
        {
            _pos.longLots -= signal.lots;
            return (signal.price - _pos.longCost) * signal.lots;
        }

        decimal closeShort(Signal signal)
        {
            _pos.shortLots -= signal.lots;
            return (_pos.shortCost - signal.price) * signal.lots;
        }

        public decimal Process(Signal signal)
        {
            decimal profit = 0;

            switch (signal.actInfo)
            {
                case Actions.OpenLong:
                    openLong(signal);
                    break;
                case Actions.OpenShort:
                    openShort(signal);
                    break;
                case Actions.CloseLong:
                    profit = closeLong(signal);
                    break;
                case Actions.CloseShort:
                    profit = closeShort(signal);
                    break;
            }

            return profit;
        }

        public PosStatus Retrieve()
        {
            return _pos;
        }
    }
}
