using EJournal.Data;
using EJournal.Entities;
using EJournal.Interfaces;

namespace EJournal.Repositories;

public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(AppDbContext context) : base(context) { }
}
