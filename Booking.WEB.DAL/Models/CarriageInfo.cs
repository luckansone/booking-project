using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.DAL.Models
{
    public class CarriageInfo
    {
        public int TrainId { get; set; }
        public int CarriageId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int CountOfSeats { get; set; }
        public List<ReservedSeat> ReservedSeats { get; set; }

        public CarriageInfo()
        {
            ReservedSeats = new List<ReservedSeat>();
        }
    }
}
