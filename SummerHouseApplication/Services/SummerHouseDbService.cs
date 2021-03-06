﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SummerHouseApplication.Data;
using SummerHouseApplication.Models;
using SummerHouseApplication.Models.Info;
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
        private readonly UserManager<SummerHouseUser> _userManager;
        public SummerHouseDbService(SummerHouseDbContext ctx,
            UserManager<SummerHouseUser> userManager)
        {
            _userManager = userManager;
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
                )
                .Include(m => m.Coordinates)
                .Include(m => m.Info)
                .FirstOrDefault();

                if(markerToBeDeleted != null)
                {
                    if (markerToBeDeleted.FishingNetId.HasValue)
                    {
                        // This is a fishing net marker
                        var fishingNetMarkers = _ctx.Markers
                            .Where(m => m.FishingNetId == markerToBeDeleted.FishingNetId.Value)
                            .Include(m => m.Info)
                            .Include(m => m.Coordinates)
                            .ToList();

                        var fishingNetToBeDeleted = _ctx.FishingNets.Where(n => n.Id == markerToBeDeleted.FishingNetId.Value).First();
                        _ctx.FishingNets.Remove(fishingNetToBeDeleted);

                        foreach (var marker in fishingNetMarkers)
                        {
                            _ctx.Locations.Remove(marker.Coordinates);
                            _ctx.InfoWindows.Remove(marker.Info);
                        }

                        _ctx.Markers.RemoveRange(fishingNetMarkers);
                        _ctx.SaveChanges();
                    }
                    else
                    {
                        // Fish marker
                        _ctx.Locations.Remove(markerToBeDeleted.Coordinates);
                        _ctx.InfoWindows.Remove(markerToBeDeleted.Info);
                        _ctx.Markers.Remove(markerToBeDeleted);
                        _ctx.SaveChanges();
                    }
                }
                else if(summerHouse.LocationOnMap != null &&
                    summerHouse.LocationOnMap.Latitude == location.Latitude &&
                    summerHouse.LocationOnMap.Longitude == location.Longitude)
                {
                    summerHouse.LocationOnMap = null;
                    _ctx.Update(summerHouse);
                    _ctx.SaveChanges();
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
            var userOwnedHouses = _ctx.SummerHouses.Where(h => h.Owner.Id == user.Id).ToList();

            var userSharedHouses = _ctx.SharedSummerHouses
                .Where(sh => sh.User.Id == user.Id)
                .Select(sh => sh.SummerHouseId).ToList();

            if(userSharedHouses.Count > 0)
            {
                userOwnedHouses.AddRange(
                _ctx.SummerHouses.Where(s => userSharedHouses.Contains(s.Id))
                );
            }

            return userOwnedHouses;
        }
        public SummerHouse GetSummerHouseById(SummerHouseUser user, int Id)
        {
            var house = _ctx.SummerHouses.Where(h => h.Id == Id && h.Owner.Id == user.Id)
                .Include(h => h.LocationOnMap)
                .Include(h => h.QuestionAnswerPairs)
                .FirstOrDefault();

            if(house == null)
            {
                // Check if this is a shared summerhouse
                var sharedHouse = _ctx.SharedSummerHouses
                .Where(sh => sh.User.Id == user.Id).FirstOrDefault();

                if(sharedHouse != null)
                {
                    house = _ctx.SummerHouses.Where(s => s.Id == sharedHouse.SummerHouseId)
                        .Include(h => h.LocationOnMap)
                        .Include(h => h.QuestionAnswerPairs)
                        .FirstOrDefault();
                }

            }

            return house;
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
        public void ShareSummerHouse(SummerHouse houseToBeShared, string emailOfNewUser)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(emailOfNewUser).Result;
                SharedSummerHouse sharedHouse = new SharedSummerHouse
                {
                    SummerHouse = houseToBeShared,
                    User = user
                };
                _ctx.SharedSummerHouses.Add(sharedHouse);
                _ctx.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public QuestionAnswer CreateQuestionAnswerPair(QuestionAnswer qaPair)
        {
            try
            {
                _ctx.Add(qaPair);
                _ctx.SaveChanges();
                return qaPair;
            }
            catch (Exception)
            {
                throw;
            }

            
        }
    }
}
