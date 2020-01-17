using System.Threading.Tasks;
using OrchardCore.Indexing;
using OrchardCore.ValueForSortingThree.Model;

namespace OrchardCore.ValueForSortingThree.Indexing
{
    public class ValueForSortingThreePartIndexHandler : ContentPartIndexHandler<ValueForSortingThreePart>
    {
        public override Task BuildIndexAsync(ValueForSortingThreePart part, BuildPartIndexContext context)
        {
            var options = context.Settings.ToOptions() 
                | DocumentIndexOptions.Analyze
                ;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.ValueForSortingThree, options);
            }

            return Task.CompletedTask;
        }
    }
}
