using AGL_Common;
using AGL_DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGL_BussinessLogicLayer
{
    /// <summary>
    /// This is the interface for all Bussiness methods.
    /// </summary>
    public interface IPeopleBAL
    {
        Task<Response<List<Gender>>> GetPeople();
    }
}
