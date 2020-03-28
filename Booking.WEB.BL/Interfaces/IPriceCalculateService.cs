using Booking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface IPriceCalculateService
    {
        double GetPrice(RouteInfo model);
    }
}
