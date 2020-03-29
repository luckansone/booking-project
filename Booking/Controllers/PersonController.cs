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

        List<TicketViewModel> ticketViewModels;
        public PersonController( IMapperControl mapperControl, IPersonService personService,
            IReservedSeatService reservedSeatService, ITicketService ticketService,
            IOrderService orderService)
        {
            this.mapperControl = mapperControl;
            this.personService = personService;
            this.reservedSeatService = reservedSeatService;
            this.ticketService = ticketService;
            this.orderService = orderService;
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
                    SNP = String.Format("{0} {1} {2}", people[0].Name, people[0].Surname, people[0].Patronymic)
                });
            }
            var tickets = mapperControl.GetTicketsByViewModel(ticketViewModels);
            tickets = ticketService.Create(tickets);

            for(int i = 0; i < tickets.Count();i++)
            {
                orderService.MakeOrder(people[0].PersonId, tickets[0].TicketId);
            }

            Session["TicketsInfo"] = ticketViewModels;

            return View("GetTickets", ticketViewModels);
        }

        public void CreatePdf()
        {
            List<TicketViewModel> ticketInfo = Session["TicketsInfo"] as List<TicketViewModel>;
            PdfWriter writer = new PdfWriter(@"C:\Users\Helen Kravchuk\source\repos\Booking.DAL\Booking\App_Data\ticket.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
          
            document.Add(new Paragraph("Ticket").SetFont(font));
            List list = new List().SetSymbolIndent(12).SetListSymbol("\u2022").SetFont(font);
            document.Add(list);
            document.Close();
        }
    }
}