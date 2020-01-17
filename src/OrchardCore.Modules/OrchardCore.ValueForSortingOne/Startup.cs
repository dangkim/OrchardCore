using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Modules;
using OrchardCore.ValueForSortingOne.Drivers;
using OrchardCore.ValueForSortingOne.Indexing;
using OrchardCore.ValueForSortingOne.Model;
using OrchardCore.ValueForSortingOne.ViewModels;

namespace OrchardCore.ValueForSortingOne
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ValueForSortingOnePartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // ValueForSortingOne Part
            services.AddScoped<IContentPartDisplayDriver, ValueForSortingOneDisplay>();
            services.AddSingleton<ContentPart, ValueForSortingOnePart>();
            services.AddScoped<IContentPartIndexHandler, ValueForSortingOnePartIndexHandler>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
