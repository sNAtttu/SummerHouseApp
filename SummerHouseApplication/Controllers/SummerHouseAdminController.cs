using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Models.Info;
using SummerHouseApplication.Services;
using Microsoft.AspNetCore.Identity;
using SummerHouseApplication.Models;

namespace SummerHouseApplication.Controllers
{
    public class SummerHouseAdminController : Controller
    {
        
        private readonly SummerHouseDbService _dataService;
        private readonly UserManager<SummerHouseUser> _userManager;
        public SummerHouseAdminController(
            SummerHouseDbService dataservice,
            UserManager<SummerHouseUser> userManager
            )
        {
            _dataService = dataservice;
            _userManager = userManager;
        }

        [HttpGet("summerhouseadmin/{summerhouseid}")]
        public IActionResult Index(int summerhouseid)
        {
            if (summerhouseid == 0) return View("Error");
            var currentUser = GetUser();
            var house = _dataService.GetSummerHouseById(currentUser, summerhouseid);
            return View("Admin", house);
        }

        public IActionResult CreateQuestionAnswerPair(CreateQaFormViewModel qaVm)
        {
            var currentUser = GetUser();
            var house = _dataService.GetSummerHouseById(currentUser, qaVm.SummerHouseId);
            if(house.Owner != currentUser)
            {
                // Someone is trying to create qa pair for summerhouse but user is not an owner.
                // We will pretend that he can do that but in real life
                // nothing changes, hihihi.
                return View("Admin", house);
            }
            else
            {

                var question = qaVm.Question;
                if (question[(question.Length - 1)] != '?')
                {
                    question += "?";
                }

                QuestionAnswer pair = new QuestionAnswer
                {
                    Answer = qaVm.Answer,
                    Question = qaVm.Question,
                    SummerHouse = house
                };

                _dataService.CreateQuestionAnswerPair(pair);
                return View("Admin", house);
            }

        }

        private SummerHouseUser GetUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

    }
}