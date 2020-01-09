using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.SearchA.Models;
using OrchardCore.SearchA.Settings;
using OrchardCore.SearchA.ViewModels;
using YesSql;
using Microsoft.Extensions.Localization;
using OrchardCore.Mvc.ModelBinding;

namespace OrchardCore.SearchA.Drivers
{
    public class SearchAPartDisplayDriver : ContentPartDisplayDriver<SearchAPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISession _session;
        private readonly IStringLocalizer<SearchAPartDisplayDriver> T;

        public SearchAPartDisplayDriver(
            IContentDefinitionManager contentDefinitionManager,
            ISession session,
            IStringLocalizer<SearchAPartDisplayDriver> localizer)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _session = session;
            T = localizer;
        }

        public override IDisplayResult Edit(SearchAPart searchAPart)
        {
            return Initialize<SearchAPartViewModel>("SearchAPart_Edit", m => BuildViewModel(m, searchAPart));
        }

        public override async Task<IDisplayResult> UpdateAsync(SearchAPart model, IUpdateModel updater)
        {
            var settings = GetSearchAPartSettings(model);

            await updater.TryUpdateModelAsync(model, Prefix, t => t.SearchA);

            await ValidateAsync(model, updater);

            return Edit(model);
        }

        public SearchAPartSettings GetSearchAPartSettings(SearchAPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(p => p.PartDefinition.Name == nameof(SearchAPart));
            var settings = contentTypePartDefinition.GetSettings<SearchAPartSettings>();

            return settings;
        }

        private void BuildViewModel(SearchAPartViewModel model, SearchAPart part)
        {
            var settings = GetSearchAPartSettings(part);

            model.SearchA = part.SearchA;
            model.SearchAPart = part;
            model.Settings = settings;
        }

        private async Task ValidateAsync(SearchAPart searchA, IUpdateModel updater)
        {
            if (searchA.SearchA != null && (await _session.QueryIndex<SearchAPartIndex>(o => o.SearchA == searchA.SearchA && o.ContentItemId != searchA.ContentItem.ContentItemId).CountAsync()) > 0)
            {
                updater.ModelState.AddModelError(Prefix, nameof(searchA.SearchA), T["Your searchA is already in use."]);
            }
        }
    }
}
