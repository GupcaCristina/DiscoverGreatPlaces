using AutoMapper;
using Places.Domain;
using Places.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.BLL.Profiles
{
    public class PlacesProfile : Profile
    {
        public PlacesProfile()
        {
            CreateMap<Place, PlaceDetailsDTO>()
               .ForMember(dest => dest.NumberOfReviews, opt => opt.Ignore())
               .ForMember(dest => dest.Facilities, opt => opt.Ignore())
               .ForMember(dest => dest.RatingAVG, opt => opt.Ignore());
            CreateMap<Place, CreatePlaceDTO>()
                .ForMember(dest => dest.Facilities, opt => opt.Ignore())
                .ForPath(dest => dest.Address.AdditionalInfo, src => src.MapFrom(model => model.Address.AdditionalInfo))
                .ForPath(dest => dest.Address.PostalCode, src => src.MapFrom(model => model.Address.PostalCode))
                .ForPath(dest => dest.Address.Id, src => src.MapFrom(model => model.Address.Id))
                .ForPath(dest => dest.Address.StreetNumber, src => src.MapFrom(model => model.Address.StreetNumber))
                .ForPath(dest => dest.Address.Street.Name, src => src.MapFrom(model => model.Address.Street.Name))
                .ForPath(dest => dest.Address.Street.Id, src => src.MapFrom(model => model.Address.Street.Id))
                .ForPath(dest => dest.Address.Street.City.Name, src => src.MapFrom(model => model.Address.Street.City.Name))
                .ForPath(dest => dest.Address.Street.City.Id, src => src.MapFrom(model => model.Address.Street.City.Id))
                .ForPath(dest => dest.Address.Street.City.Country.Name, src => src.MapFrom(model => model.Address.Street.City.Country.Name))
                .ForPath(dest => dest.Address.Street.City.Country.Id, src => src.MapFrom(model => model.Address.Street.City.Country.Id))
                .ForPath(dest => dest.Type, src => src.MapFrom(model => model.Type.Id))
                .ForMember(dest => dest.Descriprion, src => src.MapFrom(model => model.Descriprion));

           CreateMap<Place, PlaceDTO>()
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address));
                CreateMap<CreatePlaceDTO, Place>()
               .ForPath(dest => dest.AddedDate, opt => opt.Ignore())
               .ForPath(dest => dest.ModifiedDate, opt => opt.Ignore())
               .ForPath(dest => dest.PlaceFacilities, opt => opt.Ignore());

        }




    }
}

