using AGL_Common;
using AGL_Common.Enums;
using AGL_DataAccessLayer;
using AGL_DTO.DTO;
using AGL_DTO.JsonDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGL_BussinessLogicLayer
{
    public class PeopleBAL : IPeopleBAL
    {
        #region Iniliatilization

        private IPeopleDAL _peopleDataDAL { get; }

        #endregion

        #region Constructor

        public PeopleBAL(IPeopleDAL peopleDataDAL)
        {
            _peopleDataDAL = peopleDataDAL;
        }

        #endregion

        #region Implemented methods

        public async Task<Response<List<Gender>>> GetPeople()
        {
            var response = new Response<List<Gender>>();
            var peopleResponse = await _peopleDataDAL.GetPeople();
            if (peopleResponse.ResponseStatus == ResponseStatus.Failure)
            {
                response.Errors.AddRange(peopleResponse.Errors);
                return response;
            }
            var cats = peopleResponse.Data?
                 .Where(person => person.Pets != null) // owner must have a pet
                 .Where(person => person.Pets.Any(pet => pet.Type == PetTypeJson.Cat)) // owner must have a cat
                 .GroupBy(person => person.Gender, // group by gender (male/female)
                     person => person.Pets,
                     (gender, pets) => new Gender
                     {
                         GenderType = gender,
                         Cats = pets
                             .SelectMany(pet => pet) //get all pets from a specific gender
                             .Where(pet => pet.Type == PetTypeJson.Cat) //get all cats
                             .OrderBy(pet => pet.Name) // order by cat names
                             .Select(pet => new Cat
                             {
                                 Name = pet.Name // return cat name
                            }).ToList()
                     });
            response.Data = cats?.ToList();
            return response;
        }

        #endregion
    }
}
