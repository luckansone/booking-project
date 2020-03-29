using Booking.DAL.Models;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Services
{
    public class TicketService : ITicketService
    {
        IUnitOfWork unitOfWork;
        public TicketService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<Ticket> Create(List<Ticket> tickets)
        {
            List<Ticket> regTickets = new List<Ticket>();

            foreach(var ticket in tickets)
            {
                regTickets.Add(unitOfWork.ticketRepository.Create(ticket));
            }

            return regTickets;
        }
    }
}
