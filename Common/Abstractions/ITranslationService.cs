using System.Collections.Generic;
using System.Globalization;

namespace BlazorExample.Abstractions
{
    public interface ITranslationService
    {
        IReadOnlyCollection<CultureInfo> AvailableLanguages { get; }

        CultureInfo SelectedLanguage { get; }

        void ChangeLanguage(string languageCultureName);
    }
}
