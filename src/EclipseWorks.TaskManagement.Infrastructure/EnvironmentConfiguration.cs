namespace EclipseWorks.TaskManagement.Infrastructure;

public record EnvironmentConfiguration
{
    public required MongoDbConfiguration MongoDbConfiguration { get; init; }
}

public record MongoDbConfiguration(
    string ConnectionString,
    string DataBaseName,
    CollectionsConfig Collections)
{
}

public record CollectionsConfig
{
    public required string Projects { get; init; }
    public required string TaskManagementHistory { get; init; }
}