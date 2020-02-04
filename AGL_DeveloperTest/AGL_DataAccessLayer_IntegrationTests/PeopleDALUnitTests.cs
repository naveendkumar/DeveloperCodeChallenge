using AGL_Common.Enums;
using AGL_DataAccessLayer;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AGL_DataAccessLayer_IntegrationTests
{
    public class PeopleDALUnitTests
    {
        #region Initialization

        public PeopleDAL _peopleDAL;

        #endregion

        #region Test Cases

        [Fact]
        public async Task Testcase_GetPeople_ExpectSuccess()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            _peopleDAL = new PeopleDAL(config);
            var peopleResponse = await _peopleDAL.GetPeople();
            Assert.NotNull(peopleResponse);
            Assert.True(peopleResponse.ResponseStatus == ResponseStatus.Success);
            Assert.False(peopleResponse.Errors.Any());
            var people = peopleResponse.Data;
            Assert.NotNull(people);
            Assert.True(people.Any());
        }

        #endregion
    }
}
