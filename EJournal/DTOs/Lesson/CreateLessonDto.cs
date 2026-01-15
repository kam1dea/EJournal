namespace EJournal.DTOs;

public class CreateLessonDto
{
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
    public int SemesterId { get; set; } 
    public DateTime Date { get; set; }
}