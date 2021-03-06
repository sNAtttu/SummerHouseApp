using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Models;
using SummerHouseApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SummerHouseApplication.Controllers
{
    [Authorize]
    public class SummerHouseController : Controller
    {
        private readonly SummerHouseDbService _dataService;
        private readonly UserManager<SummerHouseUser> _userManager;
        public SummerHouseController(
            SummerHouseDbService dataservice,
            UserManager<SummerHouseUser> userManager
            )
        {
            _dataService = dataservice;
            _userManager = userManager;
        }
        // GET: SummerHouse
        public ActionResult Index()
        {
            var userHouses = _dataService.GetUserSummerHouses(_userManager.GetUserAsync(User).Result);
            if (userHouses.Count > 0)
            {
                return View(userHouses);
            }
            else
            {
                return RedirectToAction("Create", "SummerHouse");
            }         
        }

        // GET: SummerHouse/Info/5
        public ActionResult Info(int id)
        {         
            return View(_dataService.GetSummerHouseById(GetUser(), id));
        }
        // GET: SummerHouse/Admin/5
        public ActionResult Admin(int id)
        {
            return View(_dataService.GetSummerHouseById(GetUser(), id));
        }

        // GET: SummerHouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SummerHouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SummerHouse house)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                house.Owner = user;
                _dataService.CreateSummerHouse(house);
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: SummerHouse/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SummerHouse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult ShareSummerHouse(int id, string email)
        {
            var house = _dataService.GetSummerHouseById(GetUser(), id);
            _dataService.ShareSummerHouse(house, email);
            return RedirectToAction("Index");
        }

        // POST: SummerHouse/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int houseId)
        {
            try
            {
                // TODO: Add delete logic here
                bool deleted = _dataService.DeleteSummerHouse(houseId);
                if (deleted)
                {
                    var userHouses = _dataService.GetUserSummerHouses(_userManager.GetUserAsync(User).Result);
                    return RedirectToAction("Index", userHouses);
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }
        private SummerHouseUser GetUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }
    }
}