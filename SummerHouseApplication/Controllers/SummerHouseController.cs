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
            return View(userHouses);
        }

        // GET: SummerHouse/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // GET: SummerHouse/Delete/5
        public ActionResult Delete(int id)
        {
            bool deleted = _dataService.DeleteSummerHouse(id);
            var userHouses = _dataService.GetUserSummerHouses(_userManager.GetUserAsync(User).Result);
            return View(userHouses);
        }

        // POST: SummerHouse/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}