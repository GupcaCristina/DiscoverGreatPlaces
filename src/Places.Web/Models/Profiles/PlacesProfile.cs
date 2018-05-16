
using Places.Domain;
using Places.DTO;
using Places.Web.Models.ViewModels;

namespace Places.Web.Models.Profiles
{
    public class PlacesProfile : AutoMapper.Profile
    {
        public PlacesProfile()
        {

            CreateMap<CreatePlaceDTO, CreatePlaceViewModel>()
                .ForMember(dest => dest.Facilities, 
                src => src.MapFrom(model => model.Facilities))
                .ForPath(dest => dest.AdditionalInfo, 
                src => src.MapFrom(model => model.Address.AdditionalInfo))
                .ForPath(dest => dest.StreetNumber, 
                src => src.MapFrom(model => model.Address.StreetNumber))
                .ForPath(dest => dest.Street, src => 
                src.MapFrom(model => model.Address.Street.Id))
                .ForPath(dest => dest.CityId, src => 
                src.MapFrom(model => model.Address.Street.City.Id))
                .ForPath(dest => dest.CountryId, src =>
                src.MapFrom(model => model.Address.Street.City.Country.Id))
                .ForPath(dest => dest.PostalCode, src =>
                src.MapFrom(model => model.Address.PostalCode));

            CreateMap<CreatePlaceViewModel, CreatePlaceDTO>()
                .ForPath(dest => dest.Address.Street.City.Country.Id,
                src => src.MapFrom(model => model.CountryId))
                 .ForPath(dest => dest.Address.Street.City.Id,
                src => src.MapFrom(model => model.CityId))
                 .ForPath(dest => dest.Address.Street.Id,
                src => src.MapFrom(model => model.Street))
                 .ForPath(dest => dest.Address.StreetNumber,
                src => src.MapFrom(model => model.StreetNumber))
                 .ForPath(dest => dest.Address.PostalCode,
                src => src.MapFrom(model => model.PostalCode))
                 .ForPath(dest => dest.Address.AdditionalInfo,
                src => src.MapFrom(model => model.AdditionalInfo))                    
                   .ForPath(dest => dest.IdUser,
                src => src.MapFrom(model => model.UserId));

            CreateMap<PlaceDTO,PlaceViewModel>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());



        }

    }
}
