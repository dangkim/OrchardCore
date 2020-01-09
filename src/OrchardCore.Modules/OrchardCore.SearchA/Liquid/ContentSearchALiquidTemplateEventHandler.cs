using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
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
                return new LiquidPropertyAccessor(async searchA =>
                {
                    var searchAPartIndex = await _session.Query<ContentItem, SearchAPartIndex>(x => x.SearchA == searchA.ToLowerInvariant()).FirstOrDefaultAsync();
                    var contentItemId = searchAPartIndex?.ContentItemId;

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
