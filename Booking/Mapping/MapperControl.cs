using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Booking.DAL.Models;
using Booking.Interfaces.Mapping;
using Booking.ViewModels;
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

                
                //cfg.CreateMap<Info, InfoViewModel>()
                //    .ForMember("CarInfoViewModels", opt => opt.MapFrom(c=>c.CarInfo))
                //    .ForMember("RouteInfoViewModels", opt=> opt.MapFrom(c=>c.RouteInfo));

                cfg.CreateMap<InfoViewModel, Info>();



                cfg.AllowNullCollections = true;
            });

            mapper = new Mapper(config);
        }


        public InfoViewModel GetInfoViewModelByInfo(Info model)
        {
            return mapper.Map<Info, InfoViewModel>(model);
        }

        public SearchTrainsModel GetSearchModelByModelView(SearchTrainsViewModel model)
        {
            return mapper.Map<SearchTrainsViewModel, SearchTrainsModel>(model);
        }

    }
}
