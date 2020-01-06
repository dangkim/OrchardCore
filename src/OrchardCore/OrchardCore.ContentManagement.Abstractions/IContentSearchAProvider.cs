using System.Threading.Tasks;

namespace OrchardCore.ContentManagement
{
    public interface IContentSearchAProvider
    {
        int Order { get; }
        Task<string> GetContentItemIdAsync(string SearchA);
    }
}
