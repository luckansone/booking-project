using Booking.DAL.Interfaces;
using Booking.DAL.Models;
using Booking.WEB.DAL.Interfaces;
using Dapper;
using System.Data;

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
                    var carriages = multi.Read<CarriageInfo>().AsList();

                    foreach(var route in Info.RouteInfo)
                    {
                        foreach(var car in carriages.FindAll(x => x.TrainId.Equals(route.TrainId)))
                        {
                            route.CarriageInfos.Add(car);
                        }
                    }
                   
                }
            }
            return Info;
        }
    }
}
