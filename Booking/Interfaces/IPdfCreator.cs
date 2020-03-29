using Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Interfaces
{
    public interface IPdfCreator
    {
        void CreatePdf(List<TicketViewModel> ticketInfo);
    }
}