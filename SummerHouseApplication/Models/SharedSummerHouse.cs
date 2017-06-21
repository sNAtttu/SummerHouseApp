using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models
{
    public class SharedSummerHouse
    {
        public int Id { get; set; }
        public int SummerHouseId { get; set; }
        public SummerHouse SummerHouse { get; set; }
        public SummerHouseUser User { get; set; }
    }
}
