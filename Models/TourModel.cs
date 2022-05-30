using Models.Hotels;
using Models.Transports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TourModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Meal { get; set; }
        public int AviablePeopleCount { get; set; }
        public int Duration { get; set; }
        public bool IsHotOffer { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public HotelModel Hotel { get; set; }
        public int? TransportInId { get; set; }
        public TransportModel? TransportIn { get; set; }
        public int? TransportOutId { get; set; }
        public TransportModel? TransportOut { get; set; }
    }
}
