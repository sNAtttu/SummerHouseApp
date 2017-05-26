
using SummerHouseApplication.Data;
using SummerHouseApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerHouseApplication.Services
{
    public class SummerHouseDbService
    {
        private readonly SummerHouseDbContext _ctx;
        public SummerHouseDbService(SummerHouseDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<SummerHouse> GetUserSummerHouses(SummerHouseUser user)
        {
            return _ctx.SummerHouses.Where(h => h.Owner.Id == user.Id).ToList();
        }

        public SummerHouse GetSummerHouseById(int Id)
        {
            return _ctx.SummerHouses.Where(h => h.Id == Id).FirstOrDefault();
        }

        public SummerHouse CreateSummerHouse(SummerHouse house)
        {
            try
            {
                house.Created = DateTime.Now;
                var createdHouse = _ctx.SummerHouses.Add(house);
                _ctx.SaveChanges();
                return createdHouse.Entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
