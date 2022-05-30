using Models;
using Models.Hotels;
using Models.Transports;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class TourVM
    {
        public TourModel Tour { get; set; }

        [Display(Name = "Tour price")]
        public string? TourPrice { get; set; }

        public HotelModel Hotel { get; set; }

        public List<HotelRoomModel> Rooms { get; set; }

        [Display(Name = "New room")]
        public HotelRoomModel NewRoom { get; set; }

        [Display(Name = "Image")]
        public IFormFile UploadedFile { get; set; }

        [Display(Name = "Transport in")]
        public TransportModel TransportIn { get; set; }

        [Display(Name = "Transport out")]
        public TransportModel TransportOut { get; set; }

        [Display(Name = "Aviable seats")]
        public int AvailibleSeatsIn { get; set; }

        [Display(Name = "Price for ticket")]
        public string? PriceForTicketIn { get; set; }

        [Display(Name = "Aviable seats")]
        public int AvailibleSeatsOut { get; set; }

        [Display(Name = "Price for ticket")]
        public string? PriceForTicketOut { get; set; }
    }
}
