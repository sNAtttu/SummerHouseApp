using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models.Map
{
    public class MapMarker
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InfoId { get; set; }
        public InfoWindow Info { get; set; }
        public int CoordinatesId { get; set; }
        public Location Coordinates { get; set; }
        public MarkerType MarkerType { get; set; }
        public FishType FishType { get; set; }
        public int? FishingNetId { get; set; }
        public FishingNet FishingNet { get; set; }
        public int SummerHouseId { get; set; }
        public SummerHouse SummerHouse { get; set; }
    }

    public enum MarkerType
    {
        FishingNet,
        FishingRod,
        SpinningRod,
        FishTrap
    }

    public enum FishType
    {
        Bass,
        Pike,
        Roach,
        Salmon,
        PikePerch,
        Bream,
        Burbot,
        MultipleFishes
    }

}
