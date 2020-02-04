using AGL_BussinessLogicLayer;
using AGL_Common.Enums;
using AGL_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGL_WebApplication.Controllers
{
    public class PeopleController : Controller
    {
        #region Initialization

        private IPeopleBAL _peopleBAL { get; }

        #endregion

        #region Constructor

        public PeopleController(IPeopleBAL peopleBAL)
        {
            _peopleBAL = peopleBAL;
        }

        #endregion

        #region Controller methods

        public async Task<IActionResult> Index()
        {
            try
            {
                var peopleResponse = await _peopleBAL.GetPeople();
                if (peopleResponse.ResponseStatus == ResponseStatus.Failure)
                {
                    peopleResponse.Errors.ForEach(error =>
                    {
                        ModelState.AddModelError("", error);
                    });

                    return View(new List<GenderViewModel>());
                }
                else
                {
                    var viewModel = peopleResponse.Data.Select(gender => new GenderViewModel
                    {
                        Gender = gender.GenderType,
                        Cats = gender.Cats.Select(cat => new CatViewModel
                        {
                            Name = cat.Name
                        }).ToList()
                    }).ToList();

                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(new List<GenderViewModel>());
            }
        }

        #endregion
    }
}