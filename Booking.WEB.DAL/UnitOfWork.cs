using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.DAL.Repositories;
using Booking.DAL.SearchEngine;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;

namespace Booking.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        public IRepository<City> cityRepository { get; set; }
        public IRepository<Person> personRepository { get; set; }
        public IRepository<Order> orderRepository { get; set; }
        public IRepository<Ticket> ticketRepository { get; set; }
        public ISearchEngine<Info, SearchTrainsModel> infoRepository { get; set; }

        public UnitOfWork(IRepository<Person> personRepository, IRepository<Order> orderRepository,
            IRepository<Ticket> ticketRepository, ISearchEngine<Info,SearchTrainsModel> infoRepository,
            IRepository<City> cityRepository)
        {
            this.personRepository = personRepository;
            this.orderRepository = orderRepository;
            this.ticketRepository = ticketRepository;
            this.infoRepository = infoRepository;
            this.cityRepository = cityRepository;
        }

    }
}
