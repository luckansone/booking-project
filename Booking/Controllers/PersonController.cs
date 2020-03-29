using Booking.Interfaces.Mapping;
using Booking.ViewModels;
using Booking.WEB.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Web.Hosting;
using iText.IO.Font;
using Booking.Interfaces;

namespace Booking.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMapperControl mapperControl;
        private List<PersonViewModel> personViewModels;
        private readonly IPersonService personService;
        private readonly IReservedSeatService reservedSeatService;
        private readonly IOrderService orderService;
        private readonly ITicketService ticketService;
        private readonly IPdfCreator pdfCreator;

        List<TicketViewModel> ticketViewModels;
        public PersonController( IMapperControl mapperControl, IPersonService personService,
            IReservedSeatService reservedSeatService, ITicketService ticketService,
            IOrderService orderService, IPdfCreator pdfCreator)
        {
            this.mapperControl = mapperControl;
            this.personService = personService;
            this.reservedSeatService = reservedSeatService;
            this.ticketService = ticketService;
            this.orderService = orderService;
            this.pdfCreator = pdfCreator;
            personViewModels = new List<PersonViewModel>();
            ticketViewModels = new List<TicketViewModel>();
        }

        [HttpGet]
        public ActionResult PeopleForms()
        {
            RouteInfoViewModel model = Session["RouteInfo"] as RouteInfoViewModel;
            ViewBag.ChosenSeats = model.SelectedCarriage.ChosenSeats;
            
            for(int i = 0; i < model.SelectedCarriage.ChosenSeats.Count; i++)
            {
                personViewModels.Add(new PersonViewModel());
            }
            return View(personViewModels.ToList());
        }

        [HttpPost]
        public ActionResult PeopleForms(List<PersonViewModel> model)
        {
            var people = mapperControl.GetPersonListByPersonViewList(model);
            people = personService.CreatePerson(people);
            RouteInfoViewModel route = Session["RouteInfo"] as RouteInfoViewModel;
            List<ReservedSeatViewModel> seats = new List<ReservedSeatViewModel>();

            foreach(var el in route.SelectedCarriage.ChosenSeats)
            {
                seats.Add(new ReservedSeatViewModel { Number = el.Number, CarriageId = route.SelectedCarriage.CarriageId });
            }

            var reservedSeats = mapperControl.GetReservedSeatsByViewModel(seats);
            reservedSeats = reservedSeatService.Create(reservedSeats);

            for(int i = 0; i < people.Count(); i++)
            {
                int seatId = reservedSeats.Find(x => x.Number.Equals(route.SelectedCarriage.ChosenSeats[i].Number)).SeatId;
                ticketViewModels.Add(new TicketViewModel
                {
                    TrainName = route.TrainName,
                    SeatNumber = route.SelectedCarriage.ChosenSeats[i].Number,
                    Price = route.SelectedCarriage.ChosenSeats[i].Price,
                    RouteId = route.RouteId,
                    Description = route.Description,
                    ArrivalTime = route.ArrivalTime,
                    CarriageNumber = route.SelectedCarriage.Number,
                    DepartureTime = route.DepartureTime,
                    SeatId = seatId,
                    SNP = String.Format("{0} {1} {2}", people[i].Name, people[i].Surname, people[i].Patronymic)
                });
            }
            var tickets = mapperControl.GetTicketsByViewModel(ticketViewModels);
            tickets = ticketService.Create(tickets);

            for(int i = 0; i < tickets.Count();i++)
            {
                orderService.MakeOrder(people[i].PersonId, tickets[i].TicketId);
            }

            CreatePdf(ticketViewModels);

            return View("GetTickets", ticketViewModels);
        }

       private void CreatePdf(List<TicketViewModel> ticketInfo)
        {
            pdfCreator.CreatePdf(ticketInfo);
        }

        public FileResult Download(int id)
        {
            var fileName = String.Format("ticket{0}.pdf", id);
            var filepath = String.Format(HostingEnvironment.MapPath("/App_Data/ticket{0}.pdf"), id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}