using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.SearchA.Models;
using OrchardCore.Apis.GraphQL.Queries;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAInputObjectType : WhereInputObjectGraphType<SearchAPart>
    {
        public SearchAInputObjectType(IStringLocalizer<SearchAInputObjectType> T)
        {
            Name = "SearchAPartInput";
            Description = T["the search value part of the content item"];

            AddScalarFilterFields<StringGraphType>("SearchA", T["the search value of the content item"]);
        }
    }
}
