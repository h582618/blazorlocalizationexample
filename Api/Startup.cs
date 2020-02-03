using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Localization;

namespace BlazorExample.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(CreateRequestLocalizationOptions());
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<WebApp.Program>();
            app.UseRequestLocalization(app.ApplicationServices.GetService<RequestLocalizationOptions>());
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<WebApp.Program>("index.html");
            });
        }

        private static RequestLocalizationOptions CreateRequestLocalizationOptions()
        {
            var supportedLanguages = new[] { new CultureInfo("nl"), new CultureInfo("en") };
            var supportedFormattingCultures = new[] { new CultureInfo("nl-NL"), new CultureInfo("en-US") };
            var result = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nl-NL", "nl"),
                SupportedCultures = supportedFormattingCultures,
                SupportedUICultures = supportedLanguages
            };

            // Now if your default browser language is English, your WebApplication will startup in English,
            // even though you set the DefaultRequestCulture to Dutch (Netherlands).
            // In most situations this is correct, but if you DO want to start in the language specified in DefaultRequestCulture
            // you can add these lines:

            //var acceptLanguageProvider = result.RequestCultureProviders.FirstOrDefault(p => p is AcceptLanguageHeaderRequestCultureProvider);
            //if (acceptLanguageProvider != null)
            //    result.RequestCultureProviders.Remove(acceptLanguageProvider);

            return result;
        }
    }
}
