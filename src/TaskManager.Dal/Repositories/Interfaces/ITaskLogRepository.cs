using TaskManager.Dal.Entities;
using TaskManager.Dal.Models;

namespace TaskManager.Dal.Repositories.Interfaces;

public interface ITaskLogRepository
{
    Task<long[]> Add(TaskLogEntityV1[] tasks, CancellationToken token);
    
    Task<TaskLogEntityV1[]> Get(TaskLogGetModel query, CancellationToken token);
}