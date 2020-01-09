using OrchardCore.SearchA.Models;
using YesSql.Indexes;

namespace OrchardCore.ContentManagement.Records
{
    public class SearchAPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string SearchA { get; set; }
    }

    public class SearchAPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<SearchAPartIndex>()
                .Map(contentItem =>
                {
                    if (!contentItem.IsPublished())
                    {
                        return null;
                    }

                    var searchAPart = contentItem.As<SearchAPart>();

                    if (searchAPart?.SearchA == null)
                    {
                        return null;
                    }

                    return new SearchAPartIndex
                    {
                        SearchA = searchAPart.SearchA.ToLowerInvariant(),
                        ContentItemId = contentItem.ContentItemId,
                    };
                });
        }
    }
}