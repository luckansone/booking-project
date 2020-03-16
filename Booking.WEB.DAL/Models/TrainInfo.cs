using Booking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.DAL.Models
{
    public class TrainInfo
    {
        public List<RouteInfo> RouteInfo { get; set; }
        public TrainInfo()
        {
            RouteInfo = new List<RouteInfo>();
        }
    }
}
