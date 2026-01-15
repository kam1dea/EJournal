using EJournal.Data;
using EJournal.DTOs.Student;
using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EJournal.Repositories;

public class StudentRepository: GenericRepository<Student>, IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    // ЖУРНАЛ
    public async Task<List<StudentJournalDto>> GetStudentsForJournalAsync(int groupId, int subjectId, int semesterId)
    {
        var lessons = await _context.LessonDates
            .Where(l => l.SubjectId == subjectId && l.SemesterId == semesterId)
            .Select(l => l.LessonDateId)
            .ToListAsync();

        var students = await _context.Students
            .Where(s => s.GroupId == groupId)
            .Select(s => new StudentJournalDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                Grades = lessons.ToDictionary(
                    lessonId => lessonId,
                    lessonId => (int?)_context.Grades
                        .Where(g => g.StudentId == s.StudentId && g.LessonDateId == lessonId)
                        .Select(g => g.Value)
                        .FirstOrDefault()
                )
            })
            .ToListAsync();

        return students;
    }

    // Студенты одной группы
    public async Task<List<StudentInGroupDto>> GetByGroupAsync(int groupId)
    {
        return await _context.Students
            .Where(s => s.GroupId == groupId)
            .Select(s => new StudentInGroupDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName
            })
            .ToListAsync();
    }

    // Все студенты + группы
    public async Task<List<StudentListDto>> GetAllWithGroupAsync()
    {
        return await _context.Students
            .Select(s => new StudentListDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                GroupId = s.GroupId,
                GroupName = s.Group.Name
            })
            .ToListAsync();
    }
}