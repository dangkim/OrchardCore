using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCore.SearchA.Settings
{
    public class SearchAPartSettingsViewModel
    {
        public string Pattern { get; set; }

        [BindNever]
        public SearchAPartSettings SearchAPartSettings { get; set; }
    }
}
