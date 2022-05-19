using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booking
    {
        //#customer_ID|fromDate|toDate|room
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Room { get; set; }
        public Hotel Hotel { get; set; }
        public Customer Customer { get; set; }
    }
}
