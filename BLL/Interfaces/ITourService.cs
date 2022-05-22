using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface ITourService
    {
        void AddTour(TourDTO NewTour);
        void EditTour(int Id, TourDTO Tour);
        IEnumerable<TourDTO> GetAllToursTemplates();
        TourDTO GetTour(int Id);
        void DeleteTour(int Id);
        IEnumerable<TourDTO> FindTourTemplatesByPrice(IEnumerable<TourDTO> tours, decimal MinPrice, decimal MaxPrice);
        IEnumerable<TourDTO> FindTourTemplates(IEnumerable<TourDTO> tours, string SeachElem);
        IEnumerable<TourDTO> FindTourTemplatesByType(IEnumerable<TourDTO> tours, string type);
        IEnumerable<TourDTO> FindTourTemplatesByDuration(IEnumerable<TourDTO> tours, int MinDuration, int MaxDuration);
        IEnumerable<TourDTO> FindTourTemplatesByCity(IEnumerable<TourDTO> tours, string City);
        IEnumerable<TourDTO> FindTourTemplatesByCountry(IEnumerable<TourDTO> tours, string Country);
        IEnumerable<TourDTO> GetAllToursTemplatesOrderedByPrice(IEnumerable<TourDTO> tours = null);
        IEnumerable<TourDTO> GetAllToursTemplatesOrderedByDuration(IEnumerable<TourDTO> tours = null);
        IEnumerable<TourDTO> GetAllToursTemplatesOrderedByCountry(IEnumerable<TourDTO> tours = null);
        IEnumerable<TourDTO> GetAllHotTourTemplates(IEnumerable<TourDTO> tours = null);
    }
}
