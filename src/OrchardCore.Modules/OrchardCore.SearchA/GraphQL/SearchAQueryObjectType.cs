using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.SearchA.Model;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAQueryObjectType : ObjectGraphType<SearchAPart>
    {
        public SearchAQueryObjectType(IStringLocalizer<SearchAQueryObjectType> T)
        {
            Name = "SearchAPart";
            Description = T["The value for searching for your content item."];

            Field(x => x.SearchValue).Description(T["The search value for your content item."]);
        }
    }
}
