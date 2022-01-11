using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace FeedbackService.Api
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddVersionedApiExplorer(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    // options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description);                    
                }

                string xmlFile = $"{typeof(SwaggerConfiguration).Assembly.GetName().Name}.xml";

                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.ActionDescriptor.RouteValues["action"]}");
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }            
            
            app.UseSwagger(options => options.RouteTemplate = $"swagger/{ApiConstants.ServiceName}/{{documentName}}/swagger.json");

            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = $"swagger/feedbackservice";

                    // build a swagger endpoint for each discovered API version
                    foreach (ApiVersionDescription desc in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                    }
                });

            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentService.Api v1"));
            
            return app;
        }

        //private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        //{
        //    string serviceDescription = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ServiceDescription.md"));
        //    var info = new OpenApiInfo
        //}
    }
}
