using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int PersonId { get; set; }
        public string DiscountName { get; set; }
        public string DiscountType { get; set; }
        public double DiscountValue { get; set; }

    }
}
