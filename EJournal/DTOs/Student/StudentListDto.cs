namespace EJournal.DTOs.Student;

public class StudentListDto
{
    public int StudentId { get; set; }
    public string FullName { get; set; } = null!;
    public int GroupId { get; set; }
    public string GroupName { get; set; } = null!;
}