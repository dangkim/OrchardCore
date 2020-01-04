using System;
using OrchardCore.SearchA.Model;
using YesSql.Indexes;

namespace OrchardCore.ContentManagement.Records
{
    public class SearchAPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string SearchValue { get; set; }
        public bool Published { get; set; }
    }

    public class SearchAPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<SearchAPartIndex>()
                .Map(contentItem =>
                {
                    var searchValue = contentItem.As<SearchAPart>()?.SearchValue;
                    if (!String.IsNullOrEmpty(searchValue) && (contentItem.Published || contentItem.Latest))
                    {
                        return new SearchAPartIndex
                        {
                            ContentItemId = contentItem.ContentItemId,
                            SearchValue = searchValue,
                            Published = contentItem.Published
                        };
                    }

                    return null;
                });
        }
    }
}