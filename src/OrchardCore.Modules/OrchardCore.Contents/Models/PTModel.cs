using OrchardCore.ContentManagement;

namespace OrchardCore.Contents.Models
{
    /// <summary>
    /// When attached to a content type, provides a way to edit the common
    /// properties of a content item like CreatedUtc and Owner.
    /// </summary>
    public class PTModel
    {
        private string errors;

        private string result;

        public string Errors { get => errors; set => errors = value; }
        public string Result { get => result; set => result = value; }
        public int sts { get; set; }
    }
}
