using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;

namespace OrchardCore.SearchB.Services
{
    public class SearchBPartContentSearchBProvider : IContentSearchAProvider
    {
        private readonly ISession _session;

        public SearchBPartContentSearchBProvider(ISession session)
        {
            _session = session;
        }

        public int Order => 100;
        
        public async Task<string> GetContentItemIdAsync(string searchB)
        {
            if (searchB.StartsWith("searchB:", System.StringComparison.OrdinalIgnoreCase))
            {
                searchB = searchB.Substring(6);

                var searchAPartIndex = await _session.Query<ContentItem, SearchBPartIndex>(x => x.SearchB == searchB.ToLowerInvariant()).FirstOrDefaultAsync();
                return searchAPartIndex?.ContentItemId;
            }

            return null;
        }
    }
}
