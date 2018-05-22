using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AutoMapper;
using Places.BLL.Profiles;
using Places.Web.Models.Profiles;

namespace Web.Controllers.Test
{
    public class HomeControllerTest
    {
        private readonly MapperConfiguration _config =
            new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new PlacesProfile());
                    cfg.AddProfile(new ImageProfile());
                    cfg.AddProfile(new AddressProfile());
                    cfg.AddProfile(new Places.Web.Models.Profiles.PlacesProfile());
                });
        private IMapper _mapper;
        public HomeControllerTest()
        {
            _mapper = _config.CreateMapper();
        }
    }
}
