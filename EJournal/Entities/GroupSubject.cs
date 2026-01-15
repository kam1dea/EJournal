namespace EJournal.Entities;

public class GroupSubject
{
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}