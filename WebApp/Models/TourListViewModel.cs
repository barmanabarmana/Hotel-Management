using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace WebApp.Models
{
    public class TourListVM
    {
        public IEnumerable<TourModel>? TourList { get; set; }
        public SelectList Type { get; set; }
        public string TourType { get; set; }
        public SelectList Country { get; set; }
        public string TourCountry { get; set; }
        public SelectList City { get; set; }
        public string TourCity { get; set; }
        public string SearchString { get; set; }
    }
}
