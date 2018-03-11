using System;

namespace Heron.Utility
{
    public static class Common
    {
        public static DateTime[] DeliveryDay = {
            new DateTime(2016, 2, 19),
            new DateTime(2016, 1, 15),
            new DateTime(2015, 12, 18),
            new DateTime(2015, 11, 20),
            new DateTime(2015, 10, 16),
            new DateTime(2015, 9, 18),
            new DateTime(2015, 8, 21),
            new DateTime(2015, 7, 17),
            new DateTime(2015, 6, 19),
            new DateTime(2015, 5, 15),
            new DateTime(2015, 4, 17),
            new DateTime(2015, 3, 20),
            new DateTime(2015, 2, 20),
            new DateTime(2015, 1, 16),
        };

        public static string Translate(Actions s)
        {
            switch (s)
            {
                case Actions.OpenLong:
                    return "开多";
                case Actions.OpenShort:
                    return "开空";
                case Actions.CloseLong:
                    return "平多";
                case Actions.CloseShort:
                    return "平空";
                default:
                    return "";
            }
        }

        public static string Translate(Direction d)
        {
            switch (d)
            {
                case Direction.Long:
                    return "多";
                case Direction.Short:
                    return "空";
                default:
                    return "";
            }
        }
    }
}
