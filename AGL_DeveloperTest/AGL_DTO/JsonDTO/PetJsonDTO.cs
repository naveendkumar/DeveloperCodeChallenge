using Newtonsoft.Json;

namespace AGL_DTO.JsonDTO
{
    public class PetJsonDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public PetTypeJson Type { get; set; }
    }
}
