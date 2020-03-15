using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class CarInfoViewModel
    {
        public int Number { get; set; }
        public int TrainId { get; set; }
        public string Name { get; set; }
        public int CountOfSeats { get; set; }
    }
}