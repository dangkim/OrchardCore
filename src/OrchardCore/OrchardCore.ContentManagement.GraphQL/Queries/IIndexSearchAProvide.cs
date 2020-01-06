using System.Collections.Generic;

namespace OrchardCore.ContentManagement.GraphQL.Queries
{
    public interface IIndexSearchAProvider
    {
        IEnumerable<IndexSearchA> GetSearchAes();
    }
}