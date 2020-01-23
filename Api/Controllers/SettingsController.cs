    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using System.Linq;
    using BlazorExample.Models;
    using BlazorExample.ResourceLibrary;

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
            public LanguageResources GetLanguage()
            {
                return new LanguageResources
                {
                    Language = HttpContext.GetRequestUICulture().Name,
                    AvailableLanguages = _localizationOptions.SupportedUICultures.Select(l => l.Name).ToList(),
                    Translations = _localizer.GetAllStrings(true).ToDictionary(ls => ls.Name, ls => ls.Value)
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
        }
    }
