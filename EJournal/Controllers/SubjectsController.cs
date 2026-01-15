using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly IGenericRepository<Subject> _repository;

    public SubjectsController(IGenericRepository<Subject> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Subject>> GetAll() => await _repository.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetById(int id)
    {
        var subject = await _repository.GetByIdAsync(id);
        if (subject == null) return NotFound();
        return subject;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Subject subject)
    {
        await _repository.AddAsync(subject);
        return CreatedAtAction(nameof(GetById), new { id = subject.SubjectId }, subject);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Subject subject)
    {
        if (id != subject.SubjectId) return BadRequest();
        await _repository.UpdateAsync(subject);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}