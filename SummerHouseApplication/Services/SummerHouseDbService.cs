
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
        public void CreateFishingNet(SummerHouse summerhouse, List<MapMarker> netMarkers)
        {
            try
            {
                netMarkers.ForEach(m => m.SummerHouse = summerhouse);
                FishingNet net = new FishingNet
                {
                    Markers = netMarkers,
                    SummerHouse = summerhouse
                };
                _ctx.FishingNets.Add(net);
                _ctx.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MapMarker CreateMarker(MapMarker marker)
        {
            try
            {
                _ctx.Markers.Add(marker);
                _ctx.SaveChanges();
                return marker;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteMarker(SummerHouse summerHouse, Location location)
        {
            try
            {
                var markerToBeDeleted = _ctx.Markers.Where(m => m.Coordinates != null &&
                m.Coordinates.Latitude == location.Latitude &&
                m.Coordinates.Longitude == location.Longitude &&
                m.SummerHouse != null && m.SummerHouse.Id == summerHouse.Id
                ).FirstOrDefault();

                if(markerToBeDeleted != null)
                {
                    if (markerToBeDeleted.FishingNetId.HasValue)
                    {
                        var fishingNetMarkers = _ctx.Markers
                            .Where(m => m.FishingNetId == markerToBeDeleted.FishingNetId.Value)
                            .ToList();

                        _ctx.RemoveRange(fishingNetMarkers);
                        _ctx.SaveChanges();
                    }
                    else
                    {
                        _ctx.Markers.Remove(markerToBeDeleted);
                        _ctx.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<FishingNet> GetFishingNetsBySummerhouseId(int summerhouseId)
        {
            try
            {
                var fishingNets = (from net in _ctx.FishingNets
                                  join marker in _ctx.Markers
                                  .Include(m => m.Coordinates)
                                  .Include(m => m.Info)
                                  on net.Id equals marker.FishingNet.Id
                                  into nm
                                  select new FishingNet
                                  {
                                      Id = net.Id,
                                      Markers = nm.ToList(),
                                      SummerHouse = net.SummerHouse

                                  }).ToList();


                return fishingNets;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<MapMarker> GetMarkersBySummerhouseId(int summerhouseId)
        {
            try
            {
                var markers = _ctx.Markers
                .Where(m => m.SummerHouse != null &&
                m.SummerHouse.Id == summerhouseId &&
                m.FishingNet == null)
                .Include(m => m.Info)
                .Include(m => m.Coordinates)
                .ToList();
                return markers;
            }
            catch(Exception ex)
            {
                throw ex;
            }

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
