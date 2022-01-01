using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackService.Core.Services;
using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FeedbackService.Api;

namespace FeedbackService
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
            // Configure Cors.
            services.ConfigureCors();

            // Configure Dependency Injection.
            services.ConfigureDependencyInjection(Configuration);

            services.AddControllers();

            // Configure swagger
            services.ConfigureSwagger();

            // Configure Automapper.
            services.AddAutoMapper(typeof(Startup));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.ConfigureSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
