using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.SearchA.Indexes;
using OrchardCore.SearchA.Models;

namespace OrchardCore.SearchA
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition(nameof(SearchAPart), builder => builder
                .Attachable()
                .WithDescription("Provides a way to define custom SearchAes for content items."));

            SchemaBuilder.CreateMapIndexTable(nameof(SearchAPartIndex), table => table
                .Column<string>("SearchA", col => col.WithLength(64))
                .Column<string>("ContentItemId", c => c.WithLength(26))
            );

            SchemaBuilder.AlterTable(nameof(SearchAPartIndex), table => table
                .CreateIndex("IDX_SearchAPartIndex_SearchA", "SearchA")
            );

            return 1;
        }
    }
}
