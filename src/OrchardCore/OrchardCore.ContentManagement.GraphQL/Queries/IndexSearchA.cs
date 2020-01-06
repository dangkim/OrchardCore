using System;
using YesSql;

namespace OrchardCore.ContentManagement.GraphQL.Queries
{
    public class IndexSearchA
    {
        public string SearchA { get; set; }

        public string Index { get; set; }

        public Func<IQuery<ContentItem>, IQuery<ContentItem>> With { get; set; }
    }
}