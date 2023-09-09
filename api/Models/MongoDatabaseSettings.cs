namespace api.Models;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string StudentCollectionName { get; set; } = String.Empty;
}
