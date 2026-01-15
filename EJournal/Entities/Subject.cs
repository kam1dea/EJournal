using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;

[Table("Subject")]
public class Subject
{
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public ICollection<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();
}