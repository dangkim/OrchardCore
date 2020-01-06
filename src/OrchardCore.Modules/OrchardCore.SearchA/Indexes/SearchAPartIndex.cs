using OrchardCore.ContentManagement;
using OrchardCore.SearchA.Models;
using YesSql.Indexes;

namespace OrchardCore.SearchA.Indexes
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

                    var SearchAPart = contentItem.As<SearchAPart>();

                    if (SearchAPart?.SearchA == null)
                    {
                        return null;
                    }

                    return new SearchAPartIndex
                    {
                        SearchA = SearchAPart.SearchA.ToLowerInvariant(),
                        ContentItemId = contentItem.ContentItemId,
                    };
                });
        }
    }
}