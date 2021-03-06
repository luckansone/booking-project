﻿using Booking.BL.Services;
using Booking.DAL;
using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.DAL.Repositories;
using Booking.DAL.SearchEngine;
using Booking.Interfaces;
using Booking.Interfaces.Mapping;
using Booking.Services;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.BL.Services;
using Booking.WEB.DAL.Calculator;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;
using Booking.WEB.DAL.Repositories;
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
            Bind<IRepository<City>>().To<CityRepository>();
            Bind<IRepository<Order>>().To<OrderRepository>();
            Bind<IRepository<Person>>().To<PeopleRepository>();
            Bind<IRepository<Ticket>>().To<TicketRepository>();
            Bind<IRepository<ReservedSeat>>().To<ReservedSeatRepository>();
            Bind<ICityService>().To<CityService>();
            Bind<ISearchEngine<Info, SearchTrainsModel>>().To<InfoSearchEngine>();
            Bind<IBookingContext>().To<BookingContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<ITrainSearchService<Info, SearchTrainsModel>>().To<TrainSearchService>();
            Bind<IMapperControl>().To<MapperControl>();
            Bind<ICalculator>().To<Calculator>();
            Bind<IPriceCalculateService>().To<PriceCalculateService>();
            Bind<IPersonService>().To<PersonService>();
            Bind<IReservedSeatService>().To<ReservedSeatService>();
            Bind<ITicketService>().To<TicketService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IPdfCreator>().To<PdfCreator>();
            Bind<IEmailSender>().To<EmailSender>();
        }
    }
}