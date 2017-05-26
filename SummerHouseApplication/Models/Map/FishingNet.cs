using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models.Map
{
    public class FishingNet
    {
        public int Id { get; set; }
        public List<MapMarker> Markers { get; set; }
        public SummerHouse SummerHouse { get; set; }
    }
}
