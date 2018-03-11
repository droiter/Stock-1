using System;
using System.Data;
using System.Threading;
using Heron.Utility;

namespace Heron.DBAccess
{
    public class DBService
    {
        public DBService()
        {
            bool succeed = false;

            while (!succeed)
            {
                try
                {
                    succeed = SqlBackend.Connect();
                    if (!succeed)
                        Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    succeed = false;
                }
            }
        }

        public DataTable Retrieve(DateTime start, DateTime end)
        {
            return SqlBackend.Select(start, end);
        }

        public DataTable Retrieve(DateTime start, DateTime end, string[] codes)
        {
            return SqlBackend.Select(start, end, codes);
        }

        public void Add(Signal signal)
        {
            SqlBackend.Insert(signal);
        }
    }
}
