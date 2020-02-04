using AGL_BussinessLogicLayer;
using AGL_Common;
using AGL_Common.Enums;
using AGL_DataAccessLayer;
using AGL_DTO;
using AGL_DTO.JsonDTO;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AGL_BussinessLogicLayer_UnitTests
{
    public class PeopleBALUnitTests
    {
        #region Initialization 

        private IPeopleBAL _peopleBAL { get; set; }
        private Mock<IPeopleDAL> _mockPeopleDAL { get; set; } = new Mock<IPeopleDAL>();

        #endregion

        #region Creating Mock Objects

        private List<PersonJsonDTO> GetMockPersonObjects()
        {
            var personMockData = new List<PersonJsonDTO>
            {
               new PersonJsonDTO
                {
                    Age = 20,
                    Gender = GenderType.Female,
                    Name = "Elle",
                    Pets = new List<PetJsonDTO>
                    {
                        new PetJsonDTO
                        {
                            Name = "Waffles",
                            Type = PetTypeJson.Cat,
                        },
                        new PetJsonDTO
                        {
                            Name = "Sam",
                            Type = PetTypeJson.Dog,
                        }
                    }
                },
                new PersonJsonDTO
                {
                    Age = 24,
                    Gender = GenderType.Male,
                    Name = "John",
                    Pets = new List<PetJsonDTO>
                    {
                        new PetJsonDTO
                        {
                            Name = "Helen",
                            Type = PetTypeJson.Cat,
                        },
                        new PetJsonDTO
                        {
                            Name = "Albert",
                            Type = PetTypeJson.Cat,
                        }
                    }
                }
            };

            return personMockData;
        }

        #endregion

        #region Positive Unit tests

        [Fact]
        public async Task Testcase_GetPeople_NoData_Success()
        {
            _mockPeopleDAL.Setup(p => p.GetPeople())
                                     .Returns(Task.Run(() => new Response<List<PersonJsonDTO>>()));

            _peopleBAL = new PeopleBAL(_mockPeopleDAL.Object);
            var actualPeople = await _peopleBAL.GetPeople();
            Assert.Null(actualPeople.Data);
            Assert.True(actualPeople.ResponseStatus == ResponseStatus.Success);
        }

        [Fact]
        public async Task Testcase_GetCats_Count_Success()
        {
            var peopleFakeObject = GetMockPersonObjects();
            _mockPeopleDAL.Setup(p => p.GetPeople())
                                    .Returns(Task.Run(() => new Response<List<PersonJsonDTO>> { Data = peopleFakeObject }));
            _peopleBAL = new PeopleBAL(_mockPeopleDAL.Object);
            var peopleFromBAL = await _peopleBAL.GetPeople();
            Assert.NotNull(peopleFromBAL.Data);
            Assert.True(peopleFromBAL.ResponseStatus == ResponseStatus.Success);
            Assert.True(peopleFromBAL.Data.Count == 2);
        }

        [Fact]
        public async Task Testcase_GetCatsByMaleGender_Success()
        {
            var peopleFakeObject = GetMockPersonObjects();
            _mockPeopleDAL.Setup(p => p.GetPeople())
                                    .Returns(Task.Run(() => new Response<List<PersonJsonDTO>> { Data = peopleFakeObject }));
            _peopleBAL = new PeopleBAL(_mockPeopleDAL.Object);
            var peopleFromBAL = await _peopleBAL.GetPeople();
            Assert.NotNull(peopleFromBAL.Data);
            Assert.True(peopleFromBAL.ResponseStatus == ResponseStatus.Success);
            var catsByMale = peopleFromBAL.Data.FirstOrDefault(p => p.GenderType == GenderType.Male);
            Assert.NotNull(catsByMale);
            Assert.NotNull(catsByMale.Cats);
            Assert.True(catsByMale.Cats.Count == 2);
        }

        [Fact]
        public async Task Testcase_GetCatsByFemaleGender_Success()
        {
            var peopleFakeObject = GetMockPersonObjects();
            _mockPeopleDAL.Setup(p => p.GetPeople())
                                    .Returns(Task.Run(() => new Response<List<PersonJsonDTO>> { Data = peopleFakeObject }));
            _peopleBAL = new PeopleBAL(_mockPeopleDAL.Object);
            var peopleFromBAL = await _peopleBAL.GetPeople();
            Assert.NotNull(peopleFromBAL.Data);
            Assert.True(peopleFromBAL.ResponseStatus == ResponseStatus.Success);

            var catsByFeMale = peopleFromBAL.Data.FirstOrDefault(p => p.GenderType == GenderType.Female);
            Assert.NotNull(catsByFeMale);
            Assert.NotNull(catsByFeMale.Cats);
            Assert.True(catsByFeMale.Cats.Count == 1);

        }
        #endregion

        #region Negative Unit tests

        [Fact]
        public async Task Testcase_GetPeople_URL_Error()
        {
            _mockPeopleDAL.Setup(p => p.GetPeople())
                                      .Returns(Task.Run(() => new Response<List<PersonJsonDTO>>
                                      {
                                          Errors = new List<string>
                                          {
                                              ErrorMessages.ErrorC001_CannotConnectToServer
                                          }
                                      }));
            _peopleBAL = new PeopleBAL(_mockPeopleDAL.Object);
            var response = await _peopleBAL.GetPeople();
            Assert.Null(response.Data);
            Assert.False(response.ResponseStatus == ResponseStatus.Success);
            Assert.True(response.ResponseStatus == ResponseStatus.Failure);
        }

        #endregion
    }
}
