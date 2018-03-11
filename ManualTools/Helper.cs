using System;
using System.Data;
using System.IO;
using Heron.Utility;
using Heron.Data;
using Heron.DBAccess;

namespace Heron.ManualTools
{
    static class Helper
    {
        public static Signal GenerateOrder(string code, Actions action)
        {
            var source = new DataService(code);
            var item = source.CurrentItem();

            return new Signal()
            {
                actInfo = action,
                code = source.Code,
                name = source.Name,
                lots = 1,
                price = item.price,
                time = item.time,
            };
        }

        public static DataTable Retrieve()
        {
            string[] codes = File.ReadAllLines("codes.txt");

            DBService service = new DBService();
            return service.Retrieve(DateTime.Now.Date, DateTime.Now.Date.AddDays(1), codes);
        }

        public static void Add(Signal signal)
        {
            DBService service = new DBService();
            service.Add(signal);
        }
    }
}
