using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Hotels;
using Logic.DTOs;

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
