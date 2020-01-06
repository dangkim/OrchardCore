using System;
using System.Linq;
using System.Threading.Tasks;
using Fluid;
using OrchardCore.SearchA.Indexes;
using OrchardCore.SearchA.Models;
using OrchardCore.SearchA.Settings;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Environment.Cache;
using OrchardCore.Liquid;
using OrchardCore.Settings;
using YesSql;

namespace OrchardCore.SearchA.Handlers
{
    public class SearchAPartHandler : ContentPartHandler<SearchAPart>
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISiteService _siteService;
        private readonly ITagCache _tagCache;
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly ISession _session;

        public SearchAPartHandler(
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

        public async override Task UpdatedAsync(UpdateContentContext context, SearchAPart part)
        {
            // Compute the Path only if it's empty
            if (!String.IsNullOrEmpty(part.SearchA))
            {
                return;
            }

            var pattern = GetPattern(part);

            if (!String.IsNullOrEmpty(pattern))
            {
                var templateContext = new TemplateContext();
                templateContext.SetValue("ContentItem", part.ContentItem);

                part.SearchA = await _liquidTemplateManager.RenderAsync(pattern, NullEncoder.Default, templateContext);
                part.Apply();
            }
        }

        /// <summary>
        /// Get the pattern from the AutoroutePartSettings property for its type
        /// </summary>
        private string GetPattern(SearchAPart part)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, "SearchAPart", StringComparison.Ordinal));
            var pattern = contentTypePartDefinition.GetSettings<SearchAPartSettings>().Pattern;

            return pattern;
        }

        public override Task PublishedAsync(PublishContentContext context, SearchAPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchA:{instance.SearchA}");
        }

        public override Task RemovedAsync(RemoveContentContext context, SearchAPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchA:{instance.SearchA}");
        }

        public override Task UnpublishedAsync(PublishContentContext context, SearchAPart instance)
        {
            return _tagCache.RemoveTagAsync($"SearchA:{instance.SearchA}");
        }

        public override async Task CloningAsync(CloneContentContext context, SearchAPart part)
        {
            var clonedPart = context.CloneContentItem.As<SearchAPart>();
            clonedPart.SearchA = await GenerateUniqueSearchAAsync(clonedPart.SearchA, clonedPart);

            clonedPart.Apply();

        }
        
        private async Task<string> GenerateUniqueSearchAAsync(string SearchA, SearchAPart context)
        {
            var version = 1;
            var unversionedSearchA = SearchA;

            var versionSeparatorPosition = SearchA.LastIndexOf('-');
            if (versionSeparatorPosition > -1)
            {
                int.TryParse(SearchA.Substring(versionSeparatorPosition).TrimStart('-'), out version);
                unversionedSearchA = SearchA.Substring(0, versionSeparatorPosition);
            }

            while (true)
            {
                var versionedSearchA = $"{unversionedSearchA}-{version++}";
                if ((await _session.QueryIndex<SearchAPartIndex>(o => o.SearchA == versionedSearchA && o.ContentItemId != context.ContentItem.ContentItemId).CountAsync()) == 0)
                {
                    return versionedSearchA;
                }
            }
        }
    }
}
