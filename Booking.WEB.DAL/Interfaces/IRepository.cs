using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetItems();
        T GetItemById(int id);
        T Create(T item);
    }
}
