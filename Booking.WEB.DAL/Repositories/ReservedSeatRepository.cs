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
    public class ReservedSeatRepository : IRepository<ReservedSeat>
    {
        private IBookingContext _context;
        public ReservedSeatRepository(IBookingContext context)
        {
            _context = context;
        }
        public ReservedSeat Create(ReservedSeat item)
        {
            using (IDbConnection conn = _context.GetConnection())
            {
                int seatId = conn.Query<int>(String.Format("INSERT INTO ReservedSeat (CarriageId, Number)VALUES({0},{1});SELECT CAST(SCOPE_IDENTITY() as int) "
                    ,item.CarriageId, item.Number)
                    ).FirstOrDefault();
               

                item.SeatId = seatId;

            }
            return item;
        }

        public ReservedSeat GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ReservedSeat> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
