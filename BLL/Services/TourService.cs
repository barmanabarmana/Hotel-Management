using AutoMapper;
using BLL.Interfaces;
using UnitsOfWork.Interfaces;
using Entities;
using BLL.Ninject;
using DTO;

namespace Logic
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
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByPrice(int MinPrice, int MaxPrice)
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll(t => t.Price >= MinPrice && t.Price <= MaxPrice));
        }

        public IEnumerable<TourDTO> FindTourTemplates(string SeachElem)
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll(t => t.Type == SeachElem || t.City == SeachElem || t.Country == SeachElem || t.Name == SeachElem));
        }
        /*
        public IEnumerable<TourDTO> FindTourTemplatesByCity(string City)
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll(t => t.City == City));
        }

        public IEnumerable<TourDTO> FindTourTemplatesByCountry(string Country)
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll(t => t.Country == Country));
        }*/

        public IEnumerable<TourDTO> FindTourTemplatesByDuration(int MinDuration, int MaxDuration)
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll(t => t.Duration >= MinDuration && t.Duration <= MaxDuration));
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByPrice()
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll().OrderBy(t => t.Price));
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByDuration()
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll().OrderBy(t => t.Duration));
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByCountry()
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll().OrderBy(t => t.Country));
        }

        public TourDTO GetTour(int Id)
        {
            return TourLogicMapper.Map<Tour, TourDTO>(UoW.ToursTemplates.GetAll(t => t.Id == Id).FirstOrDefault());
        }
    }
}
