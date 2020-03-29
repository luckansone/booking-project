using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models
{
    public class EmailStringAttachment: EmailMessage
    {
        public List<string> AttachmentsFilePath { get; set; }

        public EmailStringAttachment()
        {
            AttachmentsFilePath = new List<string>();
        }
    }
}