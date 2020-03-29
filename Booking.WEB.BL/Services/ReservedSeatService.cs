using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Services
{
    public class ReservedSeatService : IReservedSeatService
    {
        IUnitOfWork unitOfWork;
        public ReservedSeatService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<ReservedSeat> Create(List<ReservedSeat> seats)
        {
            List<ReservedSeat> reservedSeats = new List<ReservedSeat>();

            foreach(var seat in seats)
            {
                reservedSeats.Add(unitOfWork.reservedSeatRepository.Create(seat));
            }

            return reservedSeats;
        }
    }
}
