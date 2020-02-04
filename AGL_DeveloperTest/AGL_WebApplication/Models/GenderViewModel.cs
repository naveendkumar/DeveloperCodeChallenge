using AGL_DTO;
using System.Collections.Generic;

namespace AGL_WebApplication.Models
{
    public class GenderViewModel
    {
        public GenderType Gender { get; set; }
        public List<CatViewModel> Cats { get; set; }
    }
}
