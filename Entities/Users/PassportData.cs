using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class PassportData
    {
        public int Id { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Nationality { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
