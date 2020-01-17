using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Modules;
using OrchardCore.ValueForSortingTwo.Drivers;
using OrchardCore.ValueForSortingTwo.Indexing;
using OrchardCore.ValueForSortingTwo.Model;
using OrchardCore.ValueForSortingTwo.ViewModels;

namespace OrchardCore.ValueForSortingTwo
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ValueForSortingTwoPartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // Title Part
            services.AddScoped<IContentPartDisplayDriver, ValueForSortingTwoPartDisplay>();
            services.AddSingleton<ContentPart, ValueForSortingTwoPart>();
            services.AddScoped<IContentPartIndexHandler, ValueForSortingTwoPartIndexHandler>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
