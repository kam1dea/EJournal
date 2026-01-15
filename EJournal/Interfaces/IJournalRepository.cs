using EJournal.Entities;

namespace EJournal.Interfaces;

public interface IJournalRepository
{
    Task<List<Student>> GetStudents(int groupId);
    Task<List<LessonDate>> GetLessons(int groupId, int subjectId, int semesterId);
    Task<List<Grade>> GetGrades(List<int> lessonIds);

    Task<LessonDate> AddLesson(LessonDate lesson);
    Task AddGrades(IEnumerable<Grade> grades);

    Task UpsertGrade(int studentId, int lessonDateId, int? value);

    Task DeleteLesson(int lessonDateId);
    Task DeleteGrade(int studentId, int lessonDateId);
}