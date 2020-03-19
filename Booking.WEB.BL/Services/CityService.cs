using Booking.DAL.Interfaces;
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
    public class CityService : ICityService
    {
        private IUnitOfWork unitOfWork { get; set; }
        public CityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<City> GetCities()
        {
            return unitOfWork.cityRepository.GetItems();
        }
    }
}
