using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Places.Domain

{

    public class ApplicationUser : IdentityUser
    {
        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Place> Places { get; set; } = new List<Place>();

    }
}
