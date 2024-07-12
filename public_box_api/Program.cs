
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using public_box_api;



var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();
//app.MapHealthChecks("/api/health", new HealthCheckOptions()
//{
//    Predicate = _ => true,
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});
startup.Configure(app, builder.Environment); // calling Configure method
app.Run();
                                             // Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var configurations  = builder.Configuration;
//var service_provider = builder.Services.BuildServiceProvider();
//// This method gets called by the runtime. Use this method to add services to the container




//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

//// This method gets called by the runtime. Use this method to configure the HTTP request pipeline

//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseDeveloperExceptionPage();
//}
//app.UseCors(builder => builder.AllowAnyOrigin()
//                           .AllowAnyHeader()
//                           .AllowAnyMethod());

