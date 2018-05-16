using Places.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Places.DAL.Interfaces;
using System.Linq;

namespace Places.DAL.Repositories
{
    public class PlaceRepository : IRepository<Place> , IPlaceRepository
    {
        public PlaceRepository(DbContext context):base(context)
        {

        }


        public IQueryable<Place> GetPlacesByType(int id)
        {
            var places = Get().Where(p=>p.Type.Id==id)
                                         .Include(p => p.Address.Street.City.Country)
                                         .Include(p => p.Images)
                                         .Include(p => p.User)
                                         .Include(p => p.PlaceFacilities)
                                         .Include(p => p.Reviews)
                                         .Include(p => p.WorkSchedules);
            return places;

        }

        public Place GetPlaceDetails(int id)
        {

            var place = _dbset.Where(p => p.Id == id)
                                         .Include(p=>p.Type)
                                         .Include(p => p.Address.Street.City.Country)
                                         .Include(p => p.Images)
                                         .Include(p => p.User)
                                         .Include(p => p.PlaceFacilities)
                                         .Include(p => p.Reviews)
                                         .Include(p => p.WorkSchedules)
                                         .SingleOrDefault();


            return place;
        }

        public List<Facilitie> GetFacilities(int id)
        {
            var facilities = _dbset.SelectMany(p => p.PlaceFacilities)
                .Where(p => p.PlaceId == id)
                .Select(p => p.Facilitie)
                .ToList();

            return facilities;
        }

        public IQueryable<Place> Get()
        {
            var places = _dbset
                            
                                 .Include(p => p.Address.Street.City.Country)
                                 .Include(p => p.Type)
                                        .Include(p => p.Images)
                                        .Include(p => p.User)
                                        .Include(p => p.PlaceFacilities)
                                        .Include(p => p.Reviews)
                                        .Include(p => p.WorkSchedules);

            return places;

        }
     
        public List<Place> GetTopPlaces(int topNumber)
        {
            var topPlaces = _dbset
                              .OrderByDescending(p => p.Reviews.Count > 0 ? p.Reviews.Average(t => t.Rating) : 0.0   )
                                  .Include(p => p.Address.Street.City.Country)
                                  .Include(p=>p.Type)
                                         .Include(p => p.Images)
                                         .Include(p => p.User)
                                         .Include(p => p.PlaceFacilities)
                                         .Include(p => p.Reviews)
                                         .Include(p => p.WorkSchedules)
                               .Take(topNumber).ToList();     
            return topPlaces;
        }

        public IQueryable<Place> GetByCity(int cityId)
        {
            var places = _dbset.Where(p => p.Address.Street.CityID == cityId);
                             
            return places;
        }

        public IQueryable<Place> GetByCountry(int countryId)
        {
            var places = _dbset.Where(p => p.Address.Street.City.CountryID == countryId);

            return places;
        }

        public IQueryable<Place> GetByStreet(int streetId)
        {
            var places = _dbset.Where(p => p.Address.StreetID== streetId);

            return places;
        }

        public Place GetPlaceById(int id)
        {
           var place = _dbset.Where(p=>p.Id==id)
                                        .Include(p => p.Address.Street.City.Country)
                                        .Include(p => p.Type)
                                        .Include(p => p.Images)
                                        .Include(p => p.User)
                                        .Include(p => p.PlaceFacilities)
                                        .Include(p => p.Reviews)
                                        .Include(p => p.WorkSchedules).SingleOrDefault();
            return place;
        }

    }
}
