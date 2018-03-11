using System;

namespace Heron.Utility
{
    public enum Actions
    {
        Null,
        OpenLong,
        CloseLong,
        OpenShort,
        CloseShort,
    }

    public struct Signal
    {
        public Actions actInfo;
        public string code;
        public string name;
        public decimal price;
        public DateTime time;
        public int lots;
    }

    public struct PosStatus
    {
        public string code;
        public int longLots;
        public int shortLots;
        public decimal longCost;
        public decimal shortCost;
        public DateTime longTime;
        public DateTime shortTime;
    }
}
