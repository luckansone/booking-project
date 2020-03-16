using Booking.DAL.Models;
using Booking.Interfaces.Mapping;
using Booking.ViewModels;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrainSearchService<Info, SearchTrainsModel> trainSearchService;
        private readonly IMapperControl mapperControl;
        public HomeController(ITrainSearchService<Info, SearchTrainsModel> trainSearchService, IMapperControl mapperControl)
        {
            this.trainSearchService = trainSearchService;
            this.mapperControl = mapperControl;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
            SearchTrainsViewModel model = new SearchTrainsViewModel();
            return View(model);
        }


        public ActionResult Details(int trainId, int routeId, string carType)
        {
            ViewBag.CarType = carType;
            var Info = trainSearchService.SearchCarriage(trainId, routeId);
            var items = mapperControl.GetInfoViewModelByInfo(Info);
            return View(items);
        }

        [HttpPost]
        public ActionResult SearchTrains(SearchTrainsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            SearchTrainsModel trainsModel = mapperControl.GetSearchModelByModelView(model);
            var Info = trainSearchService.SearchTrains(trainsModel);
            var items = mapperControl.GetInfoViewModelByInfo(Info);
            return PartialView(items);
        }
    }
}