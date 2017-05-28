
using Microsoft.EntityFrameworkCore;
using SummerHouseApplication.Data;
using SummerHouseApplication.Models;
using SummerHouseApplication.Models.Map;
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

        public SummerHouse GetSummerHouseById(SummerHouseUser user, int Id)
        {
            return _ctx.SummerHouses
                .Where(h => h.Id == Id && h.Owner.Id == user.Id)
                .Include(h => h.LocationOnMap)
                .FirstOrDefault();
        }

        public SummerHouse MarkSummerHouseLocation(SummerHouse house, Location location)
        {
            try
            {
                _ctx.Locations.Add(location);
                var trackedHouse = _ctx.SummerHouses.Where(sh => sh.Id == house.Id).FirstOrDefault();
                trackedHouse.LocationOnMap = location;
                _ctx.SaveChanges();
                return trackedHouse;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSummerHouse(int id)
        {
            try
            {
                var house = _ctx.SummerHouses.Where(h => h.Id == id).FirstOrDefault();
                if (house != null)
                {
                    _ctx.SummerHouses.Remove(house);
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
