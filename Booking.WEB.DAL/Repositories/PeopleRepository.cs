using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Booking.WEB.DAL.Interfaces;

namespace Booking.DAL.Repositories
{
    public class PeopleRepository : IRepository<Person>
    {
        private IBookingContext _context;
        public PeopleRepository(IBookingContext context)
        {
            _context = context;
        }

        public Person Create(Person item)
        {
            string proc = "AddPerson";
            using (IDbConnection conn = _context.GetConnection())
            {
               int userId = conn.Query<int>(proc, new { @Name = item.Name, @Surname = item.Surname, @Patronymic = item.Patronymic,
                @DateOfBirth = item.DateOfBirth, @Email = item.Email, @Phone = item.Phone}, commandType: CommandType.StoredProcedure).FirstOrDefault();

                item.PersonId = userId;

            }
            return item;
        }

        public Person GetItemById(int id)
        {
            Person person = null;

            using(IDbConnection conn = _context.GetConnection())
            {
                person = conn.Query<Person>("Select * From Person WHERE PersonId = @id", new { id}).FirstOrDefault();
            }

            return person;
            
        }

        public List<Person> GetItems()
        {
            List<Person> people = new List<Person>();

            using(IDbConnection conn =_context.GetConnection())
            {
               people  = conn.Query<Person>("SELECT * FROM Person").ToList();
            }

            return people;
        }
    }
}
