using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.SearchA.Drivers;
using OrchardCore.SearchA.Handlers;
using OrchardCore.SearchA.Indexes;
using OrchardCore.SearchA.Indexing;
using OrchardCore.SearchA.Liquid;
using OrchardCore.SearchA.Models;
using OrchardCore.SearchA.Services;
using OrchardCore.SearchA.Settings;
using OrchardCore.SearchA.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using YesSql.Indexes;

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
            services.AddSingleton<IIndexProvider, SearchAPartIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IContentSearchAProvider, SearchAPartContentSearchAProvider>();

            // Identity Part
            services.AddScoped<IContentPartDisplayDriver, SearchAPartDisplayDriver>();
            services.AddSingleton<ContentPart, SearchAPart>();
            services.AddScoped<IContentPartHandler, SearchAPartHandler>();
            services.AddScoped<IContentPartIndexHandler, SearchAPartIndexHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, SearchAPartSettingsDisplayDriver>();


            services.AddScoped<ILiquidTemplateEventHandler, ContentSearchALiquidTemplateEventHandler>();
        }
    }
}
