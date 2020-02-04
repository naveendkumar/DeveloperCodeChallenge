using AGL_DTO.Enums;
using System.Collections.Generic;

namespace AGL_DTO.DTO
{
    public class Gender
    {
        public GenderType GenderType { get; set; }
        public List<Cat> Cats { get; set; }
    }
}
