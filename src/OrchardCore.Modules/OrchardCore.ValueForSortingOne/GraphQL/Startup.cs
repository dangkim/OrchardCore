using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ValueForSortingOne.Models;
using OrchardCore.Apis;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.Modules;

namespace OrchardCore.ValueForSortingOne.GraphQL
{
    [RequireFeatures("OrchardCore.Apis.GraphQL")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddObjectGraphType<ValueForSortingOnePart, ValueForSortingOneQueryObjectType>();
            services.AddInputObjectGraphType<ValueForSortingOnePart, ValueForSortingOneInputObjectType>();
            services.AddTransient<IIndexAliasProvider, ValueForSortingOnePartIndexValueForSortingOneProvider>();
        }
    }
}
