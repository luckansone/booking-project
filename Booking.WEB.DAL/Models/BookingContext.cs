using Booking.WEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Booking.DAL.Models
{
    public class BookingContext: IBookingContext
    {
        private string connectionString;

        public BookingContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        
    }
}
