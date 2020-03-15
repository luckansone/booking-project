using Booking.BL.Services;
using Booking.DAL;
using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.DAL.Repositories;
using Booking.DAL.SearchEngine;
using Booking.Interfaces.Mapping;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.Mapping;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.App_Start
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Order>>().To<OrderRepository>();
            Bind<IRepository<Person>>().To<PeopleRepository>();
            Bind<IRepository<Ticket>>().To<TicketRepository>();
            Bind<ISearchEngine<Info, SearchTrainsModel>>().To<InfoSearchEngine>();
            Bind<IBookingContext>().To<BookingContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<ITrainSearchService<Info, SearchTrainsModel>>().To<TrainSearchService>();
            Bind<IMapperControl>().To<MapperControl>();
        }
    }
}