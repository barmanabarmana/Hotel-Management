﻿using DTO.Hotels.Times;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Hotels
{
    public class HotelRoomDTO
    {
        public HotelRoomDTO() { }
        public HotelRoomDTO(int Number, int SleepingPlaces, int Price)
        {
            this.Number = Number;
            this.SleepingPlaces = SleepingPlaces;
            this.Price = Price;
            BookedDays = new List<DTOffsetDTO>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public virtual HotelDTO Hotel { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Price { get; set; }
        public List<DTOffsetDTO> BookedDays { get; set; }
    }
}
