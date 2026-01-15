namespace EJournal.DTOs;

public class GroupWithSubjectsDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = null!;
    public List<SubjectDto> Subjects { get; set; } = new();
}