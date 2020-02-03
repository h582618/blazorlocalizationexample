using BlazorExample.Abstractions;
using BlazorExample.Client.Localization;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            var host = builder.Build();

            // TODO: Load initial language

            await host.RunAsync();
        }
    }
}
