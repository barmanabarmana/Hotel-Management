using AutoMapper;
using BLL.Interfaces;
using UnitsOfWork.Interfaces;
using Entities;
using BLL.Ninject;
using DTO;

namespace BLL.Services
{
    public class TourService : ITourService
    {
        IUnitOfWork UoW;

        public TourService(IUnitOfWork UoW)
        {
            TourLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper TourLogicMapper;

        public TourService()
        {
            TourLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
            }).CreateMapper();
            UoW = DependencyResolver.ResolveUoW();
        }

        public void AddTour(TourDTO NewTour)
        {
            UoW.ToursTemplates.Add(TourLogicMapper.Map<TourDTO, Tour>(NewTour));
        }

        public void DeleteTour(int Id)
        {
            UoW.ToursTemplates.Delete(Id);
        }

        public void EditTour(int Id, TourDTO Tour)
        {
            Tour tour = UoW.ToursTemplates.Get(Id);
            tour = TourLogicMapper.Map<TourDTO, Tour>(Tour);
            UoW.ToursTemplates.Modify(Id, tour);
        }

        public IEnumerable<TourDTO> GetAllToursTemplates()
        {
            return TourLogicMapper
                .Map<IEnumerable<Tour>, List<TourDTO>>(UoW
                .ToursTemplates
                .GetAll());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByPrice(IEnumerable<TourDTO> tours, int MinPrice, int MaxPrice)
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
                t.Type == SeachElem || 
                t.City == SeachElem ||
                t.Country == SeachElem ||
                t.Name == SeachElem);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByCity(IEnumerable<TourDTO> tours, string City)
        {
            return tours
                .Where(t =>
                t.City == City);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByCountry(IEnumerable<TourDTO> tours, string Country)
        {
            return tours
                .Where(t => 
                t.Country == Country);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByDuration(IEnumerable<TourDTO> tours, int MinDuration, int MaxDuration)
        {
            return tours
                .Where(t => 
                t.Duration >= MinDuration && 
                t.Duration <= MaxDuration);
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByPrice(IEnumerable<TourDTO> tours = null)
        {
            if(tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t => 
                    t.Price);
            }
            else
            {
                return tours
                    .OrderBy(t => 
                    t.Price);
            }
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByDuration(IEnumerable<TourDTO> tours = null)
        {
            if (tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t =>
                    t.Duration);
            }
            else
            {
                return tours
                    .OrderBy(t =>
                    t.Duration);
            }
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByCountry(IEnumerable<TourDTO> tours = null)
        {
            if (tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t =>
                    t.Country);
            }
            else
            {
                return tours
                    .OrderBy(t =>
                    t.Country);
            }
        }

        public TourDTO GetTour(int Id)
        {
            return TourLogicMapper
                .Map<Tour, TourDTO>(UoW
                .ToursTemplates
                .Get(Id));
        }
    }
}
