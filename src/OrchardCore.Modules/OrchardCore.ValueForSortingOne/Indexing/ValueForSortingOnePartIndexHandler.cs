using System.Threading.Tasks;
using OrchardCore.Indexing;
using OrchardCore.ValueForSortingOne.Model;

namespace OrchardCore.ValueForSortingOne.Indexing
{
    public class ValueForSortingOnePartIndexHandler : ContentPartIndexHandler<ValueForSortingOnePart>
    {
        public override Task BuildIndexAsync(ValueForSortingOnePart part, BuildPartIndexContext context)
        {
            var options = context.Settings.ToOptions() 
                | DocumentIndexOptions.Analyze
                ;

            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key, part.ValueForSortingOne, options);
            }

            return Task.CompletedTask;
        }
    }
}
