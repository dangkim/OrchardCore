using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.SearchB.Models;
using OrchardCore.SearchB.Settings;
using OrchardCore.SearchB.ViewModels;
using YesSql;
using Microsoft.Extensions.Localization;
using OrchardCore.Mvc.ModelBinding;

namespace OrchardCore.SearchB.Drivers
{
    public class SearchBPartDisplayDriver : ContentPartDisplayDriver<SearchBPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISession _session;
        private readonly IStringLocalizer<SearchBPartDisplayDriver> T;

        public SearchBPartDisplayDriver(
            IContentDefinitionManager contentDefinitionManager,
            ISession session,
            IStringLocalizer<SearchBPartDisplayDriver> localizer)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _session = session;
            T = localizer;
        }

        public override IDisplayResult Edit(SearchBPart searchBPart)
        {
            return Initialize<SearchBPartViewModel>("SearchBPart_Edit", m => BuildViewModel(m, searchBPart));
        }

        public override async Task<IDisplayResult> UpdateAsync(SearchBPart model, IUpdateModel updater)
        {
            var settings = GetSearchAPartSettings(model);

            await updater.TryUpdateModelAsync(model, Prefix, t => t.SearchB);

            await ValidateAsync(model, updater);

            return Edit(model);
        }

        public SearchBPartSettings GetSearchAPartSettings(SearchBPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(p => p.PartDefinition.Name == nameof(SearchBPart));
            var settings = contentTypePartDefinition.GetSettings<SearchBPartSettings>();

            return settings;
        }

        private void BuildViewModel(SearchBPartViewModel model, SearchBPart part)
        {
            var settings = GetSearchAPartSettings(part);

            model.SearchB = part.SearchB;
            model.SearchBPart = part;
            model.Settings = settings;
        }

        private async Task ValidateAsync(SearchBPart searchB, IUpdateModel updater)
        {
            if (searchB.SearchB != null && (await _session.QueryIndex<SearchBPartIndex>(o => o.SearchB == searchB.SearchB && o.ContentItemId != searchB.ContentItem.ContentItemId).CountAsync()) > 0)
            {
                updater.ModelState.AddModelError(Prefix, nameof(searchB.SearchB), T["Your searchA is already in use."]);
            }
        }
    }
}
