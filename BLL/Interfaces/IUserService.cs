using DTO;
using DTO.User;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(CustomerDTO Customer);
        Task<IEnumerable<CustomerDTO>> GetAllUsersAsync();
        Task<CustomerDTO> GetUserAsync(int Id);
        Task DeleteUserAsync(int Id);
        Task EditUserAsync(int Id, CustomerDTO Customer);
        Task<BillDTO> BuildBillAsync(int CustomerWhoBookId,
            List<CustomerDTO> AdditionalTourists,
            decimal DepositAmount, int TourId,
            string HotelRoomName,
            DateTimeOffset ArrivalDate,
            DateTimeOffset DepartureDate);
        Task<List<BillDTO>> GetBillsAsync(int CustomerId);
        Task<BillDTO> BuildBillAsync(CustomerDTO customer, List<CustomerDTO> AdditionalTourists, decimal DepositAmount, int TourId, string HotelRoomName, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate);
    }
}
