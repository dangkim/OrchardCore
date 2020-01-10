using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "SearchB",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "1.0.0",
    Description = "The searchB module enables content items to have custom logical identifier.",
    Dependencies = new [] { "OrchardCore.ContentTypes" },
    Category = "Content Management"
)]
