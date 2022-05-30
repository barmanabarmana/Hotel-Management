using BLL.Interfaces;
using BLL.Ninject;
using DTO;
using DTO.Transports;
using Entities.Transports;
using UnitsOfWork.Interfaces;

namespace BLL.Services
{
    public class TransportService : ITransportService
    {
        IUnitOfWork UoW;

        public TransportService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public TransportService()
        {
            UoW = DependencyResolver.ResolveUoW();
        }

        public TourDTO AddTransportToTour(TourDTO tour,
            TransportDTO transportIn,
            int AvailibleSeatsIn,
            decimal PriceForTcketIn,
            TransportDTO transportOut,
            int AvailibleSeatsOut,
            decimal PriceForTcketOut)
        {
            tour.TransportIn = BuildTransport(transportIn, AvailibleSeatsIn, PriceForTcketIn);
            tour.TransportOut = BuildTransport(transportOut, AvailibleSeatsOut, PriceForTcketOut);
            return tour;
        }
        public async Task ApplyNewPriceForTicketAndUpdateTransportAsync(TransportDTO Transport,
            decimal PriceForTicket)
        {
            TransportDTO transport = Tools.Mapper
                .Map<Transport, TransportDTO>(await UoW.Transports.GetAsync(Transport.Id));

            if (transport.TransportPlaces != null && transport.TransportPlaces.Count != 0 &&
                transport.TransportPlaces.FirstOrDefault().Price != PriceForTicket)
            {
                await ApplyTicketPriceForEachPlaceInTransportAsync(
                      transport, PriceForTicket);
            }

            await UoW.Transports.ModifyAsync(transport.Id,
                Tools.Mapper.Map<TransportDTO, Transport>(Transport));
        }
        public async Task DeleteTransportAsync(int Id)
        {
            await UoW.Transports
                .DeleteAsync(Id);
        }
        public async Task<IEnumerable<TransportDTO>> GetAllTransportAsync()
        {
            return Tools.Mapper
                .Map<IEnumerable<Transport>, List<TransportDTO>>(await UoW.
                Transports.GetAllAsync());
        }

        public async Task<TransportDTO> GetTransportAsync(int Id)
        {
            return Tools.Mapper
                .Map<Transport, TransportDTO>(await UoW.
                Transports.GetAsync(Id));
        }

        private TransportDTO BuildTransport(TransportDTO transport,
            int availibleSeats,
            decimal Price)
        {
            for (int i = 1; i <= availibleSeats; i++)
            {
                transport.TransportPlaces.Add(new TransportPlaceDTO(transport.Id, i, Price));
            }
            return transport;
        }
        private async Task ApplyTicketPriceForEachPlaceInTransportAsync(TransportDTO transport, decimal newPrice)
        {
            foreach (var place in transport.TransportPlaces)
            {
                place.Price = newPrice;
                await UoW.TransportPlaces.ModifyAsync(place.Id, Tools.Mapper
                    .Map<TransportPlaceDTO, TransportPlace>(place));
            }
        }
    }
}
