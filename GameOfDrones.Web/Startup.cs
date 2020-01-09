using GameOfDrones.Core.Interfaces;
using GameOfDrones.Core.Services;
using GameOfDrones.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace GameOfDrones.Web
{
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
            //
            services.AddScoped(typeof(IDbContext), typeof(GameDbContext));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILogService, LogService>();
            //
            services.AddCors();
            //
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkSqlServer();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GameDbContext>(options => options.UseSqlServer(connectionString)); 

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void CreateDatabase(string connectionString, string collation = null)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            var masterCatalogConnectionString = builder.ToString();
            var query = $"CREATE DATABASE [{databaseName}]";

            if (!string.IsNullOrWhiteSpace(collation))
            {
                query = $"{query} COLLATE {collation}";
            } 

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
