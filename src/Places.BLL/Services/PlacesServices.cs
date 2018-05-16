using AutoMapper;
using DomainUtil;
using Places.BLL.Interfaces;
using Places.DAL.Interfaces;
using Places.Domain;

using Places.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Places.BLL.Services
{
    public class PlacesServices : IPlaceServices
    {
        private IPlaceRepository _repository;
        private IRepository<Place> _placeRepository;
        private IRepository<PlaceType> _placeTypeRepository;
        private IRepository<PlaceFacilitie> _placeFacilitiesRepository;
        private IRepository<Image> _imageRepository;



        public PlacesServices(IPlaceRepository repository,
                        IRepository<Place> placeRepository,
                        IRepository<PlaceType> placeTypeRepository,
                        IRepository<PlaceFacilitie> placeFacilitiesRepository,
                        IRepository<Image> imagesRepository)

        {
            _placeRepository = placeRepository;
            _repository = repository;
            _placeTypeRepository = placeTypeRepository;
            _placeFacilitiesRepository = placeFacilitiesRepository;
            _imageRepository = imagesRepository;
        }

        public ReviewSummary GetReviewSummary(int placeId)
        {
            List<Review> reviews= new List<Review>();
            try
            {
               reviews = _placeRepository.GetById(placeId).Reviews ?? new List<Review>();
            }
            catch
            {
                throw;
            }

            var result = new ReviewSummary
            {
                Count = reviews.Count
            };
            result.Avg = result.Count == 0 ? 0 : System.Math.Round( reviews.Average(x => x.Rating) , 1); 
            return result;
        }
        public List<PlaceDTO> GetTopPlaces(int topNumber = 3)
        {
            var topPlaces = _repository.GetTopPlaces(topNumber).ToList();

            var placesDTO = Mapper.Map<List<PlaceDTO>>(topPlaces);
            foreach (var item in placesDTO)
            {
                var reviewSum = GetReviewSummary(item.Id);
                item.AvgRating = reviewSum.Avg;
                item.NumberOfReviews = reviewSum.Count;
            }
            return placesDTO;
        }

        public PaginatedList<PlaceDTO> GetPaginatedList(int pageSize, int? placeType, string searchString ,int page = 0)
        {
            var allplaces = placeType.HasValue ? _repository.Get()
                                        .Where(p => p.PlaceTypeId == placeType ): _repository.Get();
            if (!String.IsNullOrEmpty(searchString))
            {
                allplaces = allplaces.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            var places = allplaces.Where(p => p.IsDeleted == false)
                .OrderByDescending(p => p.Reviews.Count > 0 ? p.Reviews.Average(t => t.Rating) : 0.0);
            var placeList = PaginatedList<Place>.Create(places, page > 0 ? page : 1, pageSize);
            var items = Mapper.Map<List<PlaceDTO>>(placeList);
            var allPlaces = new PaginatedList<PlaceDTO>(items, items.Count, page, pageSize);

            foreach (var item in allPlaces)
            {
                var result = GetReviewSummary(item.Id);
                item.NumberOfReviews = result.Count;
                item.AvgRating = result.Avg;
            }
            return allPlaces;
        }
       
              public PaginatedList<PlaceDTO> GetPlacesByUser(int pageSize, string userId, string searchString, int page = 0)
          {
            var allplaces = _repository.Get().Where(p=>p.IdUser==userId);
            if (!String.IsNullOrEmpty(searchString))
            {
                allplaces = allplaces.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            var places = allplaces.Where(p => p.IsDeleted == false)
                .OrderByDescending(p => p.Reviews.Count > 0 ? p.Reviews.Average(t => t.Rating) : 0.0);
            var placeList = PaginatedList<Place>.Create(places, page > 0 ? page : 1, pageSize);
            var items = Mapper.Map<List<PlaceDTO>>(placeList);
            var allPlaces = new PaginatedList<PlaceDTO>(items, items.Count, page, pageSize);

            foreach (var item in allPlaces)
            {
                var result = GetReviewSummary(item.Id);
                item.NumberOfReviews = result.Count;
                item.AvgRating = result.Avg;
            }
            return allPlaces;
        }
        public PlaceDetailsDTO GetPlaceDetails(int id)
        {
            var placeDetails = _repository.GetPlaceDetails(id);

            var place = Mapper.Map<PlaceDetailsDTO>(placeDetails);

            var result = GetReviewSummary(id);
            place.NumberOfReviews = result.Count;
            place.RatingAVG = result.Avg;
            var facilities = _placeFacilitiesRepository.Get().Where(p => p.PlaceId == id)
                .Select(p => new { Name = p.Facilitie.Name, Id = p.FacilitieId }).ToList();
            place.Facilities = Mapper.Map<List<FacilitiesDTO>>(facilities);

            return place;

        }

        void IPlaceServices.SavePlace(CreatePlaceDTO place)
        {
            var images = new List<Image>();
            for (int i = 0; i < place.Images.Count; i++)
            {
                var newImage = Mapper.Map<Image>(place.Images[i]);
                images.Add(newImage);
            }
            var distinctFacilities = place.Facilities.Distinct().ToList();
            var placeFacilities = new List<PlaceFacilitie>();
            for (int i = 0; i < place.Facilities.Count; i++)
            {
                var item = new PlaceFacilitie()
                {
                    FacilitieId = distinctFacilities[i].Id
                };
                placeFacilities.Add(item);
            }
            var newPlace = new Place()
            {
                Name = place.Name,
                Address = new Address()
                {
                    PostalCode = place.Address.PostalCode,
                    AdditionalInfo = place.Address.AdditionalInfo,
                    StreetID = place.Address.Street.Id,
                    StreetNumber = place.Address.StreetNumber
                },
                Images = images,
                AddedDate = DateTime.UtcNow,
                IdUser = place.IdUser,
                PlaceTypeId = place.Type,
                Telephone = place.Telephone,
                Email = place.Email,
                Website = place.Website,
                Descriprion = place.Descriprion,
                IsDeleted = false,
                PlaceFacilities = placeFacilities,


            };
              _placeRepository.Add(newPlace);
            _placeRepository.SaveChanges();
        }

    public List<PlaceTypeDTO> GetTypes()
    {
        var types = _placeTypeRepository.Get().ToList();
        var alltypes = Mapper.Map<List<PlaceTypeDTO>>(types);
        return alltypes;
    }

    public CreatePlaceDTO GetById(int id)
    {
        var placeDetails = _repository.GetPlaceById(id);
        var place = Mapper.Map<CreatePlaceDTO>(placeDetails);
        var facilities = _placeFacilitiesRepository.Get().Where(p => p.PlaceId == id)
          .Select(p => new { Name = p.Facilitie.Name, Id = p.FacilitieId }).ToList();

        place.Facilities = Mapper.Map<List<FacilitieDTO>>(facilities);
        return place;
    }

    public void UpdatePlace(CreatePlaceDTO place)
    {
        var newPlace = new Place()
        {
            Id = place.Id,
            Name = place.Name,
            IdUser = place.IdUser,
            Address = new Address()
            {
                PostalCode = place.Address.PostalCode,
                AdditionalInfo = place.Address.AdditionalInfo,
                StreetID = place.Address.Street.Id,
                StreetNumber = place.Address.StreetNumber
            },
            Images = Mapper.Map<List<Image>>(place.Images),
            ModifiedDate = DateTime.UtcNow,
            PlaceTypeId = place.Type,
            Telephone = place.Telephone,
            Email = place.Email,
            Website = place.Website,
            Descriprion = place.Descriprion,

        };
        _placeRepository.Update(newPlace);
        _placeRepository.SaveChanges();

    }
        public void DeletePlace(int id)
        {
            var place = _placeRepository.GetById(id);
            place.IsDeleted = true;

            _placeRepository.Update(place);
            _placeRepository.SaveChanges();
        }

      
    }

    public class ReviewSummary
{
    public double Avg { get; set; }
    public int Count { get; set; }
}

}

