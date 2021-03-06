using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium2_poprawa.Models;
using Kolokwium2_poprawa.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kolokwium2_poprawa
{
    public class Startup
    {
        //EfCommand: Scaffold-DbContext 'Data Source=db-mssql;Initial Catalog=s18731;Integrated Security=True' Microsoft.EntityFrameworkCore.SqlServer -Tables Artist,Artist_Event,Event,Event_Organiser,Organiser -OutputDir Models
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbService, EfArtistDbService>();
            services.AddDbContext<s18731Context>(opt =>
            {
                opt.UseSqlServer("Data Source=db-mssql;Initial Catalog=s18731;Integrated Security=True");
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
