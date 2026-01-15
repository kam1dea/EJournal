using EJournal.Data;
using EJournal.DTOs;
using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EJournal.Repositories;

public class GroupSubjectRepository : IGroupSubjectRepository
{
    private readonly AppDbContext _context;

    public GroupSubjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubjectDto>> GetSubjectsForGroupAsync(int groupId)
    {
        return await _context.GroupSubjects
            .Where(gs => gs.GroupId == groupId)
            .Select(gs => new SubjectDto
            {
                SubjectId = gs.SubjectId,
                Name = gs.Subject.Name
            })
            .ToListAsync();
    }

    public async Task AddSubjectToGroupAsync(int groupId, int subjectId)
    {
        var exists = await _context.GroupSubjects
            .AnyAsync(gs => gs.GroupId == groupId && gs.SubjectId == subjectId);

        if (!exists)
        {
            _context.GroupSubjects.Add(new GroupSubject
            {
                GroupId = groupId,
                SubjectId = subjectId
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveSubjectFromGroupAsync(int groupId, int subjectId)
    {
        var gs = await _context.GroupSubjects
            .FirstOrDefaultAsync(x => x.GroupId == groupId && x.SubjectId == subjectId);

        if (gs != null)
        {
            _context.GroupSubjects.Remove(gs);
            await _context.SaveChangesAsync();
        }
    }
}
