using TaskManager.Dal.Entities;
using TaskManager.Dal.Models;

namespace TaskManager.Dal.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<long[]> Add(TaskEntityV1[] tasks, CancellationToken token);
    
    Task<TaskEntityV1[]> Get(TaskGetModel query, CancellationToken token);

    Task Assign(AssignTaskModel model, CancellationToken token);
    
    Task<SubTaskModel[]> GetSubTasksInStatus(long parentTaskId, int[] statuses, CancellationToken token);
}