using AutoMapper;
using Places.BLL.Interfaces;
using Places.DAL.Interfaces;
using Places.Domain;
using Places.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Places.BLL.Services
{
    public class FacilitiesServices: IFacilitiesServices
    {
        IRepository<Facilitie> _facilitiesRepository;
        public FacilitiesServices(IRepository<Facilitie> facilitiesRepository)
        {
            _facilitiesRepository = facilitiesRepository;
        }

        public List<FacilitiesDTO> GetFacilities()
        {
            var facilities = _facilitiesRepository.Get().ToList();
            var result = Mapper.Map<List<FacilitiesDTO>>(facilities);
            return result;
        }
    }
}
