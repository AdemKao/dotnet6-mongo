// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

Console.WriteLine("Hello, World!");

var mongoUri = "";
IMongoClient client;
IMongoCollection<Student> _students;

try
{
    client = new MongoClient(mongoUri);
}
catch (Exception e)
{
    Console.WriteLine("There was a problem connecting to your " +
        "Atlas cluster. Check that the URI includes a valid " +
        "username and password, and that your IP address is " +
        $"in the Access List. Message: {e.Message}");
    Console.WriteLine(e);
    Console.WriteLine();
    return;
}
var dbName = "SideProject";
var collectionName = "studentcourses";

_students = client.GetDatabase(dbName)
   .GetCollection<Student>(collectionName);


try
{
    Student student = new Student() { Name = "Adem" };
    _students.InsertOne(student);
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong trying to insert the new documents." +
        $" Message: {e.Message}");
    Console.WriteLine(e);
    Console.WriteLine();
    return;
}


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
