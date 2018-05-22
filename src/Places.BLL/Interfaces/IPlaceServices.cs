using DomainUtil;
using Places.DTO;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Places.BLL.Interfaces
{
    public interface IPlaceServices
    {
        List<PlaceDTO> GetTopPlaces(int topNumber = 3);
        PlaceDetailsDTO GetPlaceDetails(int id);
        PaginatedList<PlaceDTO> GetPaginatedList(int pageSize, int? placeType  , string searchString, int page );
        List<PlaceDTO> GetPlacesByUser(string userId);
        List<PlaceTypeDTO> GetTypes();
        void SavePlace(CreatePlaceDTO place);
        CreatePlaceDTO GetById(int id);
        void UpdatePlace(CreatePlaceDTO placeViewModel);
        void DeletePlace(int id);
    }
}
