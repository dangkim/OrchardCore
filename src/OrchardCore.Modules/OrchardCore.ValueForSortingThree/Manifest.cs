using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "ValueForSortingThree",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "2.0.0",
    Description = "The ValueForSortingThree module enables content items to have Value For Sorting.",
    Dependencies = new[] { "OrchardCore.Contents" },
    Category = "Content Management"
)]
