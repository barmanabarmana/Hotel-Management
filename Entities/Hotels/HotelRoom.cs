using Entities.Hotels.Times;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Hotels
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Price { get; set; }
        public virtual List<DTOffset> BookedDays { get; set; }
    }
}
