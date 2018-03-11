using WAPIWrapperCSharp;
using System;
using System.Collections.Generic;
using Heron.Utility;

namespace Heron.Data
{
    static class DataFeed
    {
        static WindAPI _api;
        static Dictionary<ulong, DataHandler> _dict;

        readonly static object _threadLock = new object();

        static DataFeed()
        {
            _dict = new Dictionary<ulong, DataHandler>();
            _api = new WindAPI();
            _api.start();
        }

        static void wrapper(ulong rid, WindData item)
        {
            var s = DataHelper.ParseItem(item, "wsq");
            if ((s != null) && (s.time <= DateTime.Now))
            {
                DataHandler handler;
                lock (_threadLock)
                {
                    handler = _dict[rid];
                }

                handler(s);
            }
        }

        public static ulong Start(string code, DataHandler handler)
        {
            string fields = "rt_last,rt_vol,rt_time";
            //for (int i = 1; i <= 5; i++)
            //    fields +=
            //        ",rt_ask" + i.ToString() +
            //        ",rt_asize" + i.ToString() +
            //        ",rt_bid" + i.ToString() +
            //        ",rt_bsize" + i.ToString();

            int errCode = 0;
            ulong id;

            lock (_threadLock)
            {
                id = _api.wsq(ref errCode, code, fields, "", wrapper, true);
                _dict.Add(id, handler);
            }

            return id;
        }

        public static void Stop(ulong id)
        {
            lock (_threadLock)
            {
                _api.cancelRequest(id);
                _dict.Remove(id);
            }
        }

        public static DataNode QueryItem(string code)
        {
            string fields = "rt_last,rt_vol,rt_time";

            WindData result;
            lock (_threadLock)
            {
                result = _api.wsq(code, fields, "");
            }

            return DataHelper.ParseItem(result, "wsq");
        }

        public static DataNode[] QueryDataK(string code, DateTime day)
        {
            WindData result;
            lock (_threadLock)
            {
                var start = day.Date.AddHours(9.5);
                var end = day.Date.AddHours(15);
                result = _api.wsi(code, "close,volume", start, end, "BarSize=1");
            }

            return DataHelper.ParseData(result, "wsi");
        }

        public static DataNode[] QueryData(string code, DateTime day)
        {
            WindData result;
            lock (_threadLock)
            {
                var start = day.Date.AddHours(9.5);
                var end = day.Date.AddHours(15);
                result = _api.wst(code, "last,volume", start, end, "");
            }

            return DataHelper.ParseData(result, "wst");
        }

        public static string[] Query(string code, string field, string start, string end)
        {
            WindData result;
            lock (_threadLock)
            {
                result = _api.wsd(code, field, start, end, "");
            }

            return DataHelper.ParseInfo(result, "wsd"); ;
        }
    }
}
