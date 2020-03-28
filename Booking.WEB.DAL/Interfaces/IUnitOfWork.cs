using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<City> cityRepository { get; set; }
        IRepository<Person> personRepository { get; set; }
         IRepository<Order> orderRepository { get; set; }
         IRepository<Ticket> ticketRepository { get; set; }
         ISearchEngine<Info, SearchTrainsModel> infoRepository { get; set; }
         ICalculator Calculator { get; set; }
    }
}
