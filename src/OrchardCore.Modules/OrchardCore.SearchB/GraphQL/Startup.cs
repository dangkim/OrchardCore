using Microsoft.Extensions.DependencyInjection;
using OrchardCore.SearchB.Models;
using OrchardCore.Apis;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.Modules;

namespace OrchardCore.SearchB.GraphQL
{
    [RequireFeatures("OrchardCore.Apis.GraphQL")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddObjectGraphType<SearchBPart, SearchBQueryObjectType>();
            services.AddInputObjectGraphType<SearchBPart, SearchBInputObjectType>();
            services.AddTransient<IIndexAliasProvider, SearchBPartIndexSearchBProvider>();
        }
    }
}
