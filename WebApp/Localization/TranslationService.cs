using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BlazorExample.Abstractions;
using BlazorExample.Models;

namespace BlazorExample.WebApp.Localization
{
    public class TranslationService : ITranslationProvider, ITranslationService
    {
        private readonly ILanguageLoader _languageLoader;
        private IReadOnlyDictionary<string, string> _translations;

        public TranslationService(ILanguageLoader languageLoader)
        {
            _languageLoader = languageLoader;
            _languageLoader.StartLoadLanguage(null, LoadLanguage);
        }

        public event EventHandler LanguageChanged;

        public IReadOnlyCollection<CultureInfo> AvailableLanguages { get; private set; }

        public CultureInfo SelectedLanguage { get; private set; } = CultureInfo.InvariantCulture;

        public string this[string name] => GetString(name);

        public string this[string name, params object[] arguments] => GetString(name, arguments);

        public void ChangeLanguage(string languageCultureName)
        {
            if (string.IsNullOrEmpty(languageCultureName)) throw new ArgumentNullException(nameof(languageCultureName));
            if (AvailableLanguages.All(ci => ci.Name != languageCultureName)) throw new ArgumentException($"Unsupported language: {languageCultureName}");
            if (languageCultureName != SelectedLanguage.Name)
                _languageLoader.StartLoadLanguage(languageCultureName, LoadLanguage);
        }

        private void LoadLanguage(LanguageResources resources)
        {
            _translations = resources.Translations;
            SelectedLanguage = new CultureInfo(resources.Language);
            AvailableLanguages = resources.AvailableLanguages.Select(n => new CultureInfo(n)).ToList();
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }

        private string GetString(string name, params object[] arguments)
        {
            if (_translations == null || !_translations.TryGetValue(name, out var value))
                value = name;

            if (arguments.Length > 0)
                value = string.Format(SelectedLanguage, value, arguments);

            return value;
        }
    }
}
