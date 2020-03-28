using Booking.WEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Booking.WEB.DAL.Calculator
{
    public class Calculator: ICalculator
    {
        private IBookingContext _context;
        public Calculator(IBookingContext context)
        {
            _context = context;
        }

        public double GetDistance(int RouteId)
        {
            string funcscript = String.Format("SELECT dbo.CalculateDistance({0})", RouteId);
            double result=0;

            using (IDbConnection conn = _context.GetConnection())
            {
                result = conn.Query<double>(funcscript, new { }).First();
            }
            
            return result;
        }

        public double GetTrainSpeed(int TrainId)
        {
            string funcscript = String.Format(@"SELECT trtype.Speed FROM Train AS tr
                                                  INNER JOIN TrainType AS trtype
                                                  ON tr.TrainTypeId = trtype.TrainTypeId
                                                  WHERE TrainId = {0}", TrainId);
            double result = 0;

            using (IDbConnection conn = _context.GetConnection())
            {
                result = conn.Query<double>(funcscript, new { }).First();
            }

            return result;
        }
    }
}
