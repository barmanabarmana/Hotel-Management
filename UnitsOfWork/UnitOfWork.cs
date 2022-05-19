using DAL;
using Entities;
using Entities.Users;
using Repositories.Generic;
using UnitsOfWork.Interfaces;

namespace UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context = new();
        private GenericRepository<Hotel> _hotelRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Booking> _bookingRepository;

        public GenericRepository<Hotel> HotelRepository
        {
            get
            {

                if (_hotelRepository == null)
                {
                    _hotelRepository = new GenericRepository<Hotel>(_context);
                }
                return _hotelRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (_customerRepository == null)
                {
                    _customerRepository = new GenericRepository<Customer>(_context);
                }
                return _customerRepository;
            }
        }
        public GenericRepository<Booking> BookingRepository
        {
            get
            {

                if (_bookingRepository == null)
                {
                    _bookingRepository = new GenericRepository<Booking>(_context);
                }
                return _bookingRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
