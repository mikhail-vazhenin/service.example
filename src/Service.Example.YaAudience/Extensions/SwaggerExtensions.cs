using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Example.YaAudience.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Service.Example.YaAudience.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApplicationVersion.ProjectName,
                    Version = ApplicationVersion.InformationalVersion,

                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwagger(this IApplicationBuilder app, string basePath)
        {
            SwaggerBuilderExtensions.UseSwagger(app, c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" } };
                });
            });
            app.UseSwaggerUI(options =>
            {
                var swaggerPath = $"{basePath}/swagger/v1/swagger.json";

                options.SwaggerEndpoint(swaggerPath, ApplicationVersion.ProjectName);
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
