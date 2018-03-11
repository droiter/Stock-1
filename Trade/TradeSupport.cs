using System;
using Heron.Utility;

namespace Heron.Trade
{
    static class TradeSupport
    {
        static TradeSupport()
        { }

        public static bool Trading(Signal signal)
        {
            return true;
        }

        public static PosStatus Retrieve()
        {
            return new PosStatus();
        }
    }
}
