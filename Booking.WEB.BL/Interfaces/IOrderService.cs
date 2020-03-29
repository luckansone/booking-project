using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(int personId, int ticketId);
    }
}
