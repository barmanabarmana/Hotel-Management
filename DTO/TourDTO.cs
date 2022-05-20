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
        public TourDTO(string Name, decimal Price, string Type, string Country, string City, int Duration, string Description)
        {
            this.Name = Name;
            this.Price = Price;
            this.Type = Type;
            this.Country = Country;
            this.City = City;
            this.Duration = Duration;
            this.Description = Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
    }
}
