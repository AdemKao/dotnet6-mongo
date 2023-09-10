namespace api.Models;

public interface IMongoDbSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
    string StudentCollectionName { get; set; }
    string UserCollectionName { get; set; }
}
