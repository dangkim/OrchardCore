using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.SearchA.Models;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAQueryObjectType : ObjectGraphType<SearchAPart>
    {
        public SearchAQueryObjectType(IStringLocalizer<SearchAQueryObjectType> T)
        {
            Name = "SearchAPart";
            Description = T["Alternative path for the content item"];

            Field("SearchA", x => x.SearchA, true);
        }
    }
}
