# Blazor WebAssembly Localization

See [Knowledge Base article](http://www.forestbrook.net/docs/blazor/localization.html) for detailed description.

This solution:
* Is intended for [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models#blazor-webassembly). 
* Reads the translations from your Api when the Blazor app is started and when the language is changed.
* Can be used with translations in **.resx files**, in a **translations database** or with any **other source of translations**.
* Using translations on a page is just as easy as inserting **`@Translation["Welcome"]`**.
* When the language is changed, pages with translations are automatically updated.
* Implements a `<SelectLanguage />` Razor component with a drop-down list to select the language.
* The selected language code is stored in an ASP.NET Culture Cookie, but if you prefer you can also store the selected language code in the browsers local storage and use a `ui-culture` query parameter or an Accept-Language header in the Api calls.

