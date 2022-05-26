using AutoMapper;
using BLL.Ninject;
using UnitsOfWork.Interfaces;
using BLL.Interfaces;
using Entities.Transports;
using DTO.Transports;
using DTO.User;
using Entities.Users;
using DTO;
using Entities;
using DTO.Hotels;
using Entities.Hotels;
using DTO.Files;
using Entities.Files;

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

        private IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Transport, TransportDTO>();
            cfg.CreateMap<TransportDTO, Transport>();
            cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
            cfg.CreateMap<Hotel, HotelDTO>();
            cfg.CreateMap<HotelDTO, Hotel>();
            cfg.CreateMap<ImageDTO, Image>();
            cfg.CreateMap<Image, ImageDTO>();
            cfg.CreateMap<TourDTO, Tour>();
            cfg.CreateMap<Tour, TourDTO>();
            cfg.CreateMap<HotelRoomDTO, HotelRoom>();
            cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
            cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();
        }).CreateMapper();

        public TourDTO AddTransportToTour(TourDTO tour, 
            TransportDTO transportIn, 
            int AvailibleSeatsIn,
            decimal PriceForTcketIn,
            TransportDTO transportOut,
            int AvailibleSeatsOut,
            decimal PriceForTcketOut)
        {
            tour = InsertTransportIntoTour(tour, transportIn, AvailibleSeatsIn, PriceForTcketIn);
            tour = InsertTransportIntoTour(tour, transportOut, AvailibleSeatsOut, PriceForTcketOut);
            return tour;
        }
        public void ApplyNewPriceForTicketAndUpdateTransport(TransportDTO Transport,
            decimal PriceForTicket)
        {
            TransportDTO transport = Mapper
                .Map<Transport, TransportDTO>(UoW.Transports.Get(Transport.Id));

            if (transport.TransportPlaces != null && transport.TransportPlaces.Count != 0 &&
                transport.TransportPlaces.FirstOrDefault().Price != PriceForTicket)
            {
                ApplyTicketPriceForEachPlaceInTransport(
                      transport, PriceForTicket);
            }

            UoW.Transports.Modify(transport.Id,
                Mapper.Map<TransportDTO, Transport>(Transport));
        }
        public void DeleteTransport(int Id)
        {
            UoW.Transports
                .Delete(Id);
        }
        public IEnumerable<TransportDTO> GetAllTransport()
        {
            return Mapper
                .Map<IEnumerable<Transport>, List<TransportDTO>>(UoW.
                Transports.GetAll(t => 
                t.TransportPlaces));
        }

        public TransportDTO GetTransport(int Id)
        {
            return Mapper.
                Map<Transport, TransportDTO>(UoW.
                Transports
                .GetAll(t =>
                t.Id == Id, 
                t =>
                t.TransportPlaces)
                .FirstOrDefault());
        }

        private TourDTO InsertTransportIntoTour(TourDTO tour, 
            TransportDTO transport, int availibleSeats, decimal Price)
        {
            for(int i = 1; i <= availibleSeats; i++)
            {
                transport.TransportPlaces.Add(new TransportPlaceDTO(transport.Id, i, Price));
            }
            tour.Transports.Add(transport);
            return tour;
        }
        private void ApplyTicketPriceForEachPlaceInTransport(TransportDTO transport, decimal newPrice)
        {
            foreach(var place in transport.TransportPlaces)
            {
                place.Price = newPrice;
                UoW.TransportPlaces.Modify(place.Id, Mapper
                    .Map<TransportPlaceDTO, TransportPlace>(place));
            }
        }
    }
}
