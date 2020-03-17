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
        private InfoViewModel InfoItems { get; set; }
        public HomeController(ITrainSearchService<Info, SearchTrainsModel> trainSearchService, IMapperControl mapperControl)
        {
            this.trainSearchService = trainSearchService;
            this.mapperControl = mapperControl;
            InfoItems = new InfoViewModel();
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


        public ActionResult Details(int trainId, string carType)
        {
            ViewBag.CarType = carType;
         
            InfoItems.RouteInfo = TempData["InfoItems"] as List<RouteInfoViewModel>;
           
            foreach(var route in InfoItems.RouteInfo)
            {
                if (route.TrainId == trainId)
                {
                    InfoItems.TrainInfo = route;
                    var carriages = trainSearchService.SearchCarriages(trainId);
                    InfoItems.TrainInfo.CarInfoViewModels = mapperControl.GetCarModelByCar(carriages);
                }
            }

            return View(InfoItems.TrainInfo);
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
            InfoItems = mapperControl.GetInfoViewModelByInfo(Info);
            TempData["InfoItems"] = InfoItems.RouteInfo;
            return PartialView(InfoItems);
        }
    }
}