using OrchardCore.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace OrchardCore.ValueForSortingThree.Model
{
    public class ValueForSortingThreePart : ContentPart
    {
        [Required]
        public int ValueForSortingThree { get; set; }
    }
}
