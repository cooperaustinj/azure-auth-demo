using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureAuthDemo.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            .AddAzureAD(options => config.Bind("AzureAd", options));

            // services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            // {
            //     options.Authority = options.Authority + "/v2.0/";
            //     options.TokenValidationParameters.ValidateIssuer = true;
            // });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapGet("/private", async context =>
                    {
                        await context.Response.WriteAsync("Private page");
                    })
                    .RequireAuthorization();
                endpoints
                    .MapControllers();
            });
        }
    }
}
