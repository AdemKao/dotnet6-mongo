using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.Models;

namespace api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private IStudentService studentService { get; }
    public StudentsController(IStudentService studentService)
    {
        this.studentService = studentService;

    }
    // GET  api/v1/<StudentsController>
    [HttpGet]
    public ActionResult<List<Student>> Get()
    {
        return studentService.Get();
    }

    // GET  api/v1/<StudentsController>/id
    [HttpGet("{id}")]
    public ActionResult<Student> Get(string id)
    {
        var student = studentService.Get(id);
        if (student == null)
            return NotFound($"Student with Id = {id} not found.");
        return student;
    }

    // POST  api/v1/<StudentsController>
    [HttpPost]
    public ActionResult<Student> Post(Student student)
    {
        studentService.Create(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    // Put  api/v1/<StudentsController>
    [HttpPut("{id}")]
    public ActionResult<Student> Put(string id, [FromBody] Student student)
    {
        var existingStudent = studentService.Get(id);
        if (existingStudent == null)
            return NotFound($"Student with Id = {id} not found.");

        studentService.Update(id, student);
        // return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        return NoContent();
    }

    // Delete  api/v1/<StudentsController>/id
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var existingStudent = studentService.Get(id);
        if (existingStudent == null)
            return NotFound($"Student with Id = {id} not found.");

        studentService.Remove(id);

        return Ok($"Student with Id = {id} deleted");
    }
}