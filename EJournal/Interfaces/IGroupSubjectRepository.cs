using EJournal.DTOs;

namespace EJournal.Interfaces;

public interface IGroupSubjectRepository
{
    Task<List<SubjectDto>> GetSubjectsForGroupAsync(int groupId);
    Task AddSubjectToGroupAsync(int groupId, int subjectId);
    Task RemoveSubjectFromGroupAsync(int groupId, int subjectId);
}