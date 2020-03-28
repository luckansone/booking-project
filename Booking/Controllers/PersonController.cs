using Booking.Interfaces.Mapping;
using Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMapperControl mapperControl;
        private List<PersonViewModel> personViewModels;
        public PersonController( IMapperControl mapperControl)
        {
            this.mapperControl = mapperControl;
            personViewModels = new List<PersonViewModel>();
        }

        [HttpGet]
        public ActionResult PeopleForms()
        {
            RouteInfoViewModel model = Session["RouteInfo"] as RouteInfoViewModel;
            for(int i = 0; i < model.SelectedCarriage.ChosenSeats.Count; i++)
            {
                personViewModels.Add(new PersonViewModel());
            }
            return View(personViewModels.ToList());
        }

        [HttpPost]
        public ActionResult PeopleForms(List<PersonViewModel> model)
        {
            return View();
        }
    }
}