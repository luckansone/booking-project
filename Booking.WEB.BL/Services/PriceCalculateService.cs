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
    public class PriceCalculateService : IPriceCalculateService
    {
        private readonly IUnitOfWork unitOfWork;
        public PriceCalculateService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public double GetPrice(RouteInfo model)
        {
           double averagePrice = 250;
           double speed = unitOfWork.Calculator.GetTrainSpeed(model.TrainId);
           double distance = unitOfWork.Calculator.GetDistance(model.RouteId);

           return averagePrice * CarTypeCoef(model.SelectedCarriage.Name)*DistanceCoef(distance) *SpeedCoef(speed);
        }

        private double CarTypeCoef(string carType)
        {
            double coef = 1;

            switch (carType)
            {
                case "Плацкарт":
                    {
                        coef = 1.2;
                        break;
                    }
                case "Купе":
                    {
                        coef = 1.3;
                        break;
                    }
                case "Люкс":
                    {
                        coef = 1.4;
                        break;
                    }
            }

            return coef;
        }

        private double SpeedCoef(double speed)
        {
            if (speed <= 80)
            {
                return 1;
            }
            else
            {
                if (speed <= 90)
                {
                    return 1.2;
                }
                else
                {
                    return 1.4;
                }
            }
        }

        private double DistanceCoef(double distance)
        {
            if (distance <= 300) 
            {
                return 1;
            }
            else
            {
                if (distance <= 350)
                {
                    return 1.2;
                }
                else
                {
                    return 1.4;
                }
            }

        }
    }
}
