using EJournal.Data;
using EJournal.DTOs;
using EJournal.DTOs.Student;
using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EJournal.Services;

using EJournal.DTOs;
using EJournal.Entities;

public class JournalService
{
    private readonly IJournalRepository _repo;

    public JournalService(IJournalRepository repo)
    {
        _repo = repo;
    }

    public async Task<JournalDto> GetJournal(
        int groupId,
        int subjectId,
        int semesterId)
    {
        var students = await _repo.GetStudents(groupId);
        var lessons = await _repo.GetLessons(groupId, subjectId, semesterId);

        var lessonIds = lessons.Select(l => l.LessonDateId).ToList();
        var grades = lessonIds.Count == 0
            ? new List<Grade>()
            : await _repo.GetGrades(lessonIds);

        return new JournalDto
        {
            Students = students.Select(s => new StudentInGroupDto()
            {
                StudentId = s.StudentId,
                FullName = s.FullName
            }).ToList(),

            Lessons = lessons.Select(l => new LessonDto
            {
                Id = l.LessonDateId,
                Date = DateOnly.FromDateTime(l.Date)
            }).ToList(),

            Grades = grades.Select(g => new GradeDto
            {
                StudentId = g.StudentId,
                LessonDateId = g.LessonDateId,
                Value = g.Value
            }).ToList()
        };
    }

    public async Task AddLesson(CreateLessonDto dto)
    {
        var lesson = await _repo.AddLesson(new LessonDate
        {
            GroupId = dto.GroupId,
            SubjectId = dto.SubjectId,
            SemesterId = dto.SemesterId,
            Date = dto.Date
        });

        var students = await _repo.GetStudents(dto.GroupId);

        var grades = students.Select(s => new Grade
        {
            StudentId = s.StudentId,
            LessonDateId = lesson.LessonDateId,
            Value = null
        });

        await _repo.AddGrades(grades);
    }

    public Task SetGrade(SetGradeDto dto) =>
        _repo.UpsertGrade(dto.StudentId, dto.LessonDateId, dto.Value);

    public Task DeleteLesson(int lessonDateId) =>
        _repo.DeleteLesson(lessonDateId);

    public Task DeleteGrade(int studentId, int lessonDateId) =>
        _repo.DeleteGrade(studentId, lessonDateId);
}

