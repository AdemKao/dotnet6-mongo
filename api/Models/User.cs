
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace api.Models;

[BsonIgnoreExtraElements]

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = String.Empty;

    [BsonElement("username")]
    public string UserName { get; set; } = String.Empty;

    [BsonElement("password")]
    public byte[] PasswordHash { get; set; }

    [BsonElement("passwordsalt")]
    public byte[] PasswordSalt { get; set; }
}
