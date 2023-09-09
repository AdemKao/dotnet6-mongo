using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

[BsonIgnoreExtraElements]
public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = String.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = String.Empty;

    [BsonElement("firstname")]
    public string FirstName { get; set; } = String.Empty;

    [BsonElement("lastname")]
    public string LastName { get; set; } = String.Empty;

    [BsonElement("graduated")]
    public bool IsGraduated { get; set; }

    [BsonElement("courses")]
    public string[]? Courses { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; } = String.Empty;

    [BsonElement("age")]
    public int Age { get; set; } = 0;

}
