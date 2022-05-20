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
            TransportLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper TransportLogicMapper;

        public TransportService()
        {
            TransportLogicMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TransportDTO, Transport>();
                    cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                    cfg.CreateMap<Transport, TransportDTO>();
                    cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                }).CreateMapper();
            UoW = DependencyResolver.ResolveUoW();
        }

        public void AddTransport(TransportDTO NewTransport, int AvailibleSeats, int PriceForTicket)
        {
            for (int i = 1; i <= AvailibleSeats; i++)
                NewTransport.TransportPlaces.Add(new TransportPlaceDTO(NewTransport, i, PriceForTicket));
            UoW.Transports.Add(TransportLogicMapper.Map<TransportDTO, Transport>(NewTransport));
        }

        public void DeleteTransport(int Id)
        {
            UoW.Transports.Delete(Id);
        }

        public IEnumerable<TransportDTO> GetAllTransport()
        {
            return TransportLogicMapper.Map<IEnumerable<Transport>, List<TransportDTO>>(UoW.Transports.GetAll(t => t.TransportPlaces));
        }

        public TransportDTO GetTransport(int Id)
        {
            return TransportLogicMapper.Map<Transport, TransportDTO>(UoW.Transports.GetAll(t => t.Id == Id, t => t.TransportPlaces).FirstOrDefault());
        }
    }
}
