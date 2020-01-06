using System.Threading.Tasks;

namespace OrchardCore.ContentManagement
{
    public interface IContentSearchAManager
    {
        Task<string> GetContentItemIdAsync(string searchA);
    }
}
