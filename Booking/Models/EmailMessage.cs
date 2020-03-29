using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models
{
    public class EmailMessage
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}