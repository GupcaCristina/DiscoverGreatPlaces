using Microsoft.AspNetCore.Identity;
using Moq;
using Places.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Places.Test.Web.Test.Controllers
{
    public static class UserManagerProvider
    {
       public static Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
