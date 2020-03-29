using Booking.DAL.Models;
using Booking.WEB.BL.Interfaces;
using Booking.WEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Services
{
    public class PersonService: IPersonService
    {
        IUnitOfWork unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Person> CreatePerson(List<Person> item)
        {
            List<Person> people = new List<Person>();
            
            foreach(var el in item)
            {
                people.Add(unitOfWork.personRepository.Create(el));
            }

            return people;
        }
    }
}
