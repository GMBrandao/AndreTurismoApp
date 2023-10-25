using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public City City { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Neighborhood { get; set; }

        public Address() { }

        public Address(AddressDTO addressDTO)
        {
            this.Street = addressDTO.Street;
            this.CEP = addressDTO.CEP;
            this.City = new City() { Description = addressDTO.City };
            this.Neighborhood = addressDTO.Neighborhood;
        }
    }
}
