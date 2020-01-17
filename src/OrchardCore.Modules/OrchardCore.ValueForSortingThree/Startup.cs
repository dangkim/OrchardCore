using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Modules;
using OrchardCore.ValueForSortingThree.Drivers;
using OrchardCore.ValueForSortingThree.Indexing;
using OrchardCore.ValueForSortingThree.Model;
using OrchardCore.ValueForSortingThree.ViewModels;

namespace OrchardCore.ValueForSortingThree
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ValueForSortingThreePartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // ValueForSortingThree Part
            services.AddScoped<IContentPartDisplayDriver, ValueForSortingThreePartDisplay>();
            services.AddSingleton<ContentPart, ValueForSortingThreePart>();
            services.AddScoped<IContentPartIndexHandler, ValueForSortingThreePartIndexHandler>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
