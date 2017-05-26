using SummerHouseApplication.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models
{
    public class SummerHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool HasBeach { get; set; }
        public bool HasElectricity { get; set; }
        public bool HasRunningWater { get; set; }
        public bool HasSauna { get; set; }
        public DateTime Created { get; set; }
        public Location LocationOnMap { get; set; }
        public SummerHouseUser Owner { get; set; }
        public List<MapMarker> FishMarkers { get; set; }
        public List<FishingNet> FishingNets { get; set; }
    }
}
