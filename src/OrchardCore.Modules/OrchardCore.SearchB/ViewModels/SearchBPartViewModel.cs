using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.SearchB.Models;
using OrchardCore.SearchB.Settings;

namespace OrchardCore.SearchB.ViewModels
{
    public class SearchBPartViewModel
    {
        public string SearchB { get; set; }

        [BindNever]
        public SearchBPart SearchBPart { get; set; }

        [BindNever]
        public SearchBPartSettings Settings { get; set; }
    }
}
