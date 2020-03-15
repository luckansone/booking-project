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
    public class OrderRepository : IRepository<Order>
    {
        private IBookingContext _context;
        public OrderRepository(IBookingContext context)
        {
            _context = context;
        }

        public Order GetItemById(int id)
        {
            Order person = null;

            using (IDbConnection conn = _context.GetConnection())
            {
                person = conn.Query<Order>(@"SELECT OrderId, PersonId, dis.Name AS DiscountName, distype.TypeName AS DiscountType, dis.Value AS DiscountValue
                                                FROM dbo.""Order"" AS ord
                                                INNER JOIN Discount AS dis
                                                ON ord.DiscountId = dis.DiscountId
                                                INNER JOIN DiscountType AS distype
                                                ON dis.DiscountTypeId = distype.DiscountTypeId WHERE OrderId = @id", new { id }).FirstOrDefault();
            }

            return person;
        }

        public List<Order> GetItems()
        {
            List<Order> orders = new List<Order>();

            using (IDbConnection conn = _context.GetConnection())
            {
                orders = conn.Query<Order>( @"SELECT OrderId, PersonId, dis.Name AS DiscountName, distype.TypeName AS DiscountType, dis.Value AS DiscountValue
                                                FROM dbo.""Order"" AS ord
                                                INNER JOIN Discount AS dis
                                                ON ord.DiscountId = dis.DiscountId
                                                INNER JOIN DiscountType AS distype
                                                ON dis.DiscountTypeId = distype.DiscountTypeId").ToList();
            }

            return orders;
        }

        public Order Create(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
