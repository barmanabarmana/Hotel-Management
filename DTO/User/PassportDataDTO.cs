using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class PassportDataDTO
    {
        public int Id { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Nationality { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
