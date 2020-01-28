using OrchardCore.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace OrchardCore.ValueForSortingOne.Models
{
    public class ValueForSortingOnePart : ContentPart
    {
        [Required]
        public int ValueForSortingOne { get; set; }
    }
}
