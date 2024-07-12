using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
 

namespace public_box_api.Extention
{
    public static class HealthCheckConfig
    {
      
        public static void AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // version 1 
            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    healthQuery: "SELECT 1;",
                    name: "sql server",
                    failureStatus: HealthStatus.Degraded,
                    tags: new string[] { "db", "sql", "sqlserver" })
                //multiple db
                //.AddSqlServer(
                //    connectionString: configuration.GetConnectionString("AnotherConnection"),
                //    healthQuery: "SELECT 1;",
                //    name: "sql2",
                //    failureStatus: HealthStatus.Degraded,
                //    tags: new string[] { "db", "sql", "sqlserver" }
                //       )
                .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
                .AddCheck<MemoryHealthCheck>($"Feedback Service Memory Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback Service" })
                .AddUrlGroup(new Uri("https://localhost:44325/WeatherForecast"), name: "base URL", failureStatus: HealthStatus.Unhealthy);

            //services.AddHealthChecksUI();
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

            }).AddInMemoryStorage();
            // version 1 
            // version 2 
            //services.Configure<RemoteOptions>(options => configuration.Bind(options));

            //services
            //    .AddHealthChecksUI()
            //    .AddInMemoryStorage()
            //    //                .AddHealthChecksUI(setupSettings: settings =>
            //    //                {
            //    //                    settings
            //    //                        .AddHealthCheckEndpoint("api1", "http://localhost:8001/custom/healthz")
            //    //                        .AddWebhookNotification("webhook1", "http://webhook", "mypayload")
            //    //                        .SetEvaluationTimeInSeconds(16);
            //    //                })
            //    .Services
            //    .AddHealthChecks()
            //    .AddSqlServer(
            //            connectionString: configuration.GetConnectionString("DefaultConnection"),
            //            healthQuery: "SELECT 1;",
            //            name: "sql server",
            //            failureStatus: HealthStatus.Degraded,
            //            tags: new string[] { "db", "sql", "sqlserver" }
            //                )
            //    .AddUrlGroup(new Uri("http://httpbin.org/status/200"), name: "uri-1")
            //    .AddUrlGroup(new Uri("http://httpbin.org/status/200"), name: "uri-2")
            //    .AddUrlGroup(
            //        sp =>
            //        {
            //            var remoteOptions = sp.GetRequiredService<IOptions<RemoteOptions>>().Value;
            //            return remoteOptions.RemoteDependency;
            //        },
            //        "uri-3")
            //    .AddUrlGroup(new Uri("http://httpbin.org/status/500"), name: "uri-4")
            //    .Services

            //    .AddControllers();
            // version 2 
            // version 3

            services
           //.AddDemoAuthentication()
           .AddHealthChecks()
           //.AddProcessAllocatedMemoryHealthCheck(maximumMegabytesAllocated: 100, tags: new[] { "process", "memory" })
           .AddCheck<RandomHealthCheck>("random1", tags: new[] { "random" })
           .AddCheck<RandomHealthCheck>("random2", tags: new[] { "random" })
           .Services
           .AddHealthChecksUI(setupSettings: setup =>
           {
               setup.SetHeaderText("Branding Demo - Health Checks Status");
               setup.AddHealthCheckEndpoint("endpoint1", "/health-random");
               setup.AddHealthCheckEndpoint("endpoint2", "health-process");
               //Webhook endpoint with custom notification hours, and custom failure and description messages

               setup.AddWebhookNotification("webhook1", uri: "https://healthchecks2.requestcatcher.com/",
                       payload: "{ message: \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
                           restorePayload: "{ message: \"[[LIVENESS]] is back to life\"}",
                           shouldNotifyFunc: (livenessName, report) => DateTime.UtcNow.Hour >= 8 && DateTime.UtcNow.Hour <= 23,
                           customMessageFunc: (livenessName, report) =>
                           {
                               var failing = report.Entries.Where(e => e.Value.Status == UIHealthStatus.Unhealthy);
                               return $"{failing.Count()} healthchecks are failing";
                           }, customDescriptionFunc: (livenessName, report) =>
                           {
                               var failing = report.Entries.Where(e => e.Value.Status == UIHealthStatus.Unhealthy);
                               return $"{string.Join(" - ", failing.Select(f => f.Key))} healthchecks are failing";
                           });

               //Webhook endpoint with default failure and description messages

               setup.AddWebhookNotification(
                   name: "webhook1",
                   uri: "https://healthchecks.requestcatcher.com/",
                   payload: "{ message: \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
                   restorePayload: "{ message: \"[[LIVENESS]] is back to life\"}");
           }).AddInMemoryStorage()
             .Services
           .AddControllers();
            // version 3
        }
        public static void UseHealthChecksSetup(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            // version 2 
            //app.UseRouting()
            //.UseEndpoints(config =>
            //{
            //    config.MapHealthChecks("/healthz", new HealthCheckOptions
            //    {
            //        Predicate = _ => true,
            //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    });

            //    config.MapHealthChecksUI(setup =>
            //    {
            //        setup.UIPath = "/show-health-ui"; // this is ui path in your browser
            //        setup.ApiPath = "/health-ui-api"; // the UI ( spa app )  use this path to get information from the store ( this is NOT the healthz path, is internal ui api )
            //        setup.PageTitle = "My wonderful Health Checks UI"; // the page title in <head>
            //    });

            //    config.MapDefaultControllerRoute();
            //});
            // version 2 
            // version 1 
            //app.UseHealthChecksUI(delegate (HealthChecks.UI.Configuration.Options options)
            //{
            //    options.UIPath = "/healthcheck-ui";
            //    options.AddCustomStylesheet("./assets/css/HealthCheck.css");

            //});
            // version 1 
            // version 3
            app.UseRouting()
         .UseEndpoints(config =>
         {
             config.MapHealthChecks("/health-random", new HealthCheckOptions
             {
                 Predicate = r => r.Tags.Contains("random"),
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });

             config.MapHealthChecks("/health-process", new HealthCheckOptions
             {
                 Predicate = r => r.Tags.Contains("process"),
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });

             config.MapHealthChecksUI(setup => setup.AddCustomStylesheet("./assets/css/HealthCheck.css"));

             config.MapDefaultControllerRoute();
         });
            // version 3
        }

    }

    public class RemoteHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RemoteHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("https://api.ipify.org");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy($"Remote endpoints is healthy.");
                }

                return HealthCheckResult.Unhealthy("Remote endpoint is unhealthy");
            }
        }
    }

    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> _options;

        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            _options = options;
        }

        public string Name => "memory_check";

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var options = _options.Get(context.Registration.Name);

            // Include GC information in the reported diagnostics.
            var allocated = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
        {
            { "AllocatedBytes", allocated },
            { "Gen0Collections", GC.CollectionCount(0) },
            { "Gen1Collections", GC.CollectionCount(1) },
            { "Gen2Collections", GC.CollectionCount(2) },
        };
            var status = (allocated < options.Threshold) ? HealthStatus.Healthy : HealthStatus.Unhealthy;

            return Task.FromResult(new HealthCheckResult(
                status,
                description: "Reports degraded status if allocated bytes " +
                    $">= {options.Threshold} bytes.",
                exception: null,
                data: data));
        }
    }
    public class MemoryCheckOptions
    {
        public string Memorystatus { get; set; }
        //public int Threshold { get; set; }
        // Failure threshold (in bytes)
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }
    public class RemoteOptions
    {
        public Uri RemoteDependency { get; set; } = null!;
    }
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDemoAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://demo.identityserver.io";
                    options.Audience = "api";

                }).Services
                  .AddAuthorization(configure => configure.AddPolicy("AuthUserPolicy", config => config.RequireAuthenticatedUser()));
        }
    }
    public class RandomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Minute % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(description: $"The healthcheck {context.Registration.Name} failed at minute {DateTime.UtcNow.Minute}"));
        }
    }
}
