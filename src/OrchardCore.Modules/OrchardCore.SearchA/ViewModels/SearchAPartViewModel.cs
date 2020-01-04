using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.SearchA.Model;

namespace OrchardCore.SearchA.ViewModels
{
    public class SearchAPartViewModel
    {
        public string SearchValue { get; set; }

        [BindNever]
        public SearchAPart SearchAPart { get; set; }
    }
}
