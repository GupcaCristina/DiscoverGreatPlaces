using AutoMapper;
using Places.DTO;
using Places.Web.Models.ViewModels;

namespace Places.Web.Models.Profiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {

            CreateMap<AddressDTO, AddressViewModel>()
                .ForMember(x => x.PostalCode, opt => opt.MapFrom(model => model.PostalCode))
                .ForMember(x => x.Street, opt => opt.MapFrom(model => model.Street.Name))
                .ForMember(x => x.City, opt => opt.MapFrom(model => model.Street.City.Name))
                .ForMember(x => x.Country, opt => opt.MapFrom(model => model.Street.City.Country.Name))
                .ForMember(x => x.AdditionalInfo, opt => opt.MapFrom(model => model.AdditionalInfo));

        }

    }
}
