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
        IEnumerable<TransportDTO> GetAllTransport();
        TransportDTO GetTransport(int Id);
        void DeleteTransport(int Id);
        void ApplyNewPriceForTicketAndUpdateTransport(TransportDTO tour, decimal PriceForTicket);
    }
}
