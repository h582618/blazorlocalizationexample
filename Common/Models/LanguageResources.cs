using System.Collections.Generic;

namespace BlazorExample.Models
{
    public class LanguageResources
    {
        /// <summary>
        /// Language culture code like "en-US" or "nl-NL"
        /// </summary>
        public string Language { get; set; }

        public IReadOnlyCollection<string> AvailableLanguages { get; set; }

        public IReadOnlyDictionary<string, string> Translations { get; set; }
    }
}
