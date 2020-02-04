using AGL_Common;
using AGL_DTO.JsonDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGL_DataAccessLayer
{
    public interface IPeopleDAL
    {
        /// <summary>
        /// Returns a List of PersonDto's or failure with errors.
        /// </summary>
        /// <returns></returns>
        Task<Response<List<PersonJsonDTO>>> GetPeople();
    }
}
