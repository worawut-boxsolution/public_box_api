using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using public_box_api.Extention;
using public_box_api.Middlewares;
using public_box_api.application;
using public_box_api.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Xml;


namespace public_box_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            //services.AddPersistenceInfrastructure(Configuration);
            services.AddSharedInfrastructure(Configuration);
            services.AddSharedApplication(Configuration);
            services.AddControllers();
            services.AddJwtTokenAuthentication(Configuration);
            services.AddApiVersioningExtension();
            services.AddSwaggerConfiguration(Configuration);
           
            services.AddCors();
            //services.AddHealthChecks().AddCheck("sample_health_check", () => HealthCheckResult.Healthy("Sample check is healthy.")); ;
            //services.AddHealthChecksUI();
            services.AddHealthChecksConfiguration(Configuration);


			//Add log4net config
		 
			//XmlDocument log4netConfig = new XmlDocument();
			//log4netConfig.Load(File.OpenRead("log4net.config"));
			//var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
			//		   typeof(log4net.Repository.Hierarchy.Hierarchy));
			//log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

			//Add log4net config
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());

            app.UseSwaggerSetup(Configuration, env);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHealthChecksSetup(Configuration, env);
            //app.UseHealthChecks("/health");
            //app.UseHealthChecksUI(options => options.UIPath = "/health-ui");
            //// HealthCheck middleware
            //app.UseHealthChecks("/hc", new HealthCheckOptions()
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.Run();
        }
    }
}
 
