using Places.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Places.DAL.EF

{
    public class PlacesContext : IdentityDbContext
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



            //modelBuilder.Entity<ApplicationUser>().ToTable("User", "dbo");
            //modelBuilder.Entity<IdentityRole>().ToTable("Role", "dbo");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");



        }
    }
}