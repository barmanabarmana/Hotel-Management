using DTO;
using DTO.Transports;

namespace BLL.Interfaces
{
    public interface ITransportService
    {
        TourDTO AddTransportToTour(TourDTO tour,
            TransportDTO transportIn,
            int AvailibleSeatsIn,
            decimal PriceForTcketIn,
            TransportDTO transportOut,
            int AvailibleSeatsOut,
            decimal PriceForTcketOut);
        Task<IEnumerable<TransportDTO>> GetAllTransportAsync();
        Task<TransportDTO> GetTransportAsync(int Id);
        Task DeleteTransportAsync(int Id);
        Task ApplyNewPriceForTicketAndUpdateTransportAsync(TransportDTO tour, decimal PriceForTicket);
    }
}
