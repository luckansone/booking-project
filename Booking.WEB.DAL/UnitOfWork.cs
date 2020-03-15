using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.DAL.Repositories;
using Booking.DAL.SearchEngine;
using Booking.WEB.DAL.Interfaces;

namespace Booking.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        public IRepository<Person> personRepository { get; set; }
        public IRepository<Order> orderRepository { get; set; }
        public IRepository<Ticket> ticketRepository { get; set; }
        public ISearchEngine<Info, SearchTrainsModel> infoRepository { get; set; }

        public UnitOfWork(IRepository<Person> personRepository, IRepository<Order> orderRepository, IRepository<Ticket> ticketRepository, ISearchEngine<Info,SearchTrainsModel> infoRepository)
        {
            this.personRepository = personRepository;
            this.orderRepository = orderRepository;
            this.ticketRepository = ticketRepository;
            this.infoRepository = infoRepository;
        }

    }
}
