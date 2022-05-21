using DTO.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Files
{
    public class ImageDTO
    {
        public ImageDTO(string path)
        {
            Path = path;
        }
        public int Id { get; set; }
        public string Path { get; set; }
        public HotelDTO Hotel { get; set; }
    }
}
