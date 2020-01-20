using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ValueForSortingOne.Models;

namespace OrchardCore.ValueForSortingOne.ViewModels
{
    public class ValueForSortingOnePartViewModel
    {
        public string ValueForSortingOne { get; set; }

        [BindNever]
        public ValueForSortingOnePart ValueForSortingOnePart { get; set; }
    }
}
