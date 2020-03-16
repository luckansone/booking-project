using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.ViewModels
{
    public class CarInfoViewModel
    {
        public int TrainId { get; set; }
        public int CarriageId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int CountOfSeats { get; set; }
        public List<ReservedSeatViewModel> ReservedSeats { get; set; }

        public CarInfoViewModel()
        {
            ReservedSeats = new List<ReservedSeatViewModel>();
        }
    }
}