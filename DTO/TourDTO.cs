using DTO.Hotels;
using DTO.Transports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TourDTO
    {
        public TourDTO() { }
        public TourDTO(string Title, 
            decimal Price, 
            string Type,
            string Country, 
            string City, 
            int Duration, 
            string Description, 
            bool isHotOffer)
        {
            this.Title = Title;
            this.Price = Price;
            this.Type = Type;
            this.Country = Country;
            this.City = City;
            this.Duration = Duration;
            this.Description = Description;
            IsHotOffer = isHotOffer;
         }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Duration { get; set; }
        public bool IsHotOffer { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public HotelDTO Hotel { get; set; }
        public TransportDTO TransportIn { get; set; }
        public TransportDTO TransportOut { get; set; }
    }
}
