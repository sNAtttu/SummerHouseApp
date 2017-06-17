using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SummerHouseApplication.Models.Map;
using SummerHouseApplication.Services;

namespace SummerHouseApplication.Api
{
    [Produces("application/json")]
    
    public class MapMarkerController : Controller
    {
        private readonly SummerHouseDbService _dataService;
        public MapMarkerController(SummerHouseDbService dataService)
        {
            _dataService = dataService;
        }
        [Route("api/mapmarker/{summerhouseid}")]
        public List<MapMarker> GetSummerHouseMarkers(int summerhouseid)
        {
            return _dataService.GetMarkersBySummerhouseId(summerhouseid);
        }
        [Route("api/fishingnet/{summerhouseid}")]
        public List<FishingNet> GetFishingNets(int summerhouseid)
        {
            return _dataService.GetFishingNetsBySummerhouseId(summerhouseid);
        }
    }

}