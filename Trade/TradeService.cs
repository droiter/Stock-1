using Heron.Utility;

namespace Heron.Trade
{
    public class TradeService
    {
        public bool Execute(Signal signal)
        {
            return TradeSupport.Trading(signal);
        }

        public PosStatus Query()
        {
            return TradeSupport.Retrieve();
        }
    }
}
