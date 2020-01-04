using System.Threading.Tasks;
using OrchardCore.Indexing;
using OrchardCore.SearchA.Model;

namespace OrchardCore.SearchA.Indexing
{
    public class SearchAPartIndexHandler : ContentPartIndexHandler<SearchAPart>
    {
        public override Task BuildIndexAsync(SearchAPart part, BuildPartIndexContext context)
        {
            var options = context.Settings.ToOptions() 
                | DocumentIndexOptions.Analyze
                ;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.SearchValue, options);
            }

            return Task.CompletedTask;
        }
    }
}
