using DAL;
using Entities;
using Entities.Hotels;
using Entities.Transports;
using Entities.Users;
using Repositories.Generic;
using Repositories.Interface;
using UnitsOfWork.Interfaces;

namespace UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagementDbContext ManagementContext;
        private UsageDbContext UsageContext;

        public UnitOfWork()
        {
            ManagementContext = new ManagementDbContext();
            UsageContext = new UsageDbContext();
        }

        public UnitOfWork(ManagementDbContext ManagementContext, UsageDbContext UsageContext)
        {
            this.ManagementContext = ManagementContext;
            this.UsageContext = UsageContext;
        }


        private IRepository<Hotel> _hotels;
        private IRepository<HotelRoom> _hotelsrooms;
        private IRepository<Tour> _tourstemplates;
        private IRepository<Transport> _transports;

        private IRepository<TransportPlace> _transportsplases;
        private IRepository<Customer> _customers;
        private IRepository<Tour> _orderedtours;
        private IRepository<HotelRoomReservation> _hotelsroomsreservations;

        public IRepository<Hotel> Hotels 
        { 
            get 
            { 
                if (_hotels == null) 
                    _hotels = new GenericRepository<Hotel>(ManagementContext); 
                return _hotels; 
            } 
        }

        public IRepository<HotelRoom> HotelsRooms
        { 
            get
            { 
                if (_hotelsrooms == null)
                    _hotelsrooms = new GenericRepository<HotelRoom>(ManagementContext); 
                return _hotelsrooms; 
            } 
        }
        public IRepository<Tour> ToursTemplates 
        {
            get
            {
                if (_tourstemplates == null)
                    _tourstemplates = new GenericRepository<Tour>(ManagementContext); 
                return _tourstemplates;
            } 
        }
        public IRepository<Transport> Transports 
        { 
            get 
            { 
                if (_transports == null) 
                    _transports = new GenericRepository<Transport>(ManagementContext);
                return _transports;
            } 
        }

        public IRepository<Customer> Customers 
        { 
            get 
            { 
                if (_customers == null) 
                    _customers = new GenericRepository<Customer>(UsageContext); 
                return _customers;
            } 
        }
        public IRepository<Tour> OrderedTours 
        { 
            get 
            {
                if (_orderedtours == null) 
                    _orderedtours = new GenericRepository<Tour>(UsageContext); 
                return _orderedtours;
            } 
        }
        public IRepository<HotelRoomReservation> HotelsRoomsReservations 
        { 
            get
            { 
                if (_hotelsroomsreservations == null) 
                    _hotelsroomsreservations = new GenericRepository<HotelRoomReservation>(UsageContext);
                return _hotelsroomsreservations;
            } 
        }
        public IRepository<TransportPlace> TransportsPlace { 
            get
            {
                if (_transportsplases == null) 
                    _transportsplases = new GenericRepository<TransportPlace>(UsageContext);
                return _transportsplases; 
            } 
        }

        public void DeleteDB()
        {
            ManagementContext.Database.EnsureDeleted();
            UsageContext.Database.EnsureDeleted();
            ManagementContext.Database.EnsureCreated();
            UsageContext.Database.EnsureCreated();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ManagementContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
