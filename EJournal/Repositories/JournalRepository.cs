using EJournal.Interfaces;

namespace EJournal.Repositories;

using EJournal.Data;
using EJournal.Entities;
using Microsoft.EntityFrameworkCore;

public class JournalRepository : IJournalRepository
{
    private readonly AppDbContext _context;

    public JournalRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> GetStudents(int groupId) =>
        _context.Students
            .Where(s => s.GroupId == groupId)
            .OrderBy(s => s.FullName)
            .ToListAsync();

    public Task<List<LessonDate>> GetLessons(int groupId, int subjectId, int semesterId) =>
        _context.LessonDates
            .Where(l =>
                l.GroupId == groupId &&
                l.SubjectId == subjectId &&
                l.SemesterId == semesterId)
            .OrderBy(l => l.Date)
            .ToListAsync();

    public Task<List<Grade>> GetGrades(List<int> lessonIds) =>
        _context.Grades
            .Where(g => lessonIds.Contains(g.LessonDateId))
            .ToListAsync();

    public async Task<LessonDate> AddLesson(LessonDate lesson)
    {
        _context.LessonDates.Add(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }

    public async Task AddGrades(IEnumerable<Grade> grades)
    {
        _context.Grades.AddRange(grades);
        await _context.SaveChangesAsync();
    }

    public async Task UpsertGrade(int studentId, int lessonDateId, int? value)
    {
        var grade = await _context.Grades
            .FirstOrDefaultAsync(g =>
                g.StudentId == studentId &&
                g.LessonDateId == lessonDateId);

        if (grade == null)
        {
            grade = new Grade
            {
                StudentId = studentId,
                LessonDateId = lessonDateId,
                Value = value
            };
            _context.Grades.Add(grade);
        }
        else
        {
            grade.Value = value;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteLesson(int lessonDateId)
    {
        var lesson = await _context.LessonDates.FindAsync(lessonDateId);
        if (lesson == null) return;

        _context.LessonDates.Remove(lesson);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGrade(int studentId, int lessonDateId)
    {
        var grade = await _context.Grades
            .FirstOrDefaultAsync(g =>
                g.StudentId == studentId &&
                g.LessonDateId == lessonDateId);

        if (grade == null) return;

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();
    }
}
