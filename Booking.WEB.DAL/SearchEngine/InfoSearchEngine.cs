using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Booking.DAL.SearchEngine
{
    public class InfoSearchEngine: ISearchEngine<Info, SearchTrainsModel>
    {
        private IBookingContext _context;

        public Info Info { get; set; }
        public InfoSearchEngine(IBookingContext context)
        {
            _context = context;
            Info = new Info();
        }

        public Info SearchItems(SearchTrainsModel model)
        {
            string proc = "GetTravelDescription";

            using (IDbConnection conn = _context.GetConnection())
            {
                using (var multi = conn.QueryMultiple(proc, new { @DepartureDate = model.DepartureDate, @FromStation = model.FromStation, @ToStation = model.ToStation }, commandType: CommandType.StoredProcedure))
                {
                    Info.RouteInfo = multi.Read<RouteInfo>().AsList();
       
                    var carriagefree = multi.Read<CarriageFreeSeatsInfo>().AsList();

                    foreach(var route in Info.RouteInfo)
                    {

                        foreach(var car in carriagefree.FindAll(x => x.TrainId.Equals(route.TrainId)))
                        {
                            route.CarriageFreeSeatsInfos.Add(car);
                        }
                    }
                   
                }
            }
            return Info;
        }

        public List<CarriageInfo> SearchCarriages(int trainId)
        {
            string proc = "GetTrainInfo";

            List<CarriageInfo> info = new List<CarriageInfo>();
            using (IDbConnection conn = _context.GetConnection())
            {
                using(var multi = conn.QueryMultiple(proc, new { @TrainId = trainId}, commandType: CommandType.StoredProcedure))
                {
                    var carriageInfo = multi.Read<CarriageInfo>().AsList();
                    var resersedSeats = multi.Read<ReservedSeat>().AsList();

                    foreach(var car in carriageInfo)
                    {
                        foreach(var seat in resersedSeats.FindAll(x => x.CarriageId.Equals(car.CarriageId)))
                        {
                            car.ReservedSeats.Add(seat);
                        }
                        info.Add(car);
                    }
                }
            }
            return info;
        }
    }
}
