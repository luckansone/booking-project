using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Booking.Models
{
    public class SmtpSendConfiguration
    {
        public string DefaultEmailFrom { get; set; } 
        public string DefaultNameFrom { get; set; } 
        public string Password { get; set; }

        public SmtpSendConfiguration()
        {
            DefaultEmailFrom = ConfigurationManager.ConnectionStrings["DefaultEmail"].ConnectionString;
            DefaultNameFrom = ConfigurationManager.ConnectionStrings["DefaultName"].ConnectionString;
            Password = ConfigurationManager.ConnectionStrings["DefaultPassword"].ConnectionString;
        }
    }
}