using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _repository;

    public StudentsController(IStudentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllWithGroupAsync());
    }

    [HttpGet("group/{groupId}")]
    public async Task<IActionResult> GetByGroup(int groupId)
    {
        return Ok(await _repository.GetByGroupAsync(groupId));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return NotFound();
        return student;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Student student)
    {
        await _repository.AddAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Student student)
    {
        if (id != student.StudentId) return BadRequest();
        await _repository.UpdateAsync(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}