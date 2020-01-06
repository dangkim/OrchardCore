using Microsoft.Extensions.DependencyInjection;
using OrchardCore.SearchA.Models;
using OrchardCore.Apis;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.Modules;

namespace OrchardCore.SearchA.GraphQL
{
    [RequireFeatures("OrchardCore.Apis.GraphQL")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddObjectGraphType<SearchAPart, SearchAQueryObjectType>();
            services.AddInputObjectGraphType<SearchAPart, SearchAInputObjectType>();
            services.AddTransient<IIndexSearchAProvider, SearchAPartIndexSearchAProvider>();
        }
    }
}
