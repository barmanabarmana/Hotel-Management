using AutoMapper;
using BLL.Ninject;
using UnitsOfWork.Interfaces;
using BLL.Interfaces;
using Entities.Transports;
using DTO.Transports;

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

        IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransportDTO, Transport>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
            cfg.CreateMap<Transport, TransportDTO>();
            cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
        }).CreateMapper();

        public void AddTransport(TransportDTO NewTransport, int AvailibleSeats, int PriceForTicket)
        {
            for (int i = 1; i <= AvailibleSeats; i++)
                NewTransport.TransportPlaces.Add(new TransportPlaceDTO(NewTransport, i, PriceForTicket));

            UoW.Transports
                .Add(Mapper
                .Map<TransportDTO, Transport>(NewTransport));
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
    }
}
