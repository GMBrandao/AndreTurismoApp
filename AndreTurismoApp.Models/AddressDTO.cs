using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class AddressDTO
    {
        public int Id;

        [JsonProperty("pais")]
        public string? Country { get; set; }

        [JsonProperty("cep")]
        public string? CEP { get; set; }

        [JsonProperty("logradouro")]
        public string? Street { get; set; }

        [JsonProperty("bairro")]
        public string? Neighborhood { get; set; }
        
        [JsonProperty("localidade")]
        public string? City { get; set; }

        [JsonProperty("uf")]
        public string? State { get; set; }
    }
}
