using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class TourListVM
    {
        public List<TourModel>? TourList { get; set; }
        public SelectList Types { get; set; }
        [Display(Name = "Tour type")]
        public string TourType { get; set; }
        public SelectList Arrive { get; set; }
        [Display(Name = "Tour country")]
        public string TourCountry { get; set; }
        public SelectList DeparturePoint { get; set; }
        [Display(Name = "tour departure point")]
        public string TourDeparturePoint { get; set; }
        [Display(Name = "Search string")]
        public string SearchString { get; set; }
        [Display(Name = "Minimum tour duration")]
        public int MinTourDuration { get; set; }
        [Display(Name = "Maximum tour duration")]
        public int MaxTourDuration { get; set; }
        [Display(Name = "Minimum tour price")]
        public string MinTourPrice { get; set; }
        [Display(Name = "Maximum tour price")]
        public string MaxTourPrice { get; set; }
        [Display(Name = "Order by")]
        public int OrderBy { get; set; }
    }
}
