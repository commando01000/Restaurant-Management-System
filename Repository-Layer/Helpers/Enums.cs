using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Repository
{
    public static class TimeZones
    {
        public const string EgyptTimezone = "Egypt Standard Time";
    }
    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Canceled = 4
    }
}
