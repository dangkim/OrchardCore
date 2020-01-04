using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Apis;
using OrchardCore.SearchA.Model;
using OrchardCore.ContentManagement.GraphQL;
using OrchardCore.ContentManagement.GraphQL.Queries;
using OrchardCore.Modules;

namespace OrchardCore.SearchA.GraphQL
{
    [RequireFeatures("OrchardCore.Apis.GraphQL")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddInputObjectGraphType<SearchAPart, SearchAInputObjectType>();
            services.AddObjectGraphType<SearchAPart, SearchAQueryObjectType>();
            services.AddTransient<IIndexAliasProvider, SearchAPartIndexAliasProvider>();
        }
    }
}
