using AGL_BussinessLogicLayer;
using AGL_Common;
using AGL_DTO;
using AGL_DTO.DTO;
using AGL_WebApplication.Controllers;
using AGL_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AGL_WebApplication_UnitTests
{
    public class PeopleControllerTests
    {
        #region Initialization

        private PeopleController _peopleController { get; set; }
        private Mock<IPeopleBAL> _mockPeopleBAL { get; set; } = new Mock<IPeopleBAL>();

        private static int genderTypeCount = 2;
        private static int maleCount = 2;
        private static int femaleCount = 2;

        #endregion

        #region Creating Mock object

        private List<Gender> GetMockGenderObjects()
        {
            var mockGenderObject = new List<Gender>
            {
                new Gender
                {
                    GenderType = GenderType.Female,
                    Cats = new List<Cat>
                    {
                        new Cat
                        {
                            Name = "Jennifer"
                        },
                        new Cat
                        {
                            Name = "Samantha"
                        }
                    },
                },
                new Gender
                {
                    GenderType = GenderType.Male,
                    Cats = new List<Cat>
                    {
                        new Cat
                        {
                            Name = "Fred",
                        },
                        new Cat
                        {
                            Name = "Tom",
                        }
                    },
                },
            };
            return mockGenderObject;
        }

        #endregion

        #region MockDataInitialization

        private void InitializaMockData()
        {
            _mockPeopleBAL.Setup(p => p.GetPeople())
                             .Returns(Task.Run(() => new Response<List<Gender>> { Data = GetMockGenderObjects() }));
        }

        #endregion

        #region Test Cases

        [Fact]
        public async Task Testcase_GetPeople_Data_Success()
        {
            InitializaMockData();
            _peopleController = new PeopleController(_mockPeopleBAL.Object);
            var actionResult = await _peopleController.Index();
            Assert.NotNull(actionResult);
            Assert.True(actionResult is ViewResult);

            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.Model);
            Assert.True(viewResult.Model is List<GenderViewModel>);
         }

        [Fact]
        public async Task Testcase_GetPeople_MaleData_Success()
        {
       
            InitializaMockData();
            _peopleController = new PeopleController(_mockPeopleBAL.Object);
            var actionResult = await _peopleController.Index();

            var viewResult = actionResult as ViewResult;
            var genders = viewResult.Model as List<GenderViewModel>;
            Assert.NotNull(genders);
            Assert.True(genders.Count == genderTypeCount);

            var maleOwners = genders.FirstOrDefault(p => p.Gender == GenderType.Male);
            Assert.NotNull(maleOwners);
            Assert.NotNull(maleOwners.Cats);
            Assert.True(maleOwners.Cats.Count == maleCount);
            Assert.True(maleOwners.Cats.FirstOrDefault()?.Name == "Fred");
            Assert.True(maleOwners.Cats[1]?.Name == "Tom");
        }

        [Fact]
        public async Task Testcase_GetPeople_FemaleData_Success()
        {
            InitializaMockData();
            _peopleController = new PeopleController(_mockPeopleBAL.Object);
            var actionResult = await _peopleController.Index();

            var viewResult = actionResult as ViewResult;
            var genders = viewResult.Model as List<GenderViewModel>;
            Assert.NotNull(genders);
            Assert.True(genders.Count == genderTypeCount);

            var femaleOwners = genders.FirstOrDefault(p => p.Gender == GenderType.Female);
            Assert.NotNull(femaleOwners);
            Assert.NotNull(femaleOwners.Cats);
            Assert.True(femaleOwners.Cats.Count == femaleCount);
            Assert.True(femaleOwners.Cats.FirstOrDefault()?.Name == "Jennifer");
        }

        #endregion
    }
}
