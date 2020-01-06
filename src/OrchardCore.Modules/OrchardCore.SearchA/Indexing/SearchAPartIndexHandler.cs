using System.Threading.Tasks;
using OrchardCore.SearchA.Models;
using OrchardCore.Indexing;

namespace OrchardCore.SearchA.Indexing
{
    public class SearchAPartIndexHandler : ContentPartIndexHandler<SearchAPart>
    {
        public override Task BuildIndexAsync(SearchAPart part, BuildPartIndexContext context)
        {
            var options = DocumentIndexOptions.Store;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.SearchA, options);
            }

            return Task.CompletedTask;
        }
    }
}
