using System.Collections.Generic;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.ValueForSortingOne.GraphQL
{
    public class ValueForSortingOnePartIndexValueForSortingOneProvider : IIndexAliasProvider
    {
        private static readonly IndexAlias[] _aliases = new[]
        {
            new IndexAlias
            {
                Alias = "valueForSortingOnePart",
                Index = nameof(ValueForSortingOnePartIndex),
                With = q => q.With<ValueForSortingOnePartIndex>()
            }
        };

        public IEnumerable<IndexAlias> GetAliases()
        {
            return _aliases;
        }
    }
}
