using Models.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Files
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public HotelModel Hotel { get; set; }
    }
}
