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
    public async Task<ActionResult<List<Student>>> Get()
    {
        return await studentService.GetAsync();
    }

    // GET  api/v1/<StudentsController>/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> Get(string id)
    {
        var student = await studentService.GetAsync(id);
        if (student == null)
            return NotFound($"Student with Id = {id} not found.");
        return student;
    }

    // POST  api/v1/<StudentsController>
    [HttpPost]
    public async Task<ActionResult<Student>> Post(Student student)
    {
        await studentService.CreateAsync(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    // Put  api/v1/<StudentsController>
    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> Put(string id, [FromBody] Student student)
    {
        var existingStudent = await studentService.GetAsync(id);
        if (existingStudent == null)
            return NotFound($"Student with Id = {id} not found.");

        await studentService.UpdateAsync(id, student);
        // return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        return NoContent();
    }

    // Delete  api/v1/<StudentsController>/id
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var existingStudent = await studentService.GetAsync(id);
        if (existingStudent == null)
            return NotFound($"Student with Id = {id} not found.");

        await studentService.RemoveAsync(id);

        return Ok($"Student with Id = {id} deleted");
    }
}