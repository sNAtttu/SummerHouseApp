using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Models.Info
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public SummerHouse SummerHouse { get; set; }
    }
}
