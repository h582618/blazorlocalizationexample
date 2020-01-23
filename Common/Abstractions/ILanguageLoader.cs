using BlazorExample.Models;
using System;

namespace BlazorExample.Abstractions
{
    public interface ILanguageLoader
    {
        void StartLoadLanguage(string languageCultureName, Action<LanguageResources> setLanguage);
    }
}
