using OrchardCore.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace OrchardCore.ValueForSortingTwo.Model
{
    public class ValueForSortingTwoPart : ContentPart
    {
        [Required]
        public string ValueForSortingTwo { get; set; }
    }
}
