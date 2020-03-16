using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class CarFreeInfoViewModel
    {
        public int TrainId { get; set; }
        public string Name { get; set; }
        public int FreeSeats { get; set; }
    }
}