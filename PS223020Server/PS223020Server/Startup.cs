using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PS223020Server.Automapper;
using PS223020Server.BusinessLogic.AutoMapperProfile;
using PS223020Server.BusinessLogic.Core.Interfaces;
using PS223020Server.BusinessLogic.Services;
using PS223020Server.DataAccess.Core.Interfaces.DbContext;
using PS223020Server.DataAccess.DbContext;

namespace PS223020Server
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
            services.AddAutoMapper(typeof(BusinessLogicProfile), typeof(MicroserviceProfile));
            services.AddDbContext<IRubicContext, RubicContext>(o => o.UseSqlite("Data Source=rubicone.db"));

            services.AddScoped<IUserService, UserService>();
            services.AddControllers();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(p => p.AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor 
                | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();

            using var scope = app.ApplicationServices.CreateScope();

            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var dbContext = scope.ServiceProvider.GetRequiredService<RubicContext>();
            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
