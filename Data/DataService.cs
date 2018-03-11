using System;
using Heron.Utility;

namespace Heron.Data
{
    public delegate void DataHandler(DataNode item);

    public class DataService
    {
        ulong _rid;

        public string Code { get; set; }
        public string Name { get; set; }

        public DataService(string code)
        {
            Code = code;
            Name = getName();
        }

        public DataNode CurrentItem()
        {
            return DataFeed.QueryItem(Code);
        }

        public DataNode[] DailyData(DateTime day)
        {
            return DataFeed.QueryData(Code, day);
        }

        public DataNode[] DailyDataK(DateTime day)
        {
            return DataFeed.QueryDataK(Code, day);
        }

        public void Start(DataHandler handler)
        {
            _rid = DataFeed.Start(Code, handler);
        }

        public void Stop()
        {
            DataFeed.Stop(_rid);
        }

        string getName()
        {
            return DataFeed.Query(Code, "sec_name", "0TD", "0TD")[0];
        }

        int getVol(DateTime day)
        {
            var s = day.ToString();
            return int.Parse(DataFeed.Query(Code, "volume", s, s)[0]);
        }

        public decimal GetPreClose(DateTime day)
        {
            var s = day.ToString();
            return decimal.Parse(DataFeed.Query(Code, "pre_close", s, s)[0]);
        }

        public int GetRecentAvgVol(DateTime day)
        {
            var start = "ED-5TD";
            var end = day.Date.AddDays(-1).ToString();

            var info = DataFeed.Query(Code, "volume", start, end);

            int sum = 0;
            foreach (var i in info)
                sum = sum / 2 + int.Parse(i);

            if (sum == 0)
                return getVol(day);
            else
                return sum / 2;
        }
    }
}
