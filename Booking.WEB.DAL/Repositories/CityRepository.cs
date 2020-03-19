using Booking.DAL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.DAL.Repositories
{
    public class CityRepository : IRepository<City>
    {
        private IBookingContext _context;

        public CityRepository(IBookingContext context)
        {
            _context = context;
        }
        public City Create(City item)
        {
            throw new NotImplementedException();
        }

        public City GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public List<City> GetItems()
        {
            List<City> cities = new List<City>();

            using (IDbConnection conn = _context.GetConnection())
            {
                cities = conn.Query<City>("SELECT * FROM City").ToList();
            }

            return cities;
        }
    }
}
