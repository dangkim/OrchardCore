using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ValueForSortingThree.Model;

namespace OrchardCore.ValueForSortingThree.ViewModels
{
    public class ValueForSortingThreePartViewModel
    {
        public int? ValueForSortingThree { get; set; }

        [BindNever]
        public ValueForSortingThreePart ValueForSortingThreePart { get; set; }
    }
}
