using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "SearchA",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "2.0.0",
    Description = "The searchA module enables content items to have search values.",
    Dependencies = new[] { "OrchardCore.Contents" },
    Category = "Content Management"
)]
