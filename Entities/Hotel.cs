using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Hotel
    {
        //#hotel_ID|Description|Rooms
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public decimal Cost { get; set; }
        public string Location { get; set; }
    }
}
