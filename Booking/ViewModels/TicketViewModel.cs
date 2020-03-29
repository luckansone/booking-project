using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class TicketViewModel
    {
        public int SeatId { get; set; }
        public int RouteId { get; set; }
        public double Price { get; set; }
        public int SeatNumber { get; set; }
        public int CarriageNumber { get; set; }
        public string TrainName { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Description { get; set; }
        public string SNP { get; set; }
        public string Email { get; set; }
    }
}