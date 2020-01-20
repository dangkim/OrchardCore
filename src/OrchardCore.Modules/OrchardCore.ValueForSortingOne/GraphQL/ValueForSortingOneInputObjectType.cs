using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.ValueForSortingOne.Models;
using OrchardCore.Apis.GraphQL.Queries;

namespace OrchardCore.ValueForSortingOne.GraphQL
{
    public class ValueForSortingOneInputObjectType : WhereInputObjectGraphType<ValueForSortingOnePart>
    {
        public ValueForSortingOneInputObjectType(IStringLocalizer<ValueForSortingOneInputObjectType> T)
        {
            Name = "ValueForSortingOnePartInput";
            Description = T["the Value For Sorting  value part of the content item"];

            AddScalarFilterFields<StringGraphType>("ValueForSortingOne", T["the Value For Sorting of the content item"]);
        }
    }
}
