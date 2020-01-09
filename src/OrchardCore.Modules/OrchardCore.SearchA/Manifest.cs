using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "SearchA",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "1.0.0",
    Description = "The searchA module enables content items to have custom logical identifier.",
    Dependencies = new [] { "OrchardCore.ContentTypes" },
    Category = "Content Management"
)]
