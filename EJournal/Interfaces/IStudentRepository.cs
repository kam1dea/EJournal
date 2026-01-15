using EJournal.DTOs.Student;
using EJournal.Entities;
using StudentJournalDto = EJournal.DTOs.StudentJournalDto;

namespace EJournal.Interfaces;

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<List<StudentJournalDto>> GetStudentsForJournalAsync(int groupId, int subjectId, int semesterId);
    Task<List<StudentInGroupDto>> GetByGroupAsync(int groupId);
    Task<List<StudentListDto>> GetAllWithGroupAsync();
}