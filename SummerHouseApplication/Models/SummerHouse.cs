using SummerHouseApplication.Models.Info;
using SummerHouseApplication.Models.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models
{
    public class SummerHouse
    {
        public int Id { get; set; }
        [Display(Name="Nimi")]
        public string Name { get; set; }
        [Display(Name="Osoite")]
        public string Address { get; set; }
        [Display(Name = "Kaupunki")]
        public string City { get; set; }
        [Display(Name = "Postinumero")]
        public string PostalCode { get; set; }
        [Display(Name = "Ranta")]
        public bool HasBeach { get; set; }
        [Display(Name = "Sähkö")]
        public bool HasElectricity { get; set; }
        [Display(Name = "Juokseva vesi")]
        public bool HasRunningWater { get; set; }
        [Display(Name = "Sauna")]
        public bool HasSauna { get; set; }
        public DateTime Created { get; set; }
        public Location LocationOnMap { get; set; }
        public SummerHouseUser Owner { get; set; }
        public List<MapMarker> FishMarkers { get; set; }
        public List<FishingNet> FishingNets { get; set; }
        public List<SharedSummerHouse> AuthorizedUsers { get; set; }
        public List<QuestionAnswer> QuestionAnswerPairs { get; set; }
    }
}
