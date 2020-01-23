using BlazorExample.Abstractions;
using BlazorExample.Client.Localization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorExample.WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILanguageLoader, LanguageLoader>();
            services.AddSingleton<TranslationService>();
            services.AddSingleton<ITranslationProvider>(sp => sp.GetRequiredService<TranslationService>());
            services.AddSingleton<ITranslationService>(sp => sp.GetRequiredService<TranslationService>());
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
