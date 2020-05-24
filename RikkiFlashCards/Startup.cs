using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AnkiFlashCards.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnkiFlashCards.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AnkiFlashCards
{
    /*
     * everytime we publish, change Web config to use "outofprocess" hostingModel
     * <aspNetCore processPath="dotnet" arguments=".\AnkiFlashCards.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="outofprocess" />
     */
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RikkiFlashCardsDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySQLConnection")));



            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<RikkiFlashCardsDbContext>();
            services.AddControllersWithViews();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IDeckService, DeckService>();
            services.AddRazorPages();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc(mvc => mvc.EnableEndpointRouting = false);

            //session
            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Subject}/{action=Index}");
            });
        }
    }
}
