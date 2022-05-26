using AutoMapper;
using BLL.Interfaces;
using UnitsOfWork.Interfaces;
using Entities;
using BLL.Ninject;
using DTO;
using DTO.Hotels;
using Entities.Hotels;
using DTO.Transports;
using Entities.Transports;
using DTO.Files;
using Entities.Files;

namespace BLL.Services
{
    public enum OrderBy
    {
        DefaultSort,
        FromCheapToExpensive,
        FromExpensiveToCheap,
        OnStartDateTour,
        FromShortToLong,
        FromLongToShort,
    }
    public class TourService : ITourService
    {
        IUnitOfWork UoW;

        public TourService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public TourService()
        {
            UoW = DependencyResolver.ResolveUoW();
        }

        IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TourDTO, Tour>();
            cfg.CreateMap<Tour, TourDTO>();
            cfg.CreateMap<HotelDTO, Hotel>();
            cfg.CreateMap<Hotel, HotelDTO>();
            cfg.CreateMap<HotelRoomDTO, HotelRoom>();
            cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            cfg.CreateMap<TransportDTO, Transport>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
            cfg.CreateMap<Transport, TransportDTO>();
            cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            cfg.CreateMap<ImageDTO, Image>();
            cfg.CreateMap<Image, ImageDTO>();
        }).CreateMapper();

        public void AddTour(TourDTO NewTour)
        {
            UoW.ToursTemplates.Add(Mapper.Map<TourDTO, Tour>(NewTour));
        }

        public void DeleteTour(int Id)
        {
            UoW.ToursTemplates.Delete(Id);
        }

        public void UpdateTour(int Id, TourDTO Tour)
        {
            Tour tour = UoW.ToursTemplates.Get(Id);
            tour = Mapper.Map<TourDTO, Tour>(Tour);
            UoW.ToursTemplates.Modify(Id, tour);
        }

        public IEnumerable<TourDTO> GetAllToursTemplates()
        {
            return Mapper
                .Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(UoW
                .ToursTemplates
                .GetAll());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByPrice(IEnumerable<TourDTO> tours, decimal MinPrice, decimal MaxPrice)
        {
            return tours
                .Where(t =>
                t.Price >= MinPrice &&
                t.Price <= MaxPrice);
        }

        public IEnumerable<TourDTO> FindTourTemplates(IEnumerable<TourDTO> tours, string SeachElem)
        {
            return tours
            .Where(t =>
            t.City.ToUpper().Contains(SeachElem.ToUpper()) ||
            t.Country.ToUpper().Contains(SeachElem.ToUpper()) ||
            t.Title.ToUpper().Contains(SeachElem.ToUpper()));
        }
        public IEnumerable<TourDTO> FindTourTemplatesByType(IEnumerable<TourDTO> tours, string Type)
        {
            return tours
                .Where(t =>
                t.Type.ToUpper() == Type.ToUpper());
        }


        public IEnumerable<TourDTO> FindTourTemplatesByCity(IEnumerable<TourDTO> tours, string City)
        {
            return tours
                .Where(t =>
                t.City.ToUpper() == City.ToUpper());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByCountry(IEnumerable<TourDTO> tours, string Country)
        {
            return tours
                .Where(t =>
                t.Country.ToUpper() == Country.ToUpper());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByDuration(IEnumerable<TourDTO> tours, int MinDuration, int MaxDuration)
        {

            return tours
                .Where(t =>
                t.Duration >= MinDuration &&
                t.Duration <= MaxDuration);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByDeparturePoint(IEnumerable<TourDTO> tours, string DeparturePoint)
        {
            return tours
                .Where(t =>
                t.Transports.Any(t => 
                t.DeparturePoint == DeparturePoint));
        }

        public IEnumerable<TourDTO> GetTourTemplatesOrderBy(IEnumerable<TourDTO> tours, int orderby)
        {
            return orderby switch
            {
                (int)OrderBy.DefaultSort => GetToursTemplatesOrderedByPrice(tours),
                (int)OrderBy.FromCheapToExpensive => GetToursTemplatesOrderedByPrice(tours),
                (int)OrderBy.FromExpensiveToCheap => GetToursTemplatesOrderedByPrice(tours).Reverse(),
                (int)OrderBy.OnStartDateTour => GetToursTemplatesOrderedByStartDay(tours),
                (int)OrderBy.FromShortToLong => GetToursTemplatesOrderedByDuration(tours),
                (int)OrderBy.FromLongToShort => GetToursTemplatesOrderedByDuration(tours).Reverse(),
                _ => tours,
            };
        }

        private IEnumerable<TourDTO> GetToursTemplatesOrderedByStartDay(IEnumerable<TourDTO> tours)
        {
            return tours
                .OrderBy(t =>
                t.Transports[0].DepartureTime);
        }

        private IEnumerable<TourDTO> GetToursTemplatesOrderedByPrice(IEnumerable<TourDTO> tours)
        {
            return tours
                .OrderBy(t =>
                t.Price);
        }

        private IEnumerable<TourDTO> GetToursTemplatesOrderedByDuration(IEnumerable<TourDTO> tours)
        {
            return tours
                .OrderBy(t =>
                t.Duration);
        }
        public decimal FindCheapestTourPrice(IEnumerable<TourDTO> tours)
        {
            if (!ListNotNullAndContainsAnyElement(tours))
            {
                return 0;
            }
            return GetToursTemplatesOrderedByPrice(tours).First().Price;
        }
        public decimal FindExpensivestTourPrice(IEnumerable<TourDTO> tours)
        {
            if (!ListNotNullAndContainsAnyElement(tours))
            {
                return 0;
            }
            return GetToursTemplatesOrderedByPrice(tours).Last().Price;
        }
        public int FindShortestTourDuration(IEnumerable<TourDTO> tours)
        {
            if (!ListNotNullAndContainsAnyElement(tours))
            {
                return 0;
            }
            return GetToursTemplatesOrderedByDuration(tours).First().Duration;
        }
        public int FindLongestTourDuration(IEnumerable<TourDTO> tours)
        {
            if(!ListNotNullAndContainsAnyElement(tours))
            {
                return 0;
            }
            return GetToursTemplatesOrderedByDuration(tours).Last().Duration;
        }
        public IEnumerable<TourDTO> GetToursStartingOnDate(IEnumerable<TourDTO> tours, DateTime startRange, DateTime endRange)
        {
            return tours.Where(t =>
                t.Transports[0].DepartureTime.Date >= startRange &&
                t.Transports[0].DepartureTime.Date <= endRange);
        }

        public IEnumerable<TourDTO> GetHotTourTemplates()
        {
            return Mapper
                .Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(UoW.
                ToursTemplates.GetAll(t =>
                t.IsHotOffer == true));
        }
        public TourDTO GetTour(int Id)
        {
            return Mapper
                .Map<Tour, TourDTO>(UoW
                .ToursTemplates
                .Get(Id));
        }
        private bool ListNotNullAndContainsAnyElement(IEnumerable<object> list)
        {
            if(list != null && list.Any())
            {
                return true;
            }
            return false;
        }
    }
}
