using Microsoft.Extensions.Configuration;

namespace public_box_api.Extention
{
    public static class SecurityConfig
    {
        public static void UseSecuirtySetup(this IApplicationBuilder app, IConfiguration configuratio)
        {
            app.UseReferrerPolicy(options => options.NoReferrer()); // Referrer-Policy Http Reader
            app.UseXContentTypeOptions();  // X-Content-Type-Options Header
            app.UseXXssProtection(options => options.EnabledWithBlockMode());   // X- XSS - Protection Header
            app.UseXfo(options => options.Deny());  // X-Frame-Options Header
            var hosts = configuratio.GetSection("SecurityFeature:Feature-UseCors").Get<List<string>>();
            //_configuration["SecurityFeature:Feature-UseCors"]

            app.UseCors(options => options.WithOrigins(hosts.ToArray()).AllowAnyHeader().AllowAnyMethod());

            app.UseCsp(options => options   // [CSP] Content-Security-Policy Header
                    .BlockAllMixedContent()
                    .DefaultSources(s => s.Self())
                    .ScriptSources(s => s.Self().UnsafeInline().CustomSources(configuratio["SecurityFeature:Feature-ScriptSources"]))
                    .StyleSources(s => s.Self())
                    .StyleSources(s => s.UnsafeInline())
                    .StyleSources(s => s.CustomSources(configuratio["SecurityFeature:Feature-StyleSources"]))
                    .FontSources(s => s.Self())
                    .FontSources(s => s.CustomSources(configuratio["SecurityFeature:Feature-FontSources"]))
                );

            app.Use(async (context, next) =>
            {
                if (!context.Response.Headers.ContainsKey("Feature-Policy"))
                {
                    context.Response.Headers.Add("Feature-Policy", configuratio["SecurityFeature:Feature-Policy"]);
                }

                await next();
            });
        }
    }
}
