using Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ManageToursVM
    {
        public List<TourModel> Tours { get; set; }
        [Display(Name = "Search string")]
        public string SearchString { get; set; }
    }
}
