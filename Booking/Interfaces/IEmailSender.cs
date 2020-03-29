using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Booking.Interfaces
{
    public interface IEmailSender
    {
        Result SendEmail(EmailStringAttachment message);
    }
}