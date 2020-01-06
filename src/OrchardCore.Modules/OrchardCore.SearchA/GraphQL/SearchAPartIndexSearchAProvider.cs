using System.Collections.Generic;
using OrchardCore.SearchA.Indexes;
using OrchardCore.ContentManagement.GraphQL.Queries;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAPartIndexSearchAProvider : IIndexSearchAProvider
    {
        private static readonly IndexSearchA[] _SearchAes = new[]
        {
            new IndexSearchA
            {
                SearchA = "searchAPart",
                Index = "SearchAPartIndex",
                With = q => q.With<SearchAPartIndex>()
            }
        };

        public IEnumerable<IndexSearchA> GetSearchAes()
        {
            return _SearchAes;
        }
    }
}
