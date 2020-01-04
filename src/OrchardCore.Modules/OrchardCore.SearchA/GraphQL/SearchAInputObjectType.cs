using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.Apis.GraphQL.Queries;
using OrchardCore.SearchA.Model;

namespace OrchardCore.SearchA.GraphQL
{
    public class SearchAInputObjectType : WhereInputObjectGraphType<SearchAPart>
    {
        public SearchAInputObjectType(IStringLocalizer<SearchAInputObjectType> T)
        {
            Name = "SearchAPartInput";
            Description = T["the search value A part of the content item"];

            AddScalarFilterFields<StringGraphType>("searchValue", T["the path of the content item to filter"]);
        }
    }
}
