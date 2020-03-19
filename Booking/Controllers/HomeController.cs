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
        private readonly ICityService cityService;
        private InfoViewModel InfoItems { get; set; }
        private List<CarInfoViewModel> CarInfoViewModels { get; set; }
        public HomeController(ITrainSearchService<Info, SearchTrainsModel> trainSearchService, IMapperControl mapperControl, ICityService cityService)
        {
            this.trainSearchService = trainSearchService;
            this.mapperControl = mapperControl;
            this.cityService = cityService;
            InfoItems = new InfoViewModel();
            CarInfoViewModels = new List<CarInfoViewModel>();
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

        public ActionResult CompartmentCar(int carId)
        {
            CarInfoViewModels = TempData["TrainInfo"] as List<CarInfoViewModel>;
            CarInfoViewModel model = new CarInfoViewModel();
            ViewBag.TrainInfo = CarInfoViewModels;
            string viewName = String.Empty;

            foreach (var car in CarInfoViewModels)
            {
                if(car.CarriageId == carId)
                {
                    model = car;
                    CheckCarType(model.Name, ref viewName);
                    break;
                }
            }
            return PartialView(viewName, model);
        }

        public void CheckCarType(string carType, ref string viewName)
        {
            switch (carType)
            {
                case "Плацкарт":
                    {
                        viewName = "SeatPostCar";
                        break;
                    }
                case "Купе":
                    {
                        viewName = "CompartmentCar";
                        break;
                    }
            }
        }
        public ActionResult Details(int trainId, string carType)
        {
            ViewBag.CarType = carType;
         
            InfoItems.RouteInfo = TempData["InfoItems"] as List<RouteInfoViewModel>;
            ViewBag.InfoItems = InfoItems.RouteInfo;

            foreach(var route in InfoItems.RouteInfo)
            {
                if (route.TrainId == trainId)
                {
                    InfoItems.TrainInfo = route;
                    var carriages = trainSearchService.SearchCarriages(trainId);
                    InfoItems.TrainInfo.CarInfoViewModels = mapperControl.GetCarModelByCar(carriages);
                    break;
                }
            }
            TempData["TrainInfo"] = InfoItems.TrainInfo.CarInfoViewModels;
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


        public JsonResult AutoСompleteSearch(string term)
        {
            var cities = cityService.GetCities().ToList()
                .Where(x => x.Name.Contains(term))
                .Select(x => new { value = x.Name });

            return Json(cities, JsonRequestBehavior.AllowGet);
        }
    }
}