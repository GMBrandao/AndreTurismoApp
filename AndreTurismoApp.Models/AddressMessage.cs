using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class AddressMessage
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public City City { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
