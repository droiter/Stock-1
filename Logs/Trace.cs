using System;
using System.IO;
using System.Text;

namespace Heron.Logs
{
    static class Trace
    {
        static decimal _sum;
        readonly static object _threadLock = new object();

        static Trace()
        {
            _sum = 0;

            if (File.Exists("result.csv"))
                File.Delete("result.csv");
        }

        public static void Record(
            string code,
            string name,
            string direction,
            DateTime openTime,
            decimal openPrice,
            DateTime closeTime,
            decimal closePrice,
            int lots,
            decimal profit)
        {
            lock (_threadLock)
            {
                _sum += profit;

                File.AppendAllText("result.csv",
                    code + ","
                    + name + ","
                    + direction + ","
                    + openTime + ","
                    + openPrice + ","
                    + closeTime + ","
                    + closePrice + ","
                    + lots + ","
                    + profit + ","
                    + _sum + "\n",
                    Encoding.GetEncoding("gb2312"));
            }
        }
    }
}
