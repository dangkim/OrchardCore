using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Liquid;
using YesSql;

namespace OrchardCore.SearchB.Liquid
{
    public class ContentSearchBLiquidTemplateEventHandler : ILiquidTemplateEventHandler
    {
        private readonly IContentManager _contentManager;
        private readonly ISession _session;

        public ContentSearchBLiquidTemplateEventHandler(IContentManager contentManager, ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public Task RenderingAsync(TemplateContext context)
        {
            context.MemberAccessStrategy.Register<LiquidContentAccessor, LiquidPropertyAccessor>("SearchB", obj =>
            {
                return new LiquidPropertyAccessor(async searchB =>
                {
                    var searchBPartIndex = await _session.Query<ContentItem, SearchBPartIndex>(x => x.SearchB == searchB.ToLowerInvariant()).FirstOrDefaultAsync();
                    var contentItemId = searchBPartIndex?.ContentItemId;

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
