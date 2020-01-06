using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.SearchA.Indexes;
using OrchardCore.ContentManagement;
using OrchardCore.Liquid;
using YesSql;

namespace OrchardCore.SearchA.Liquid
{
    public class ContentSearchALiquidTemplateEventHandler : ILiquidTemplateEventHandler
    {
        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        public ContentSearchALiquidTemplateEventHandler(IContentManager contentManager, ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public Task RenderingAsync(TemplateContext context)
        {
            context.MemberAccessStrategy.Register<LiquidContentAccessor, LiquidPropertyAccessor>("SearchA", obj =>
            {
                return new LiquidPropertyAccessor(async SearchA =>
                {
                    var SearchAPartIndex = await _session.Query<ContentItem, SearchAPartIndex>(x => x.SearchA == SearchA.ToLowerInvariant()).FirstOrDefaultAsync();
                    var contentItemId = SearchAPartIndex?.ContentItemId;

                    if (contentItemId == null)
                    {
                        return NilValue.Instance;
                    }

                    return FluidValue.Create(await _contentManager.GetAsync(contentItemId));
                });
            });

            return Task.CompletedTask;
        }
    }
}
