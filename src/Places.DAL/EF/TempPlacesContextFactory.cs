using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;

namespace Places.DAL.EF

{
    public class TempPlacesContextFactory : IDesignTimeDbContextFactory<PlacesContext>
    {
        public PlacesContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PlacesContext>();
            DbContextOptions<PlacesContext> options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new PlacesContext(options);
        }
    }
}