using Dapper;
using TaskManager.Dal.Entities;
using TaskManager.Dal.Models;
using TaskManager.Dal.Repositories.Interfaces;
using TaskManager.Dal.Settings;
using Microsoft.Extensions.Options;

namespace TaskManager.Dal.Repositories;

public class TaskCommentRepository : PgRepository, ITaskCommentRepository
{
    public TaskCommentRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value) {}
    
    public async Task<long> Add(TaskCommentEntityV1 model, CancellationToken token)
    {
        const string sqlQuery = @"
insert into task_comments (task_id, author_user_id, message, at) 
values (@TaskId, @AuthorUserId, @Message, @At) 
returning id;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryFirstAsync<long>(
            new CommandDefinition(
                sqlQuery,
                model,
                cancellationToken: token));

        return id;
    }

    public async Task Update(TaskCommentEntityV1 model, CancellationToken token)
    {
        const string sqlQuery = @"
update task_comments
   set message = @Message,
       modified_at = current_timestamp
 where id = @Id;
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                model,
                cancellationToken: token));
    }
    
    public async Task SetDeleted(long commentId, CancellationToken token)
    {
        const string sqlQuery = @"
update task_comments
   set deleted_at = current_timestamp
 where id = @CommentId;
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    CommentId = commentId
                },
                cancellationToken: token));
    }

    public async Task<TaskCommentEntityV1[]> Get(TaskCommentGetModel model, CancellationToken token)
    {
        const string sqlQuery = @"
select id, task_id, author_user_id, message, at, modified_at, deleted_at
  from task_comments
 where task_id = @TaskId
   and case 
           when @IncludeDeleted = true then true
           else deleted_at is null end
 order by at desc;
";

        await using var connection = await GetConnection();
        var comments = await connection.QueryAsync<TaskCommentEntityV1>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    model.TaskId,
                    model.IncludeDeleted
                },
                cancellationToken: token));

        return comments.ToArray();
    }
}
