using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public double Price { get; set; }
        public int SeatId { get; set; }
        public int RouteId { get; set; }

    }
}
