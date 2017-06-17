using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Models;
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
        private readonly UserManager<SummerHouseUser> _userManager;
        public MapController(
            SummerHouseDbService dataservice,
            UserManager<SummerHouseUser> userManager
            )
        {
            _dataService = dataservice;
            _userManager = userManager;
        }
        [HttpGet("/map/{id}")]
        public IActionResult Index(int Id)
         {
            var house = _dataService.GetSummerHouseById(GetUser(), Id);
            return View(house);
        }

        [HttpPost("/map/marker/{summerhouseid}")]
        public IActionResult PostMarker(int summerhouseid, [FromBody]MapMarker marker)
        {
            var house = _dataService.GetSummerHouseById(GetUser(), summerhouseid);
            if (marker != null && house != null)
            {
                marker.SummerHouse = house;
                _dataService.CreateMarker(marker);
            }
            return View("Index", house);
        }
        [HttpPost("/map/location/{summerhouseid}")]
        public IActionResult PostCottageLocation(int summerhouseid, [FromBody]Location location)
        {

            var house = _dataService.GetSummerHouseById(GetUser(), summerhouseid);
            _dataService.MarkSummerHouseLocation(house, location);
            return View("Index", house);
        }
        [HttpPost("/map/fishingnet/{summerhouseid}")]
        public IActionResult PostFishingNet(int summerhouseid, [FromBody]List<MapMarker> markers)
        {
            var house = _dataService.GetSummerHouseById(GetUser(), summerhouseid);
            _dataService.CreateFishingNet(house, markers);
            return View("Index", house);
        }
        private SummerHouseUser GetUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

    }
}
