using Models.Hotels.Times;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Hotels
{
    public class HotelRoomModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Price { get; set; }
        public List<DTOffsetModel> BookedDays { get; set; }
    }
}
