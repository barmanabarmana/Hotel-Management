using DTO;
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
        Task<CustomerDTO> GetUserAsync(int Id);
        Task DeleteUserAsync(int Id);
        Task EditUserAsync(int Id, CustomerDTO Customer);
        Task<BillDTO> BuildBillAsync(int CustomerWhoBookId,
            List<CustomerDTO> AdditionalTourists, 
            decimal DepositAmount, int TourId, 
            string HotelRoomName,
            DateTimeOffset ArrivalDate, 
            DateTimeOffset DepartureDate);
        Task<List<BillDTO>> GetBills(int CustomerId);
    }
}
