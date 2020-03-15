using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class InfoViewModel
    {
        public List<RouteInfoViewModel> RouteInfoViewModels { get; set; }
        public List<CarInfoViewModel> CarInfoViewModels { get; set; }

        public InfoViewModel()
        {
            RouteInfoViewModels = new List<RouteInfoViewModel>();
            CarInfoViewModels = new List<CarInfoViewModel>();
        }
    }
}