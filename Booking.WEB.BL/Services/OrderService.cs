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
    public class OrderService : IOrderService
    {
        IUnitOfWork unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void MakeOrder(int personId, int ticketId)
        {
            unitOfWork.orderRepository.Create(new Order { PersonId = personId, TicketId = ticketId});
        }
    }
}
