using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class RouteInfoViewModel
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string Description { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Duration { get; set; }
    }
}