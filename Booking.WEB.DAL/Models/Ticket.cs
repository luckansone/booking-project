using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public double Price { get; set; }
        public int SeatNumber { get; set; }
        public int CarriageNumber { get; set; }
        public string TrainName { get; set; }
        public string RouteDescription { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
