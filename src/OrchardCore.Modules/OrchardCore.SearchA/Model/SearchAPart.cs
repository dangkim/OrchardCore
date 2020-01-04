using OrchardCore.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace OrchardCore.SearchA.Model
{
    public class SearchAPart : ContentPart
    {
        [Required]
        public string SearchValue { get; set; }
    }
}
