using System;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Data.Migration;
using Newtonsoft.Json.Linq;
using YesSql;
using Microsoft.Extensions.Logging;

namespace OrchardCore.SearchA
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;
        private readonly ISession _session;
        private readonly ILogger<Migrations> _logger;

        public Migrations(
            IContentDefinitionManager contentDefinitionManager, 
            ISession session,
            ILogger<Migrations> logger)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _session = session;
            _logger = logger;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("SearchAPart", builder => builder
                .Attachable()
                .WithDescription("Provides a SearchA for your content item.")
                .WithDefaultPosition("0")
                );

            return 2;
        }
        
        public async Task<int> UpdateFrom1()
        {
            // This code can be removed in RC
            // We are patching all content item versions by moving the SearchA to DisplayText
            // This step doesn't need to be executed for a brand new site

            var lastDocumentId = 0;

            for(;;)
            {
                var contentItemVersions = await _session.Query<ContentItem, ContentItemIndex>(x => x.DocumentId > lastDocumentId).Take(10).ListAsync();
                
                if (!contentItemVersions.Any())
                {
                    // No more content item version to process
                    break;
                }

                foreach(var contentItemVersion in contentItemVersions)
                {
                    if (String.IsNullOrEmpty(contentItemVersion.DisplayText)
                        && UpdateSearchAPart(contentItemVersion.Content))
                    {
                        _session.Save(contentItemVersion);
                        _logger.LogInformation($"A content item version's SearchA was upgraded: '{contentItemVersion.ContentItemVersionId}'");
                    }

                    lastDocumentId = contentItemVersion.Id;
                }

                await _session.CommitAsync();
            } 

            bool UpdateSearchAPart(JToken content)
            {
                var changed = false;

                if (content.Type == JTokenType.Object)
                {
                    var searchValue = content["SearchAPart"] ? ["SearchAPart"]?.Value<string>();
                    
                    if (!String.IsNullOrWhiteSpace(searchValue))
                    {
                        content["DisplayText"] = searchValue;
                        changed = true;
                    }
                }

                foreach (var token in content)
                {
                    changed = UpdateSearchAPart(token) || changed;
                }

                return changed;
            }

            return 2;
        }
    }
}