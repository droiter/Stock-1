using WAPIWrapperCSharp;
using System;
using System.Globalization;
using Heron.Utility;

namespace Heron.Data
{
    static class DataHelper
    {
        public static DataNode ParseItem(WindData item, string type)
        {
            object[,] result = (object[,])item.getDataByFunc(type, false);

            if (item.GetFieldLength() < 3)
                return null;

            string l = result[0, 2].ToString();
            if (l.Length == 5)
                l = "0" + l;

            DateTime t = DateTime.Now.Date;
            if (item.timeList != null)
                t = item.timeList[0].Date;

            t += DateTime.ParseExact(l, "HHmmss", CultureInfo.InvariantCulture).TimeOfDay;

            var info = new DataNode()
            {
                code = item.codeList[0],
                price = decimal.Parse(result[0, 0].ToString()),
                volume = int.Parse(result[0, 1].ToString()),
                time = t,
            };

            return info;
        }

        public static DataNode[] ParseData(WindData item, string type)
        {
            if (item.errorCode != 0)
                if (((object[])item.data)[0].ToString().Equals("No Content", StringComparison.OrdinalIgnoreCase))
                    return null;

            object[,] result = (object[,])item.getDataByFunc(type, false);

            var info = new DataNode[item.GetTimeLength()];
            for (int i = 0; i < info.Length; i++)
                info[i] = new DataNode()
                {
                    price = decimal.Parse(result[i, 0].ToString()),
                    volume = int.Parse(result[i, 1].ToString()),
                    time = item.timeList[i],
                    code = item.codeList[0],
                };

            return info;
        }

        public static string[] ParseInfo(WindData item, string type)
        {
            object[,] result = (object[,])item.getDataByFunc(type, false);

            var info = new string[item.GetTimeLength()];
            for (int i = 0; i < info.Length; i++)
                info[i] = result[i, 0].ToString();

            return info;
        }
    }
}
