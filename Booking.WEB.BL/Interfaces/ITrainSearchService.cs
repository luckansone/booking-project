﻿using Booking.DAL.Models;
using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface ITrainSearchService<T, M>
    {
        T SearchTrains(M model);
        List<CarriageInfo> SearchCarriages(int trainId);
    }
}
