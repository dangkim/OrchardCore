using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.ValueForSortingOne.Models;

namespace OrchardCore.ValueForSortingOne.GraphQL
{
    public class ValueForSortingOneQueryObjectType : ObjectGraphType<ValueForSortingOnePart>
    {
        public ValueForSortingOneQueryObjectType(IStringLocalizer<ValueForSortingOneQueryObjectType> T)
        {
            Name = "ValueForSortingOnePart";
            Description = T["Alternative path for the content item"];

            Field("valueForSortingOne", x => x.ValueForSortingOne, true);
        }
    }
}
