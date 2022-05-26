using Models;
using Models.Hotels;
using Models.Transports;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class TourVM
    {
        public TourModel Tour { get; set; }
        [Display(Name = "Tour Price")]
        public string? TourPrice { get; set; }
        public HotelModel Hotel { get; set; }
        public List<HotelRoomModel> Rooms { get; set; }
        [Display(Name="New Room")]
        public HotelRoomModel NewRoom { get; set; }
        public IFormFile UploadedFile { get; set; }
        public TransportModel TransportIn { get; set; }
        public TransportModel TransportOut { get; set; }
        public int AvailibleSeatsIn { get; set; }
        public string? PriceForTicketIn { get; set; }
        public int AvailibleSeatsOut { get; set; }
        public string? PriceForTicketOut { get; set; }
    }
}
