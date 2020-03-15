using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class SearchTrainsModel
    {
        public DateTime DepartureDate { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
    }
}
