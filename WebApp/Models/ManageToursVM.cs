using Models;

namespace WebApp.Models
{
    public class ManageToursVM
    {
        public List<TourModel> Tours { get; set; }
        public string SearchString { get; set; }
    }
}
