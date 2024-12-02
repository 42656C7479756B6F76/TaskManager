using TaskManager.Dal.Entities;
using TaskManager.Dal.Models;

namespace TaskManager.Dal.Repositories.Interfaces;

public interface ITaskCommentRepository
{
    Task<long> Add(TaskCommentEntityV1 model, CancellationToken token);
    
    Task Update(TaskCommentEntityV1 model, CancellationToken token);
    
    Task SetDeleted(long commentId, CancellationToken token);
    
    Task<TaskCommentEntityV1[]> Get(TaskCommentGetModel model, CancellationToken token);
}
