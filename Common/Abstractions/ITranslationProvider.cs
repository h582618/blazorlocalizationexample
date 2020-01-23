using System;

namespace BlazorExample.Abstractions
{
    public interface ITranslationProvider
    {
        event EventHandler LanguageChanged;

        string this[string name] { get; }

        string this[string name, params object[] arguments] { get; }
    }
}
