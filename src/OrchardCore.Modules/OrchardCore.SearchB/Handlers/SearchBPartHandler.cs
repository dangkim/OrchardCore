using System;
using System.Linq;
using System.Threading.Tasks;
using Fluid;
using OrchardCore.SearchB.Models;
using OrchardCore.SearchB.Settings;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Cache;
using OrchardCore.Liquid;
using OrchardCore.Settings;
using YesSql;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.SearchB.Handlers
{
    public class SearchBPartHandler : ContentPartHandler<SearchBPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly ITagCache _tagCache;
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly ISession _session;

        public SearchBPartHandler(
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            ITagCache tagCache,
            ILiquidTemplateManager liquidTemplateManager,
            ISession session)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _siteService = siteService;
            _tagCache = tagCache;
            _liquidTemplateManager = liquidTemplateManager;
            _session = session;
        }

        public async override Task UpdatedAsync(UpdateContentContext context, SearchBPart part)
        {
            // Compute the Path only if it's empty
            if (!String.IsNullOrEmpty(part.SearchB))
            {
                return;
            }

            var pattern = GetPattern(part);

            if (!String.IsNullOrEmpty(pattern))
            {
                var templateContext = new TemplateContext();
                templateContext.SetValue("ContentItem", part.ContentItem);

                part.SearchB = await _liquidTemplateManager.RenderAsync(pattern, NullEncoder.Default, templateContext);
                part.Apply();
            }
        }

        /// <summary>
        /// Get the pattern from the AutoroutePartSettings property for its type
        /// </summary>
        private string GetPattern(SearchBPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, "SearchAPart", StringComparison.Ordinal));
            var pattern = contentTypePartDefinition.GetSettings<SearchBPartSettings>().Pattern;

            return pattern;
        }

        public override Task PublishedAsync(PublishContentContext context, SearchBPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchB:{instance.SearchB}");
        }

        public override Task RemovedAsync(RemoveContentContext context, SearchBPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchB:{instance.SearchB}");
        }

        public override Task UnpublishedAsync(PublishContentContext context, SearchBPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchB:{instance.SearchB}");
        }

        public override async Task CloningAsync(CloneContentContext context, SearchBPart part)
        {
            var clonedPart = context.CloneContentItem.As<SearchBPart>();
            clonedPart.SearchB = await GenerateUniqueSearchAAsync(clonedPart.SearchB, clonedPart);

            clonedPart.Apply();

        }
        
        private async Task<string> GenerateUniqueSearchAAsync(string searchA, SearchBPart context)
        {
            var version = 1;
            var unversionedSearchA = searchA;

            var versionSeparatorPosition = searchA.LastIndexOf('-');
            if (versionSeparatorPosition > -1)
            {
                int.TryParse(searchA.Substring(versionSeparatorPosition).TrimStart('-'), out version);
                unversionedSearchA = searchA.Substring(0, versionSeparatorPosition);
            }

            while (true)
            {
                var versionedSearchA = $"{unversionedSearchA}-{version++}";
                if ((await _session.QueryIndex<SearchBPartIndex>(o => o.SearchB == versionedSearchA && o.ContentItemId != context.ContentItem.ContentItemId).CountAsync()) == 0)
                {
                    return versionedSearchA;
                }
            }
        }
    }
}
