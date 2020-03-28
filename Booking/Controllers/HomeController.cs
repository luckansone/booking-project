using Booking.DAL.Models;
using Booking.Interfaces.Mapping;
using Booking.ViewModels;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrainSearchService<Info, SearchTrainsModel> trainSearchService;
        private readonly IMapperControl mapperControl;
        private readonly ICityService cityService;
        private readonly IPriceCalculateService priceCalculateService;
        private InfoViewModel InfoItems { get; set; }
        private List<CarInfoViewModel> CarInfoViewModels { get; set; }
        private RouteInfoViewModel RouteInfo { get; set; }
        public HomeController(ITrainSearchService<Info, SearchTrainsModel> trainSearchService, IMapperControl mapperControl,
            ICityService cityService, IPriceCalculateService priceCalculateService)
        {
            this.trainSearchService = trainSearchService;
            this.mapperControl = mapperControl;
            this.cityService = cityService;
            this.priceCalculateService = priceCalculateService;
            InfoItems = new InfoViewModel();
            CarInfoViewModels = new List<CarInfoViewModel>();
            RouteInfo = new RouteInfoViewModel();
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

        public ActionResult CarInfo(int carId)
        {
            CarInfoViewModels = Session["TrainInfo"] as List<CarInfoViewModel>;
            CarInfoViewModel model = new CarInfoViewModel();
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

    
        public ActionResult Details(int trainId, string carType)
        {
            ViewBag.CarType = carType;
         
            InfoItems.RouteInfo = Session["InfoItems"] as List<RouteInfoViewModel>;

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

            Session["RouteInfo"] = InfoItems.TrainInfo;
            Session["TrainInfo"] = InfoItems.TrainInfo.CarInfoViewModels;
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
            Session["InfoItems"] = InfoItems.RouteInfo;
            return PartialView(InfoItems);
        }


        public JsonResult AutoСompleteSearch(string term)
        {
            var cities = cityService.GetCities().ToList()
                .Where(x => x.Name.Contains(term))
                .Select(x => new { value = x.Name });

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChooseSeats(int [] selectedSeats, CarInfoViewModel model)
        {
            if (selectedSeats == null)
            {
                selectedSeats = new int[0];
            }

            RouteInfo = Session["RouteInfo"] as RouteInfoViewModel;
            RouteInfo.SelectedCarriage = model;

            var routeModel = mapperControl.GetRouteInfoByRouteInfoViewModel(RouteInfo);
            double price = priceCalculateService.GetPrice(routeModel);

            RouteInfo.SelectedCarriage.ChosenSeats = new List<SelectedSeat>();

            for (int i = 0; i < selectedSeats.Length; i++)
            {
                RouteInfo.SelectedCarriage.ChosenSeats.Add(new SelectedSeat { Number = selectedSeats[i], Price = price });
            }

            Session["RouteInfo"] = RouteInfo;

            return PartialView(model);
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
                case "Люкс":
                    {
                        viewName = "LuxuryCar";
                        break;
                    }
                case "Сидячий":
                    {
                        viewName = "SittingCar";
                        break;
                    }
            }
        }
    }
}