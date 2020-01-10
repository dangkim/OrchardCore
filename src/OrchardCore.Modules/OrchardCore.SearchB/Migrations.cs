using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Data.Migration;
using OrchardCore.SearchB.Models;

namespace OrchardCore.SearchB
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
            _contentDefinitionManager.AlterPartDefinition(nameof(SearchBPart), builder => builder
                .Attachable()
                .WithDescription("Provides a way to define custom SearchB for content items."));

            SchemaBuilder.CreateMapIndexTable(nameof(SearchBPartIndex), table => table
                .Column<string>("SearchB", col => col.WithLength(1024))
                .Column<string>("ContentItemId", c => c.WithLength(26))
            );

            SchemaBuilder.AlterTable(nameof(SearchBPartIndex), table => table
                .CreateIndex("IDX_SearchBPartIndex_SearchB", "SearchB")
            );

            return 1;
        }
    }
}
