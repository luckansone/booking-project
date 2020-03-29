using Booking.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.WEB.BL.Interfaces
{
    public interface IPersonService
    {
        List<Person> CreatePerson(List<Person> item);
    }
}
