using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.DAL.Interfaces
{
    public interface ICalculator
    {
        double GetDistance(int RouteId);
        double GetTrainSpeed(int TrainId);
    }
}
