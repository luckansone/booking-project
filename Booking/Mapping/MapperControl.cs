using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Booking.DAL.Models;
using Booking.Interfaces.Mapping;
using Booking.ViewModels;
using Booking.WEB.DAL.Models;
using Booking.WEB.ViewModels;

namespace Booking.WEB.Mapping
{
    public class MapperControl: IMapperControl
    {
        MapperConfiguration config;
        Mapper mapper;
        
        public MapperControl()
        {

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SearchTrainsViewModel, SearchTrainsModel>()
                    .ForMember("DepartureDate", opt => opt
                    .MapFrom(c => Convert.ToDateTime(c.DateOfDeparture.ToString().Replace("0:00:00", c.TimeOfDeparture))));

                cfg.CreateMap<ReservedSeat, ReservedSeatViewModel>();
                cfg.CreateMap<CarriageInfo, CarInfoViewModel>();
                cfg.CreateMap<CarriageFreeSeatsInfo, CarFreeInfoViewModel>();
                cfg.CreateMap<RouteInfo, RouteInfoViewModel>()
                .ForMember("CarriageFreeInfos", opt => opt.MapFrom(c => c.CarriageFreeSeatsInfos))
                .ForMember("CarInfoViewModels", opt => opt.MapFrom(c => c.CarriageInfos));
                cfg.CreateMap<Info, InfoViewModel>();

                cfg.CreateMap<ReservedSeatViewModel, ReservedSeat>();
                cfg.CreateMap<CarInfoViewModel, CarriageInfo>();
                cfg.CreateMap<CarFreeInfoViewModel, CarriageFreeSeatsInfo>();
                cfg.CreateMap<RouteInfoViewModel, RouteInfo>()
                .ForMember("CarriageFreeSeatsInfos", opt => opt.MapFrom(c => c.CarriageFreeInfos))
                .ForMember("CarriageInfos", opt => opt.MapFrom(c => c.CarInfoViewModels));

                cfg.CreateMap<Person, PersonViewModel>();
                cfg.CreateMap<PersonViewModel, Person>();

                cfg.AllowNullCollections = true;
            });

            mapper = new Mapper(config);
        }

        public List<CarInfoViewModel> GetCarModelByCar(List<CarriageInfo> model)
        {
            return mapper.Map<List<CarriageInfo>, List<CarInfoViewModel>>(model);
        }
        public InfoViewModel GetInfoViewModelByInfo(Info model)
        {
            return mapper.Map<Info, InfoViewModel>(model);
        }

        public SearchTrainsModel GetSearchModelByModelView(SearchTrainsViewModel model)
        {
            return mapper.Map<SearchTrainsViewModel, SearchTrainsModel>(model);
        }

        public RouteInfo GetRouteInfoByRouteInfoViewModel(RouteInfoViewModel model)
        {
            return mapper.Map<RouteInfoViewModel, RouteInfo>(model);
        }

        public List<Person> GetPersonListByPersonViewList(List<PersonViewModel> models)
        {
            return mapper.Map<List<PersonViewModel>, List<Person>>(models);
        }

    }
}
