using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.SearchA.Indexes;
using YesSql;

namespace OrchardCore.SearchA.Services
{
    public class SearchAPartContentSearchAProvider : IContentSearchAProvider
    {
        private readonly ISession _session;

        public SearchAPartContentSearchAProvider(ISession session)
        {
            _session = session;
        }

        public int Order => 100;
        
        public async Task<string> GetContentItemIdAsync(string SearchA)
        {
            if (SearchA.StartsWith("SearchA:", System.StringComparison.OrdinalIgnoreCase))
            {
                SearchA = SearchA.Substring(6);

                var SearchAPartIndex = await _session.Query<ContentItem, SearchAPartIndex>(x => x.SearchA == SearchA.ToLowerInvariant()).FirstOrDefaultAsync();
                return SearchAPartIndex?.ContentItemId;
            }

            return null;
        }
    }
}
