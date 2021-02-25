using BlazorExample.Abstractions;
using BlazorExample.WebApp.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorExample.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Localization:
            builder.Services.AddSingleton<ILanguageLoader, LanguageLoader>();
            builder.Services.AddSingleton<TranslationService>();
            builder.Services.AddSingleton<ITranslationProvider>(sp => sp.GetRequiredService<TranslationService>());
            builder.Services.AddSingleton<ITranslationService>(sp => sp.GetRequiredService<TranslationService>());

            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var host = builder.Build();

            // TODO: Load initial language

            await host.RunAsync();
        }
    }
}
