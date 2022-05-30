using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Users;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class BookingOnlineVM
    {
        public TourModel Tour { get; set; }
        public CustomerModel CustomerWhoBook { get; set; }
        public List<CustomerModel>? AdditionalTourist { get; set; } = new();
        [Required]
        public SelectList HotelRoomNames { get; set; }
        [Required]
        [Display(Name = "Aviable rooms")]
        public string RoomName { get; set; }
        [Display(Name = "Deposit amount")]
        public string? DepositAmount { get; set; }
    }
}
