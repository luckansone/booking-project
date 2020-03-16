using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class RouteInfo
    {
        public int RouteId { get; set; }
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string Description { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Duration { get; set; }
        public List<CarriageFreeSeatsInfo> CarriageFreeSeatsInfos { get; set; }
        public List<CarriageInfo> CarriageInfos { get; set; }

        public RouteInfo()
        {
            CarriageFreeSeatsInfos = new List<CarriageFreeSeatsInfo>();
            CarriageInfos = new List<CarriageInfo>();
        }
    }
}
