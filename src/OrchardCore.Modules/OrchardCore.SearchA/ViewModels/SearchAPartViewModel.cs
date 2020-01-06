using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.SearchA.Models;
using OrchardCore.SearchA.Settings;

namespace OrchardCore.SearchA.ViewModels
{
    public class SearchAPartViewModel
    {
        public string SearchA { get; set; }

        [BindNever]
        public SearchAPart SearchAPart { get; set; }

        [BindNever]
        public SearchAPartSettings Settings { get; set; }
    }
}
