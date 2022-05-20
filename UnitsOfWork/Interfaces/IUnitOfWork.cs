using Entities;
using Entities.Hotels;
using Entities.Transports;
using Entities.Users;
using Repositories.Generic;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitsOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<HotelRoom> HotelsRooms { get; }
        IRepository<Tour> ToursTemplates { get; }
        IRepository<Transport> Transports { get; }

        IRepository<Customer> Customers { get; }
        IRepository<Tour> OrderedTours { get; }
        IRepository<HotelRoomReservation> HotelsRoomsReservations { get; }
        IRepository<TransportPlace> TransportsPlace { get; }

        void DeleteDB();
    }
}
