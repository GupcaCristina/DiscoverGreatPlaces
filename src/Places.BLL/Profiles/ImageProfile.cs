using AutoMapper;
using Places.Domain;
using Places.DTO;


namespace Places.Web.Models.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {

            CreateMap<ImageDTO, Image>()
                   .ForPath(dest => dest.Place, opt => opt.Ignore());
        }
    }
}
