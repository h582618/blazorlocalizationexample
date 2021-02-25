    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using System.Linq;
    using BlazorExample.Models;
    using BlazorExample.ResourceLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using BlazorAppTest.Shared;
using Newtonsoft.Json;
using System;

namespace BlazorExample.Api.Controllers
    {

        [Route("api/[controller]")]
        [ApiController]
        public class SettingsController : ControllerBase
        {
            private readonly RequestLocalizationOptions _localizationOptions;
            private readonly IStringLocalizer _localizer;

            public SettingsController(IStringLocalizer<SharedResource> localizer, RequestLocalizationOptions localizationOptions)
            {
                _localizer = localizer;
                _localizationOptions = localizationOptions;
            }

            // GET api/settings/language
            [HttpGet("language")]
            public async Task<LanguageResources> GetLanguageAsync()
            {
                List<DataLang> dataLangs = await GetData();
                
                var langCode = HttpContext.GetRequestUICulture().Name;
                String language = "";

                switch(langCode)
                {
                  case "no":
                    language = "Norsk";
                    break;
                  case "en":
                    language = "English";
                    break;

                }

                List<DataLang> languages = dataLangs.Where(x => x.language == language).ToList();

                return new LanguageResources
                {
                    Language = langCode,
                    AvailableLanguages = _localizationOptions.SupportedUICultures.Select(l => l.Name).ToList(),
                    Translations = languages.ToDictionary(ls => ls.name, ls => ls.value)
                };
            }

            // POST api/settings/language
            [HttpPost("language")]
            public IActionResult SetLanguage(LanguageModel model)
            {
                // If model.CultureName == null use current language cookie or create one with the default language:
                if (string.IsNullOrEmpty(model.CultureName))
                {
                    // Check for valid language cookie:
                    var cultureName = HttpContext.GetLanguageFromCookie();
                    if (_localizationOptions.IsLanguageSupported(cultureName))
                        return Ok();

                    model.CultureName = _localizationOptions.GetDefaultLanguage();
                }

                if (!_localizationOptions.IsLanguageSupported(model.CultureName))
                    return BadRequest($"Unsupported language: {model.CultureName}");

                HttpContext.SetLanguageCookie(model.CultureName, _localizationOptions.GetDefaultFormattingCulture());
                return Ok();
            }
            public async Task<List<DataLang>> GetData()
            {
            var httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync("https://languageapi.azurewebsites.net/api/Data");

            string result1 = await response.Content.ReadAsStringAsync();

            List<DataLang> data1 = JsonConvert.DeserializeObject<List<DataLang>>(result1);

            return data1;
            }
    }
    }
