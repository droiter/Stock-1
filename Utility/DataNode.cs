using System;

namespace Heron.Utility
{
    public enum Direction
    {
        Null,
        Long,
        Short,
    }

    public class DataNode
    {
        public string code;
        public decimal price;
        public int volume;
        public DateTime time;
    }

    public struct DailyInfo
    {
        public string code;
        public DateTime time;
        public decimal preClose;
        public decimal open;
        public decimal close;
        public decimal high;
        public decimal low;
        public int volume;
        public double amplitude;
    }
}
