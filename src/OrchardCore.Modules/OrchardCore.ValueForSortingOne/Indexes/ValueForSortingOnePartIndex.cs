using OrchardCore.ValueForSortingOne.Models;
using YesSql.Indexes;

namespace OrchardCore.ContentManagement.Records
{
    public class ValueForSortingOnePartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string ValueForSortingOne { get; set; }
    }

    public class SearchAPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ValueForSortingOnePartIndex>()
                .Map(contentItem =>
                {
                    if (!contentItem.IsPublished())
                    {
                        return null;
                    }

                    var valueForSortingOnePart = contentItem.As<ValueForSortingOnePart>();

                    if (valueForSortingOnePart?.ValueForSortingOne == null)
                    {
                        return null;
                    }

                    return new ValueForSortingOnePartIndex
                    {
                        ValueForSortingOne = valueForSortingOnePart.ValueForSortingOne.ToLowerInvariant(),
                        ContentItemId = contentItem.ContentItemId,
                    };
                });
        }
    }
}