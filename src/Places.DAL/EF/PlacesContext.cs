//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Places.Domain;

namespace Places.DAL.EF

{
    public class PlacesContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {

        public PlacesContext(DbContextOptions<PlacesContext> options) : base(options)
        {

        }
        public DbSet<Place> Places { get; set; }


        public DbSet<Post> Posts { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }

        public DbSet<Facilitie> Facilities { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Menu> Menus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Pizzeria>().HasBaseType<Place>();
            modelBuilder.Entity<Restaurant>().HasBaseType<Place>();
            modelBuilder.Entity<Pub>().HasBaseType<Place>();
            modelBuilder.Entity<FastFood>().HasBaseType<Place>();

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");             
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");

            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
     
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");


            });

        }
    }
}