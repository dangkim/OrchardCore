using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.SearchB.Drivers;
using OrchardCore.SearchB.Handlers;
using OrchardCore.SearchB.Indexing;
using OrchardCore.SearchB.Liquid;
using OrchardCore.SearchB.Models;
using OrchardCore.SearchB.Services;
using OrchardCore.SearchB.Settings;
using OrchardCore.SearchB.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using YesSql.Indexes;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.SearchB
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<SearchBPartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIndexProvider, SearchAPartIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IContentSearchAProvider, SearchBPartContentSearchBProvider>();

            // Identity Part
            services.AddScoped<IContentPartDisplayDriver, SearchBPartDisplayDriver>();
            services.AddSingleton<ContentPart, SearchBPart>();
            services.AddScoped<IContentPartHandler, SearchBPartHandler>();
            services.AddScoped<IContentPartIndexHandler, SearchBPartIndexHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, SearchBPartSettingsDisplayDriver>();


            services.AddScoped<ILiquidTemplateEventHandler, ContentSearchBLiquidTemplateEventHandler>();
        }
    }
}
