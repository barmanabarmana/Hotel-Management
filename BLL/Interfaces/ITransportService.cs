using DTO.Transports;

namespace BLL.Interfaces
{
    public interface ITransportService
    {
        void AddTransport(TransportDTO NewTransport, int AvailibleSeats, int PriceForTicket);
        IEnumerable<TransportDTO> GetAllTransport();
        TransportDTO GetTransport(int Id);
        void DeleteTransport(int Id);
    }
}
