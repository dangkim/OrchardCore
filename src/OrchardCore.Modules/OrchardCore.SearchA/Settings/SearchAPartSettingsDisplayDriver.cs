using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.SearchA.Models;
using OrchardCore.Liquid;
using Microsoft.Extensions.Localization;

namespace OrchardCore.SearchA.Settings
{
    public class SearchAPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        private readonly ILiquidTemplateManager _templateManager;

        public SearchAPartSettingsDisplayDriver(ILiquidTemplateManager templateManager, IStringLocalizer<SearchAPartSettingsDisplayDriver> localizer)
        {
            _templateManager = templateManager;
            T = localizer;
        }

        public IStringLocalizer T { get; private set; }

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(SearchAPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<SearchAPartSettingsViewModel>("SearchAPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<SearchAPartSettings>();

                model.Pattern = settings.Pattern;
                model.SearchAPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(SearchAPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new SearchAPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Pattern))
            {
                if (!string.IsNullOrEmpty(model.Pattern) && !_templateManager.Validate(model.Pattern, out var errors))
                {
                    context.Updater.ModelState.AddModelError(nameof(model.Pattern), T["Pattern doesn't contain a valid Liquid expression. Details: {0}", string.Join(" ", errors)]);
                }
                else
                {
                    context.Builder.WithSettings(new SearchAPartSettings { Pattern = model.Pattern });
                }
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}