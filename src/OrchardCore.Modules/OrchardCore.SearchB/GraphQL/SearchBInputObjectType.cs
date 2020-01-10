using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.SearchB.Models;
using OrchardCore.Apis.GraphQL.Queries;

namespace OrchardCore.SearchB.GraphQL
{
    public class SearchBInputObjectType : WhereInputObjectGraphType<SearchBPart>
    {
        public SearchBInputObjectType(IStringLocalizer<SearchBInputObjectType> T)
        {
            Name = "SearchBPartInput";
            Description = T["the search value part of the content item"];

            AddScalarFilterFields<StringGraphType>("searchB", T["the search value of the content item"]);
        }
    }
}
