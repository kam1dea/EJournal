namespace EJournal.DTOs;

public class SetGradeDto
{
    public int StudentId { get; set; }
    public int LessonDateId { get; set; }
    public int? Value { get; set; }
}