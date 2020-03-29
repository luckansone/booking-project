using Booking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface ITicketService
    {
        List<Ticket> Create(List<Ticket> tickets);
    }
}
