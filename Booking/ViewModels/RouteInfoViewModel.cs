using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class RouteInfoViewModel
    {

        public int RouteId { get; set; }
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string Description { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Duration { get; set; }

        public List<CarFreeInfoViewModel> CarriageFreeInfos { get; set; }
        public List<CarInfoViewModel> CarInfoViewModels { get; set; }
        public CarInfoViewModel SelectedCarriage { get; set; }

        public RouteInfoViewModel()
        {
            CarriageFreeInfos = new List<CarFreeInfoViewModel>();
            CarInfoViewModels = new List<CarInfoViewModel>();
            SelectedCarriage = new CarInfoViewModel();
        }
    }
}