using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.SearchB.Models;
using OrchardCore.Liquid;
using Microsoft.Extensions.Localization;

namespace OrchardCore.SearchB.Settings
{
    public class SearchBPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        private readonly ILiquidTemplateManager _templateManager;

        public SearchBPartSettingsDisplayDriver(ILiquidTemplateManager templateManager, IStringLocalizer<SearchBPartSettingsDisplayDriver> localizer)
        {
            _templateManager = templateManager;
            T = localizer;
        }

        public IStringLocalizer T { get; private set; }

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(SearchBPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            return Initialize<SearchBPartSettingsViewModel>("SearchAPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<SearchBPartSettings>();

                model.Pattern = settings.Pattern;
                model.SearchBPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(SearchBPart), contentTypePartDefinition.PartDefinition.Name, StringComparison.Ordinal))
            {
                return null;
            }

            var model = new SearchBPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Pattern))
            {
                if (!string.IsNullOrEmpty(model.Pattern) && !_templateManager.Validate(model.Pattern, out var errors))
                {
                    context.Updater.ModelState.AddModelError(nameof(model.Pattern), T["Pattern doesn't contain a valid Liquid expression. Details: {0}", string.Join(" ", errors)]);
                }
                else
                {
                    context.Builder.WithSettings(new SearchBPartSettings { Pattern = model.Pattern });
                }
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}