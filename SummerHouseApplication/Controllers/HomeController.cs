using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SummerHouseApplication.Services;
using Microsoft.AspNetCore.Identity;
using SummerHouseApplication.Models;
using SummerHouseApplication.Models.HomeViewModels;

namespace SummerHouseApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<SummerHouseUser> _userManager;
        private readonly SummerHouseDbService _dataService;
        public HomeController(
            UserManager<SummerHouseUser> userManager, 
            SummerHouseDbService dataService)
        {
            _dataService = dataService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var houses = _dataService.GetUserSummerHouses(user);
            if (houses.Count > 0)
            {
                HomeIndexViewModel model = new HomeIndexViewModel
                {
                    SummerHouses = houses,
                    User = user
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Create", "SummerHouse");
            }
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
