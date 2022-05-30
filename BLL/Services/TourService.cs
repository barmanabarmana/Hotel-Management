using BLL.Interfaces;
using BLL.Ninject;
using DTO;
using Entities;
using UnitsOfWork.Interfaces;

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

        public async Task AddTour(TourDTO NewTour)
        {
            await UoW.ToursTemplates.AddAsync(Tools.Mapper.Map<Tour>(NewTour));
        }

        public async Task DeleteTour(int Id)
        {
            await UoW.ToursTemplates.DeleteAsync(Id);
        }

        public async Task UpdateTour(int Id, TourDTO Tour)
        {
            await UoW.ToursTemplates.ModifyAsync(Id, Tools.Mapper.Map<Tour>(Tour));
        }

        public async Task<IEnumerable<TourDTO>> GetAllToursTemplates()
        {
            return Tools.Mapper
                .Map<IEnumerable<TourDTO>>(await UoW
                .ToursTemplates
                .GetAllAsync());
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
                t.TransportIn.DeparturePoint == DeparturePoint);
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
                t.TransportIn.DepartureTime);
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
            if (!ListNotNullAndContainsAnyElement(tours))
            {
                return 0;
            }
            return GetToursTemplatesOrderedByDuration(tours).Last().Duration;
        }
        public IEnumerable<TourDTO> GetToursStartingOnDate(IEnumerable<TourDTO> tours, DateTime startRange, DateTime endRange)
        {
            return tours.Where(t =>
                t.TransportIn.DepartureTime.Date >= startRange &&
                t.TransportIn.DepartureTime.Date <= endRange);
        }

        public async Task<IEnumerable<TourDTO>> GetHotTourTemplatesAsync()
        {
            return Tools.Mapper
                .Map<IEnumerable<TourDTO>>(await UoW.
                ToursTemplates.GetAllAsync(t =>
                t.IsHotOffer == true));
        }
        public async Task<TourDTO> GetTourAsync(int Id)
        {
            return Tools.Mapper
                .Map<TourDTO>(await UoW
                .ToursTemplates
                .GetAsync(Id));
        }
        private bool ListNotNullAndContainsAnyElement(IEnumerable<object> list)
        {
            if (list != null && list.Any())
            {
                return true;
            }
            return false;
        }
    }
}
