using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Models
{
    public class CarriageFreeSeatsInfo
    {
        public int TrainId { get; set; }
        public string Name { get; set; }
        public int FreeSeats { get; set; }

    }
}
