using TaskManager.Dal.Entities;
using TaskManager.Dal.Models;

namespace TaskManager.Dal.Repositories.Interfaces;

public interface IUserRepository
{
    Task<long[]> Add(UserEntityV1[] users, CancellationToken token);
    
    Task<UserEntityV1[]> Get(UserGetModel query, CancellationToken token);
}