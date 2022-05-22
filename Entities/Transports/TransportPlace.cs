﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Transports
{
    public class TransportPlace
    {
        public int Id { get; set; }
        public virtual Transport Transport { get; set; }
        public int TransportId { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }
    }
}
