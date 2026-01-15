using EJournal.DTOs;
using EJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.Controllers;

[ApiController]
[Route("api/groups/{groupId}/subjects")]
public class GroupSubjectsController : ControllerBase
{
    private readonly IGroupSubjectRepository _repo;

    public GroupSubjectsController(IGroupSubjectRepository repo)
    {
        _repo = repo;
    }

    // 🔹 предметы группы
    [HttpGet]
    public async Task<IActionResult> GetSubjects(int groupId)
        => Ok(await _repo.GetSubjectsForGroupAsync(groupId));

    // 🔹 добавить предмет группе
    [HttpPost]
    public async Task<IActionResult> AddSubject(
        int groupId,
        GroupSubjectCreateDto dto)
    {
        await _repo.AddSubjectToGroupAsync(groupId, dto.SubjectId);
        return Ok();
    }

    // 🔹 удалить предмет у группы
    [HttpDelete("{subjectId}")]
    public async Task<IActionResult> RemoveSubject(
        int groupId,
        int subjectId)
    {
        await _repo.RemoveSubjectFromGroupAsync(groupId, subjectId);
        return Ok();
    }
}
