using Newtonsoft.Json;
using System.Collections.Generic;

namespace AGL_DTO.JsonDTO
{
    public class PersonJsonDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public GenderType Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public List<PetJsonDTO> Pets { get; set; }
    }
}
