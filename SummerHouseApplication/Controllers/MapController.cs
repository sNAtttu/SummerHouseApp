using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Models.Map;
using SummerHouseApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Controllers
{
    [Authorize]
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

        [HttpPost("/map/marker")]
        public IActionResult PostMarker()
        {
            return View();
        }
        [HttpPost("/map/location/{summerhouseid}")]
        public IActionResult PostCottageLocation(int summerhouseid, [FromBody]Location location)
        {
            var house = _dataService.GetSummerHouseById(summerhouseid);
            _dataService.MarkSummerHouseLocation(house, location);
            return Ok();
        }
    }
}
