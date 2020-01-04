using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Modules;
using OrchardCore.SearchA.Drivers;
using OrchardCore.SearchA.Indexing;
using OrchardCore.SearchA.Model;
using OrchardCore.SearchA.ViewModels;

namespace OrchardCore.SearchA
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<SearchAPartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // SearchA Part
            services.AddScoped<IContentPartDisplayDriver, SearchAPartDisplay>();
            services.AddSingleton<ContentPart, SearchAPart>();
            services.AddScoped<IContentPartIndexHandler, SearchAPartIndexHandler>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
