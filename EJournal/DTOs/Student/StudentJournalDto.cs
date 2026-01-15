namespace EJournal.DTOs.Student;

public class StudentJournalDto
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = null!;
    public Dictionary<int, int?> Grades { get; set; } = new();
    // key = LessonDateId, value = grade (1-5 or null)
}