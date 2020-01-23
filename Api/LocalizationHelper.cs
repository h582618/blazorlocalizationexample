using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;
using System.Linq;

namespace BlazorExample.Api
{
    public static class LocalizationHelper
    {
        public static string GetDefaultLanguage(this RequestLocalizationOptions localizationOptions)
            => localizationOptions.DefaultRequestCulture.UICulture.Name;

        public static string GetDefaultFormattingCulture(this RequestLocalizationOptions localizationOptions)
            => localizationOptions.DefaultRequestCulture.Culture.Name;

        public static string GetLanguageFromCookie(this HttpContext httpContext)
        {
            if (httpContext == null)
                return null;

            if (!httpContext.Request.Cookies.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out var value))
                return null;

            return CookieRequestCultureProvider.ParseCookieValue(value).UICultures.FirstOrDefault().Value;
        }

        public static CultureInfo GetRequestUICulture(this HttpContext httpContext)
            => httpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;

        public static bool IsLanguageSupported(this RequestLocalizationOptions localizationOptions, string cultureName)
            => localizationOptions.SupportedUICultures.Any(l => l.Name == cultureName);

        public static void SetLanguageCookie(this HttpContext httpContext, string language, string defaultFormattingCulture)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (!httpContext.Request.Cookies.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out var value))
            {
                SetCultureCookie(httpContext, language, defaultFormattingCulture);
                return;
            }

            var formattingCulture = CookieRequestCultureProvider.ParseCookieValue(value).Cultures.FirstOrDefault().Value;
            if (string.IsNullOrEmpty(formattingCulture))
                formattingCulture = defaultFormattingCulture;

            SetCultureCookie(httpContext, language, formattingCulture);
        }

        private static void SetCultureCookie(HttpContext httpContext, string language, string formattingCulture)
        {
            var value = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(formattingCulture, language));
            var expiration = new CookieOptions { Expires = DateTime.UtcNow.AddYears(4) };
            httpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
            httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, value, expiration);
        }
    }
}
