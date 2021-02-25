using BlazorExample.Abstractions;
using BlazorExample.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorExample.WebApp.Localization
{
    public class LanguageLoader : ILanguageLoader
    {
        private const string LanguageApiRequestUri = "api/settings/language";
        private readonly HttpClient _httpClient;

        public LanguageLoader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void StartLoadLanguage(string languageCultureName, Action<LanguageResources> setLanguage)
        {
            var languageResources = await SelectAndLoadLanguage(languageCultureName);
            setLanguage(languageResources);
        }

        private async Task<LanguageResources> SelectAndLoadLanguage(string languageCultureName)
        {
            // Post the selected language (null to use current default) to update the language cookie:
            await _httpClient.PostAsJsonAsync(LanguageApiRequestUri, new LanguageModel { CultureName = languageCultureName }).ConfigureAwait(false);

            // Load the selected language texts:
            return await _httpClient.GetFromJsonAsync<LanguageResources>(LanguageApiRequestUri).ConfigureAwait(false);
        }
    }
}
