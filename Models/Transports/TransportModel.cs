using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Transports
{
    public class TransportModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DeparturePoint { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public List<TourModel> Tours { get; set; }
        public List<TransportPlaceModel> TransportPlaces { get; set; }
    }
}
