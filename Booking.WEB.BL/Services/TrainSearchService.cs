using Booking.DAL;
using Booking.DAL.Models;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using System;

namespace Booking.BL.Services
{
    public class TrainSearchService: ITrainSearchService<Info, SearchTrainsModel>
    {

        SearchTrainsModel model { get; set; }
        private IUnitOfWork unitOfWork { get; set; }

    

        public TrainSearchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

       
        public Info SearchTrains(SearchTrainsModel model)
        {
            return unitOfWork.infoRepository.SearchItems(model);
        }
    }
}
