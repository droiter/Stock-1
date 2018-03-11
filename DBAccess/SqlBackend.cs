using System;
using System.Data;
using System.Data.SqlClient;
using Heron.Utility;

namespace Heron.DBAccess
{
    static class SqlBackend
    {
        //fill your Sql DB connection string
        const string ConStr = "";

        static SqlConnection _sc;
        readonly static object _threadLock = new object();

        static SqlBackend()
        {
            _sc = new SqlConnection(ConStr);
        }

        public static bool Connect()
        {
            if (_sc.State == ConnectionState.Open)
                return true;

            if (_sc.State == ConnectionState.Closed || _sc.State == ConnectionState.Broken)
            {
                lock (_threadLock)
                {
                    _sc.Open();
                }
                return true;
            }
            else
                return false;
        }

        public static DataTable Select(DateTime start, DateTime end, string[] codes = null)
        {
            string cmd = "SELECT * FROM [dbo].[Signals]";

            cmd += " WHERE [Time] BETWEEN '" + start.ToString()
                + "' AND '" + end.ToString() + "'";

            if (codes != null)
            {
                cmd += " AND [Code] IN (";

                foreach (var code in codes)
                {
                    if (code != null && code.Trim() != "")
                        cmd += "'" + code.Trim() + "',";
                }

                cmd += "'')";
            }

            var da = new SqlDataAdapter(cmd, _sc);
            var dt = new DataTable();

            lock (_threadLock)
            {
                da.Fill(dt);
            }

            return dt;
        }

        public static void Insert(Signal signal)
        {
            string cmd = "INSERT INTO [dbo].[Signals] VALUES('"
                + signal.time.ToString() + "', '"
                + signal.code + "', N'"
                + signal.name + "', N'"
                + Common.Translate(signal.actInfo) + "', "
                + signal.price.ToString() + ")";

            SqlCommand sc = new SqlCommand(cmd, _sc);

            lock (_threadLock)
            {
                sc.ExecuteNonQuery();
            }
        }
    }
}
