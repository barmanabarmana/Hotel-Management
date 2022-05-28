using Entities;
using Entities.Files;
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
        IRepository<Image> Images { get; }
        IRepository<Hotel> Hotels { get; }
        IRepository<HotelRoom> HotelsRooms { get; }
        IRepository<Tour> ToursTemplates { get; }
        IRepository<Transport> Transports { get; }
        IRepository<TransportPlace> TransportPlaces { get; }
        IRepository<Customer> Customers { get; }
        IRepository<HotelRoomReservation> HotelsRoomsReservations { get; }
        IRepository<Bill> Bills { get; }

        void DeleteDB();
    }
}
