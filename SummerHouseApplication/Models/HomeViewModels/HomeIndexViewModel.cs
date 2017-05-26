using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public List<SummerHouse> SummerHouses { get; set; }
        public SummerHouseUser User { get; set; }
    }
}
