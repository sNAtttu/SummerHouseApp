using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Controllers
{
    public class MapController : Controller
    {
        private readonly SummerHouseDbService _dataService;
        public MapController(SummerHouseDbService dataservice)
        {
            _dataService = dataservice;
        }
        [HttpGet("/map/{id}")]
        public IActionResult Index(int Id)
         {
            var house = _dataService.GetSummerHouseById(Id);
            return View(house);
        }

        [HttpPost]
        public IActionResult PostMarker()
        {
            return View();
        }
    }
}
