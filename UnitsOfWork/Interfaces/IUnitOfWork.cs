using Entities;
using Entities.Users;
using Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitsOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Hotel> HotelRepository { get; }
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Booking> BookingRepository { get; }
    }
}
