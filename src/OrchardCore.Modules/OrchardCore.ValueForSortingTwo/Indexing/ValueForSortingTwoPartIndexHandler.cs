using System.Threading.Tasks;
using OrchardCore.Indexing;
using OrchardCore.ValueForSortingTwo.Model;

namespace OrchardCore.ValueForSortingTwo.Indexing
{
    public class ValueForSortingTwoPartIndexHandler : ContentPartIndexHandler<ValueForSortingTwoPart>
    {
        public override Task BuildIndexAsync(ValueForSortingTwoPart part, BuildPartIndexContext context)
        {
            var options = context.Settings.ToOptions() 
                | DocumentIndexOptions.Analyze
                ;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.ValueForSortingTwo, options);
            }

            return Task.CompletedTask;
        }
    }
}
