//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Facilitie> Facilities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }

        public virtual DbSet<EventLog> EventLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<PlaceFacilitie>()
                 .HasKey(t => (new { t.PlaceId, t.FacilitieId }));

            modelBuilder.Entity<PlaceFacilitie>()
                .HasOne(pt => pt.Place)
                .WithMany(p => p.PlaceFacilities)
                .HasForeignKey(pt => pt.PlaceId);

            modelBuilder.Entity<PlaceFacilitie>()
                .HasOne(pt => pt.Facilitie)
                .WithMany(t => t.PlaceFacilities)
                .HasForeignKey(pt => pt.FacilitieId);

        }
    }
}