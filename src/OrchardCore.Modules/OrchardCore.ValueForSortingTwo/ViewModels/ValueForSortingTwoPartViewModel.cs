using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ValueForSortingTwo.Model;

namespace OrchardCore.ValueForSortingTwo.ViewModels
{
    public class ValueForSortingTwoPartViewModel
    {
        public string ValueForSortingTwo { get; set; }

        [BindNever]
        public ValueForSortingTwoPart ValueForSortingTwoPart { get; set; }
    }
}
