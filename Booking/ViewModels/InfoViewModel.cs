using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class InfoViewModel
    {
        public List<RouteInfoViewModel> RouteInfo { get; set; }
        public RouteInfoViewModel TrainInfo { get; set; }

        public InfoViewModel()
        {
            RouteInfo = new List<RouteInfoViewModel>();
        }
    }
}