using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Transports
{
    public class TransportTicketModel
    {
        public int Id { get; set; }
        public string TransportType { get; set; }
        public string DeparturePoint { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public int NumberOfSeat { get; set; }
        public decimal Price { get; set; }
        public string PassangerName { get; set; }
        public string PassangerSurname { get; set; }
    }
}
