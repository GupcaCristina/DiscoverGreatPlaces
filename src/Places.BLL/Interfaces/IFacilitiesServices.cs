using Places.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.BLL.Interfaces
{
    public interface IFacilitiesServices
    {
        List<FacilitiesDTO> GetFacilities();
    }
}
