using EJournal.Data;
using EJournal.DTOs;
using EJournal.Interfaces;
using EJournal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EJournal.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/journal")]
public class JournalController : ControllerBase
{
    private readonly JournalService _service;

    public JournalController(JournalService service)
    {
        _service = service;
    }

    [HttpGet]
    public Task<JournalDto> Get(
        int groupId,
        int subjectId,
        int semesterId)
        => _service.GetJournal(groupId, subjectId, semesterId);

    [HttpPost("lesson")]
    public async Task<IActionResult> AddLesson(CreateLessonDto dto)
    {
        await _service.AddLesson(dto);
        return Ok();
    }

    [HttpPut("grade")]
    public async Task<IActionResult> SetGrade(SetGradeDto dto)
    {
        await _service.SetGrade(dto);
        return Ok();
    }

    [HttpDelete("lesson/{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        await _service.DeleteLesson(id);
        return Ok();
    }

    [HttpDelete("grade")]
    public async Task<IActionResult> DeleteGrade(
        int studentId,
        int lessonDateId)
    {
        await _service.DeleteGrade(studentId, lessonDateId);
        return Ok();
    }
}




