using GraphQL.Types;
using Microsoft.Extensions.Localization;
using OrchardCore.SearchB.Models;

namespace OrchardCore.SearchB.GraphQL
{
    public class SearchBQueryObjectType : ObjectGraphType<SearchBPart>
    {
        public SearchBQueryObjectType(IStringLocalizer<SearchBQueryObjectType> T)
        {
            Name = "SearchBPart";
            Description = T["Alternative path for the content item"];

            Field("searchB", x => x.SearchB, true);
        }
    }
}
