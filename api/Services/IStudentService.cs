using api.Models;

namespace api.Services;

public interface IStudentService
{
    Task<List<Student>> GetAsync();
    Task<Student> GetAsync(string id);
    Task<Student> CreateAsync(Student student);
    Task UpdateAsync(string id, Student student);
    Task RemoveAsync(string id);
}
