using Booking.DAL.Models;
using Booking.ViewModels;
using Booking.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Interfaces.Mapping
{
    public interface IMapperControl
    {
        SearchTrainsModel GetSearchModelByModelView(SearchTrainsViewModel model);
        InfoViewModel GetInfoViewModelByInfo(Info model);
    }
}
