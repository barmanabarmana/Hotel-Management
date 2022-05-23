using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace WebApp.Models
{
    public class TourListVM
    {
        public List<TourModel>? TourList { get; set; }
        public SelectList Types { get; set; }
        public string TourType { get; set; }
        public SelectList Arrive { get; set; }
        public string TourCountry { get; set; }
        public SelectList DeparturePoint { get; set; }
        public string TourDeparturePoint { get; set; }
        public string SearchString { get; set; }
        public int MinTourDuration { get; set; }
        public int MaxTourDuration { get; set; }
        public decimal MinTourPrice { get; set; }
        public decimal MaxTourPrice { get; set; }
        public int OrderBy { get; set; }
    }
}
