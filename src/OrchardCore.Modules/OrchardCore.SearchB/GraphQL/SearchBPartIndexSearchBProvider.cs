using System.Collections.Generic;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.SearchB.GraphQL
{
    public class SearchBPartIndexSearchBProvider : IIndexAliasProvider
    {
        private static readonly IndexAlias[] _aliases = new[]
        {
            new IndexAlias
            {
                Alias = "searchBPart",
                Index = nameof(SearchBPartIndex),
                With = q => q.With<SearchBPartIndex>()
            }
        };

        public IEnumerable<IndexAlias> GetAliases()
        {
            return _aliases;
        }
    }
}
