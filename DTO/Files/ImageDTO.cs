﻿using DTO.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Files
{
    public class ImageDTO
    {
        public ImageDTO(string name, string path, int hotelId)
        {
            Name = name;
            Path = path;
            HotelId = hotelId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int HotelId { get; set; }
        public HotelDTO Hotel { get; set; }
    }
}
