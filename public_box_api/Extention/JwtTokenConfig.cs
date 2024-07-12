﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using public_box_api.application.DTOs.Configuration.Shared;
using public_box_api.Extention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Xml.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using public_box_api.application.Wrappers;
using Microsoft.Extensions.Options;

namespace public_box_api.Extention
{
    public static class JwtTokenConfig
    {
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
              
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                    .AddJwtBearer(x =>
                                    {
                                        x.TokenValidationParameters = new TokenValidationParameters()
                                        {
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                                            ValidateIssuer = false,
                                            ValidateAudience = false,
                                            ValidateLifetime = true,
                                            ValidIssuer = configuration["JWTSettings:Issuer"],
                                            ValidAudience = configuration["JWTSettings:Audience"],
                                            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                                            ClockSkew = TimeSpan.Zero,
                                        };
                                        x.RequireHttpsMetadata = false;
                                        x.SaveToken = true;
                                        x.Events = new JwtBearerEvents()
                                        {
                                            OnAuthenticationFailed = c =>
                                            {
                                                c.NoResult();
                                                c.Response.StatusCode = 500;
                                                c.Response.ContentType = "text/plain";
                                                return c.Response.WriteAsync(c.Exception.ToString());
                                            },
                                            OnChallenge = context =>
                                            {
                                                if (!context.Response.HasStarted)
                                                {
                                                    context.Response.StatusCode = 401;
                                                    context.Response.ContentType = "application/json";
                                                    context.HandleResponse();
                                                    var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                                                    return context.Response.WriteAsync(result);
                                                }
                                                else
                                                {
                                                    var result = JsonConvert.SerializeObject(new Response<string>("Token Expired"));
                                                    return context.Response.WriteAsync(result);
                                                }
                                            },
                                            OnForbidden = context =>
                                            {
                                                context.Response.StatusCode = 403;
                                                context.Response.ContentType = "application/json";
                                                var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                                                return context.Response.WriteAsync(result);
                                            },
                                        };
                                    });
            return services;
        }
    }
   
}
 
