using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.DAL.Interfaces
{
    public interface ISearchEngine<T, M>
    {
        T SearchItems(M model);
    }
}
