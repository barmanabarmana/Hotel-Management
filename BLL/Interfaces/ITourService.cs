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
        IEnumerable<TourDTO> GetHotTourTemplates();
        IEnumerable<TourDTO> FindTourTemplatesByDeparturePoint(IEnumerable<TourDTO> tours, string DeparturePoint = null);
        IEnumerable<TourDTO> GetTourTemplatesOrderBy(IEnumerable<TourDTO> tours, int orderby);
        decimal FindCheapestTourPrice(IEnumerable<TourDTO> tours);
        decimal FindExpensivestTourPrice(IEnumerable<TourDTO> tours);
        int FindShortestTourDuration(IEnumerable<TourDTO> tours);
        int FindLongestTourDuration(IEnumerable<TourDTO> tours);
    }
}
