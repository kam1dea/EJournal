using EJournal.DTOs.Student;

namespace EJournal.DTOs;

public class JournalDto
{
    public List<StudentInGroupDto> Students { get; set; } = [];
    public List<LessonDto> Lessons { get; set; } = [];
    public List<GradeDto> Grades { get; set; } = [];
}


