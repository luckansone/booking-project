using Booking.DAL;
using Booking.DAL.Models;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using Booking.WEB.DAL.Models;
using System;
using System.Collections.Generic;

namespace Booking.BL.Services
{
    public class TrainSearchService: ITrainSearchService<Info, SearchTrainsModel>
    {

        private readonly IUnitOfWork unitOfWork;

    
        public TrainSearchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

       
        public Info SearchTrains(SearchTrainsModel model)
        {
            return unitOfWork.infoRepository.SearchItems(model);
        }

        public List<CarriageInfo> SearchCarriages(int trainId)
        {
            return unitOfWork.infoRepository.SearchCarriages(trainId);
        }
    }
}
