using AGL_Common;
using AGL_Common.Enums;
using AGL_DTO.JsonDTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGL_DataAccessLayer
{
    public class PeopleDAL : DataLayerService, IPeopleDAL
    {
        private IConfiguration _configuration { get; }

        public PeopleDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response<List<PersonJsonDTO>>> GetPeople()
        {
            var response = new Response<List<PersonJsonDTO>>();
            var uri = new Uri(_configuration["PeopleUrl"]);
            var peopleResponse = await GetResponse(uri);
            if (peopleResponse.ResponseStatus == ResponseStatus.Failure)
            {
                response.Errors.AddRange(peopleResponse.Errors);
                return response;
            }
            else
            {
                try
                {
                    response.Data = JsonConvert.DeserializeObject<List<PersonJsonDTO>>(peopleResponse.Data);
                    if (response.Data == null || !response.Data.Any())
                    {
                        response.Errors.Add(ErrorMessages.ErrorC002_CannotDeserializePeople);
                    }
                }
                catch (JsonException)
                {
                    response.Errors.Add(ErrorMessages.ErrorC002_CannotDeserializePeople);
                }
            }

            return response;
        }
    }
}
