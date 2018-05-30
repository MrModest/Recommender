using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Recommender.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Recommender
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                                .SetBasePath(environment.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
                                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TMDbContext>(options =>
                options.UseNpgsql(
                    Configuration["Data:Movies:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(
                    Configuration["Data:Identity:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IMovieRepository,    EFMovieRepository>();
            services.AddTransient<IGenreRepository,    EFGenreRepository>();
            services.AddTransient<IUserRateRepository, EFUserRateRepository>();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Movie}/{action=Index}/{id?}");
            });
        }
    }
}