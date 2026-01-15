using EJournal.Data;
using EJournal.DTOs;
using EJournal.Entities;
using EJournal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EJournal.Repositories;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    public GroupRepository(AppDbContext context) : base(context) { }

    public async Task<GroupWithSubjectsDto?> GetGroupWithSubjectsAsync(int groupId)
    {
        return await _context.Groups
            .Where(g => g.GroupId == groupId)
            .Select(g => new GroupWithSubjectsDto
            {
                GroupId = g.GroupId,
                GroupName = g.Name,
                Subjects = g.GroupSubjects
                    .Select(gs => new SubjectDto
                    {
                        SubjectId = gs.Subject.SubjectId,
                        Name = gs.Subject.Name
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}
