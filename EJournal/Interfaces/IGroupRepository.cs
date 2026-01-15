using EJournal.DTOs;
using EJournal.Entities;

namespace EJournal.Interfaces;

public interface  IGroupRepository : IGenericRepository<Group>
{
    Task<GroupWithSubjectsDto?> GetGroupWithSubjectsAsync(int groupId);
}