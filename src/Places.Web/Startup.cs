using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Places.Web.Services;
using Places.DAL.EF;
using Places.Domain;
using AutoMapper;
using Places.DAL.Interfaces;
using Places.DAL.Repositories;
using Places.BLL.Interfaces;
using Places.BLL;
using Places.BLL.Services.Interfaces;
using Places.BLL.Services;
using Places.DAL.Iterfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Places.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //---------Localization-------------
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            //------------------------------------------

            services.AddDbContext<PlacesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PlacesContext>()
                .AddDefaultTokenProviders();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ro-RO")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            //---------External Authentication----------------
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "1947511898615267";
                facebookOptions.AppSecret = "99666ca2286034794b989c1ec0e1e911";
            });
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "365159387896-n3vh4pfst55gh0s0burtujficj1aebl5.apps.googleusercontent.com";
                googleOptions.ClientSecret = "fdHqW6GfNr92DXgzabYa0X8S";
            });

            //--------Services----------------------
            services.AddScoped<DbContext, PlacesContext>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IAddressServices, AddressServices>();
            services.AddScoped<IImageServices, ImageServices>();
            services.AddScoped<IPlaceServices, PlacesServices>();
            services.AddScoped<IReviewsServices, ReviewsServices>();
            services.AddScoped<IFacilitiesServices, FacilitiesServices>();
         
            services.AddScoped<IWorkScheduleServices, WorkScheduleServices>();
            services.AddScoped(typeof(DAL.Interfaces.IRepository<>), typeof(DAL.Repositories.IRepository<>));
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<WorkScheduleRepository, WorkScheduleRepository>();
            
       
        
            services.AddAutoMapper();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //----------Localization-----------------------
          
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(locOptions.Value);
            //------------------------------------------------------
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {          
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");
         
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //-----------
     

        }
    }
}
