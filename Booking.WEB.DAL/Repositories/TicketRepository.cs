using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.WEB.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Booking.DAL.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        private IBookingContext _context;

        public TicketRepository(IBookingContext context)
        {
            _context = context;
        }
       

        public Ticket GetItemById(int id)
        {
            Ticket ticket = null;

            using (IDbConnection conn = _context.GetConnection())
            {
                ticket = conn.Query<Ticket>(@"SELECT t.TicketId, t.Price, seat.Number AS SeatNumber, car.Number AS CarriageNumber, tr.Name AS TrainNumber, r.Description AS RouteDescription FROM Ticket AS t
                                                INNER JOIN ReservedSeat AS seat
                                                ON t.SeatId = seat.SeatId
                                                INNER JOIN Carriage AS car
                                                ON car.CarriageId= seat.CarriageId
                                                INNER JOIN Train AS tr
                                                ON tr.TrainId = car.TrainId
                                                INNER JOIN Route AS r
                                                ON t.RouteId = r.RouteId
                                                WHERE TicketId = @id", new { id }).FirstOrDefault();
            }

            return ticket;
        }

        public List<Ticket> GetItems()
        {
            List<Ticket> tickets = new List<Ticket>();

            using (IDbConnection conn = _context.GetConnection())
            {
                tickets = conn.Query<Ticket>(@"SELECT t.TicketId, t.Price, seat.Number AS SeatNumber, car.Number AS CarriageNumber, tr.Name AS TrainNumber, r.Description AS RouteDescription FROM Ticket AS t
                                                INNER JOIN ReservedSeat AS seat
                                                ON t.SeatId = seat.SeatId
                                                INNER JOIN Carriage AS car
                                                ON car.CarriageId= seat.CarriageId
                                                INNER JOIN Train AS tr
                                                ON tr.TrainId = car.TrainId
                                                INNER JOIN Route AS r
                                                ON t.RouteId = r.RouteId; ").ToList();
            }

            return tickets;
        }

        public Ticket Create(Ticket item)
        {
            using(IDbConnection conn = _context.GetConnection())
            {
                int ticketId = conn.Query<int>(String.Format("INSERT INTO Ticket(Price, SeatId, RouteId) VALUES({0},{1},{2});SELECT CAST(SCOPE_IDENTITY() as int);",
                    item.Price, item.SeatId, item.RouteId)).FirstOrDefault();

                item.TicketId = ticketId;
            }

            return item;
        }
    }
}
