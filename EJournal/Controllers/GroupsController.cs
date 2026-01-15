using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    private readonly IGroupRepository _repository;

    public GroupsController(IGroupRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Group>> GetAll() => await _repository.GetAllAsync();

    // 🔹 ГРУППА + ПРЕДМЕТЫ
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var group = await _repository.GetGroupWithSubjectsAsync(id);
        return group == null ? NotFound() : Ok(group);
    }
    

    [HttpPost]
    public async Task<ActionResult> Create(Group group)
    {
        await _repository.AddAsync(group);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Group group)
    {
        if (id != group.GroupId) return BadRequest();
        await _repository.UpdateAsync(group);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}