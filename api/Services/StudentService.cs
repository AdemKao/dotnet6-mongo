using api.Models;
using MongoDB.Driver;

namespace api.Services;

public class StudentService : IStudentService
{
    private readonly IMongoCollection<Student> _students;

    // use ctor tab to create constructor quickly
    public StudentService(IMongoDbSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _students = database.GetCollection<Student>(settings.StudentCollectionName);
    }
    public async Task<Student> CreateAsync(Student student)
    {
        await _students.InsertOneAsync(student);
        return student;
    }

    public async Task<List<Student>> GetAsync() =>
        await _students.Find(student => true).ToListAsync();

    public async Task<Student> GetAsync(string id) =>
        await _students.Find(student => student.Id == id).FirstOrDefaultAsync();

    public async Task RemoveAsync(string id) =>
        await _students.DeleteOneAsync(student => student.Id == id);

    public async Task UpdateAsync(string id, Student student) =>
        await _students.ReplaceOneAsync(student => student.Id == id, student);
}
