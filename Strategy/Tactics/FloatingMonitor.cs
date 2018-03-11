using Heron.Utility;

namespace Heron.Strategy
{
    class FloatingMonitor
    {
        const decimal Bar = 0.4m / 100;

        decimal _cost;
        decimal _maxProfit;
        decimal _floating;

        Direction _type;

        public FloatingMonitor(Direction type, decimal cost)
        {
            _type = type;
            _cost = cost;
            _maxProfit = 0;
            _floating = 0;
        }

        public void Update(decimal price)
        {
            if (_type == Direction.Long)
                _floating = price - _cost;
            if (_type == Direction.Short)
                _floating = _cost - price;

            if (_floating > _maxProfit)
                _maxProfit = _floating;
        }

        public bool Warning(decimal rate)
        {
            if (_floating <= -_cost * Bar)
                return true;

            if (_floating >= _cost * Bar)
                if ((1 - _floating / _maxProfit) >= rate)
                    return true;

            return false;
        }
    }
}
