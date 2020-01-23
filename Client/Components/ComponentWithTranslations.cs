using BlazorExample.Abstractions;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorExample.Client.Components
{
    public abstract class ComponentWithTranslations : ComponentBase, IDisposable
    {
        [Inject]
        protected ITranslationProvider Translation { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// If you override this method, always call: base.Dispose(disposing);
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Translation.LanguageChanged -= OnLanguageChanged;
        }

        protected override void OnInitialized()
        {
            Translation.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
            => StateHasChanged();
    }
}
