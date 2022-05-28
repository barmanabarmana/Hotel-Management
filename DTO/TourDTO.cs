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
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Meal { get; set; }
        public int AviablePeopleCount { get; set; }
        public int Duration { get; set; }
        public bool IsHotOffer { get; set; }
        public string Description { get; set; }
        public string? ClientName { get; set; }
        public string? ClientSurname { get; set; }
        public HotelDTO Hotel { get; set; }
        public int TransportInId { get; set; }
        public TransportDTO TransportIn { get; set; }
        public int TransportOutId { get; set; }
        public TransportDTO TransportOut { get; set; }
    }
}
