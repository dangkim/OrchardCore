using System.Threading.Tasks;
using OrchardCore.SearchB.Models;
using OrchardCore.Indexing;

namespace OrchardCore.SearchB.Indexing
{
    public class SearchBPartIndexHandler : ContentPartIndexHandler<SearchBPart>
    {
        public override Task BuildIndexAsync(SearchBPart part, BuildPartIndexContext context)
        {
            var options = DocumentIndexOptions.Store;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.SearchB, options);
            }

            return Task.CompletedTask;
        }
    }
}
