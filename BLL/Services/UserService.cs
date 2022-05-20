using AutoMapper;
using BLL.Ninject;
using BLL.Interfaces;
using UnitsOfWork.Interfaces;
using Entities.Users;
using DTO.User;
using Entities.Transports;
using DTO.Hotels;
using Entities;
using Entities.Transports;
using Exceptions;
using DTO;
using DTO.Transports;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        static UserService()
        {
            CurrentUser = null;
        }

        IUnitOfWork UoW;

        public UserService(IUnitOfWork UoW)
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerDTO, Customer>();
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();

            }).CreateMapper();
            this.UoW = UoW;
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
        }

        IMapper UserLogicMapper;
        IMapper HotelRoomToDto;
        public static CustomerDTO CurrentUser;
        public UserService()
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerDTO, Customer>();
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();
            }).CreateMapper();
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
            UoW = DependencyResolver.ResolveUoW();
        }

        public void AddUser(CustomerDTO NewUser)
        {
            UoW.Customers.Add(UserLogicMapper.Map<CustomerDTO, Customer>(NewUser));
        }

        public IEnumerable<CustomerDTO> GetAllUsers()
        {
            UoW.DeleteDB();
            return UserLogicMapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(UoW.Customers.GetAll(u => u.HotelRoomReservations, u => u.TransportTickets, u => u.Tours));
        }

        public CustomerDTO GetUser(int Id)
        {
            return UserLogicMapper.Map<Customer, CustomerDTO>(UoW.Customers.GetAll(u => u.Id == Id, u => u.HotelRoomReservations, u => u.TransportTickets, u => u.Tours).FirstOrDefault());
        }

        public void EditUser(int Id, CustomerDTO User)
        {
            UoW.Customers.Modify(Id, UserLogicMapper.Map<CustomerDTO, Customer>(User));
        }

        public void DeleteUser(int Id)
        {
            UoW.Customers.Delete(Id);
        }
        public void ReserveTour(int UserId, int TourId)
        {
            Tour tour = UoW.ToursTemplates.Get(TourId);
            Customer user = UoW.Customers.GetAll(u => u.Id == UserId, u => u.Tours).FirstOrDefault();
            user.Tours.Add(tour);
            UoW.Customers.Modify(user.Id, user);
        }

        public void ReserveRoom(int UserId, int HotelId, int HotelRoomId, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate)
        {
            Customer user = UoW.Customers.GetAll(u => u.Id == UserId, u => u.HotelRoomReservations).FirstOrDefault();
            HotelRoom hotelroom = UoW.Hotels.GetAll(h => h.Id == HotelId, h => h.Rooms).FirstOrDefault().Rooms.FirstOrDefault(r => r.Id == HotelRoomId);

            foreach (DateTimeOffset d in hotelroom.BookedDays)
            {
                DateTimeOffset FakeArrival = ArrivalDate;
                DateTimeOffset FakeDeparture = DepartureDate;
                while (FakeArrival.CompareTo(FakeDeparture) < 0)
                {
                    if (d.Date.CompareTo(FakeArrival.Date) == 0)
                        throw new AlreadyBookedItemException("Room is not availible for " + d.Day + "." + d.Month + "." + d.Year);
                    FakeArrival = FakeArrival.AddDays(1);
                }
            }
            var reserv = new HotelRoomReservation(hotelroom, user.FirstName, user.LastName, ArrivalDate.Date, DepartureDate.Date);
            while (ArrivalDate.CompareTo(DepartureDate) < 0)
            {
                hotelroom.BookedDays.Add(ArrivalDate.Date);
                ArrivalDate = ArrivalDate.AddDays(1);
            }
            UoW.HotelsRooms.Modify(hotelroom.Id, hotelroom);
            UoW.HotelsRoomsReservations.Add(reserv);
            user.HotelRoomReservations.Add(reserv);
            UoW.Customers.Modify(user.Id, user);
        }

        public void ReserveTicket(int UserId, int TransportId, int SeatNumber)
        {
            Customer user = UoW.Customers.Get(UserId);
            Transport transport = UoW.Transports.GetAll(t => t.Id == TransportId, t => t.TransportPlaces).FirstOrDefault();
            TransportPlace transportplace = transport.TransportPlaces.FirstOrDefault(p => p.Number == SeatNumber);
            if (transportplace.IsBooked)
                throw new AlreadyBookedItemException("Transport place is already booked");
            else
            {
                transportplace.IsBooked = true;
                UoW.Transports.Modify(transport.Id, transport);
                user.TransportTickets.Add(new TransportTicket(transportplace, user.FirstName, user.LastName));
                UoW.Customers.Modify(user.Id, user);
            }
        }
    }
}
