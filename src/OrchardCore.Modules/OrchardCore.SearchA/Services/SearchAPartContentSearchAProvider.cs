using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
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
        
        public async Task<string> GetContentItemIdAsync(string searchA)
        {
            if (searchA.StartsWith("searchA:", System.StringComparison.OrdinalIgnoreCase))
            {
                searchA = searchA.Substring(6);

                var searchAPartIndex = await _session.Query<ContentItem, SearchAPartIndex>(x => x.SearchA == searchA.ToLowerInvariant()).FirstOrDefaultAsync();
                return searchAPartIndex?.ContentItemId;
            }

            return null;
        }
    }
}
