using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class Info
    {
        public List<RouteInfo> RouteInfo { get; set; }
        public Info()
        {
            RouteInfo = new List<RouteInfo>();
        }
    }
}
