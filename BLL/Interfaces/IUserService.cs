using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void AddUser(CustomerDTO Customer);
        IEnumerable<CustomerDTO> GetAllUsers();
        CustomerDTO GetUser(int Id);
        void DeleteUser(int Id);
        void EditUser(int Id, CustomerDTO Customer);
        void ReserveTour(int CustomerId, int TourId);
        void ReserveRoom(int CustomerId, int HotelId, int HotelRoomId, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate);
        void ReserveTicket(int CustomerId, int TransportId, int SeatNumber);
    }
}
