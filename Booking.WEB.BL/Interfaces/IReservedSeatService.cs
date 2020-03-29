using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface IReservedSeatService
    {
        List<ReservedSeat> Create(List<ReservedSeat> seats);
    }
}
