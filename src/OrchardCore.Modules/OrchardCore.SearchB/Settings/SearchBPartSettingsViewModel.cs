using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCore.SearchB.Settings
{
    public class SearchBPartSettingsViewModel
    {
        public string Pattern { get; set; }

        [BindNever]
        public SearchBPartSettings SearchBPartSettings { get; set; }
    }
}
