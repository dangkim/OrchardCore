using System.Collections.Generic;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAPartIndexSearchAProvider : IIndexAliasProvider
    {
        private static readonly IndexAlias[] _aliases = new[]
        {
            new IndexAlias
            {
                Alias = "searchAPart",
                Index = nameof(SearchAPartIndex),
                With = q => q.With<SearchAPartIndex>()
            }
        };

        public IEnumerable<IndexAlias> GetAliases()
        {
            return _aliases;
        }
    }
}
