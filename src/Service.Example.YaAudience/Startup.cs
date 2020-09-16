using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Service.Example.YaAudience.Extensions;
using Service.Example.YaAudience.Middlewares;

namespace Service.Example.YaAudience
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
            services.AddSerilogLogging(Configuration);
            services.AddSwagger();

            services.AddControllers()
              .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
              .AddNewtonsoftJson(o =>
              {
                  o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                  o.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
              });
            
            services.RegisterDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var basePath = Configuration["pathBase"];
            app.UsePathBase(basePath);

            app.UseMiddleware<SerilogMiddleware>();            
            app.UseSwagger(basePath);

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(e => e.MapControllers());
        }
    }
}
