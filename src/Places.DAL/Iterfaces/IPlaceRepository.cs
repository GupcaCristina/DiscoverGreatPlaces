using Places.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Places.DAL.Interfaces
{
    public interface IPlaceRepository
    {
        Place GetPlaceDetails(int id);

        List<Place> GetTopPlaces(int topNumber);

        List<Facilitie> GetFacilities(int id);
        Place GetPlaceById(int id);

        IQueryable<Place> Get();

   





    }
}
