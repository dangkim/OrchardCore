using OrchardCore.SearchB.Models;
using YesSql.Indexes;

namespace OrchardCore.ContentManagement.Records
{
    public class SearchBPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string SearchB { get; set; }
    }

    public class SearchAPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<SearchBPartIndex>()
                .Map(contentItem =>
                {
                    if (!contentItem.IsPublished())
                    {
                        return null;
                    }

                    var searchAPart = contentItem.As<SearchBPart>();

                    if (searchAPart?.SearchB == null)
                    {
                        return null;
                    }

                    return new SearchBPartIndex
                    {
                        SearchB = searchAPart.SearchB.ToLowerInvariant(),
                        ContentItemId = contentItem.ContentItemId,
                    };
                });
        }
    }
}