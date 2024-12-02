using FluentMigrator;

namespace TaskManager.Dal.Migrations;

[Migration(20240112143100, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey("users_pk").Identity()
            .WithColumn("email").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("blocked_at").AsDateTimeOffset().Nullable();
        
        Create.Index("idx_users_email").OnTable("users").OnColumn("email").Ascending();

        Create.Table("task_statuses")
            .WithColumn("id").AsInt64().PrimaryKey("task_statuses_pk")
            .WithColumn("alias").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable();

        Create.Table("tasks")
            .WithColumn("id").AsInt64().PrimaryKey("tasks_pk").Identity()
            .WithColumn("parent_task_id").AsInt64().Nullable()
            .WithColumn("number").AsString().NotNullable()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("created_by_user_id").AsInt64().NotNullable()
            .WithColumn("assigned_to_user_id").AsInt64().Nullable()
            .WithColumn("completed_at").AsDateTimeOffset().Nullable();

        Create.Index("idx_tasks_created_by").OnTable("tasks").OnColumn("created_by_user_id").Ascending();
        Create.Index("idx_tasks_assigned_to").OnTable("tasks").OnColumn("assigned_to_user_id").Ascending();

        Create.ForeignKey("fk_tasks_users_created")
            .FromTable("tasks").ForeignColumn("created_by_user_id")
            .ToTable("users").PrimaryColumn("id");

        Create.ForeignKey("fk_tasks_users_assigned")
            .FromTable("tasks").ForeignColumn("assigned_to_user_id")
            .ToTable("users").PrimaryColumn("id");

        Create.Table("task_logs")
            .WithColumn("id").AsInt64().PrimaryKey("task_logs_pk").Identity()
            .WithColumn("task_id").AsInt64().NotNullable()
            .WithColumn("parent_task_id").AsInt64().Nullable()
            .WithColumn("number").AsString().NotNullable()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("assigned_to_user_id").AsInt64().Nullable()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("at").AsDateTimeOffset().NotNullable();

        Create.Index("idx_task_logs_task_id").OnTable("task_logs").OnColumn("task_id").Ascending();
        Create.Index("idx_task_logs_user_id").OnTable("task_logs").OnColumn("user_id").Ascending();
        Create.ForeignKey("fk_task_logs_tasks")
            .FromTable("task_logs").ForeignColumn("task_id")
            .ToTable("tasks").PrimaryColumn("id");
        
        Create.Table("task_comments")
            .WithColumn("id").AsInt64().PrimaryKey("task_comments_pk").Identity()
            .WithColumn("task_id").AsInt64().NotNullable()
            .WithColumn("author_user_id").AsInt64().NotNullable()
            .WithColumn("message").AsString().NotNullable()
            .WithColumn("at").AsDateTimeOffset().NotNullable();

        Create.Index("idx_task_comments_task_id").OnTable("task_comments").OnColumn("task_id").Ascending();
        Create.ForeignKey("fk_task_comments_tasks")
            .FromTable("task_comments").ForeignColumn("task_id")
            .ToTable("tasks").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.ForeignKey("fk_task_comments_tasks").OnTable("task_comments");
        Delete.ForeignKey("fk_task_logs_tasks").OnTable("task_logs");
        Delete.ForeignKey("fk_tasks_users_assigned").OnTable("tasks");
        Delete.ForeignKey("fk_tasks_users_created").OnTable("tasks");

        Delete.Index("idx_task_comments_task_id").OnTable("task_comments");
        Delete.Index("idx_task_logs_user_id").OnTable("task_logs");
        Delete.Index("idx_task_logs_task_id").OnTable("task_logs");
        Delete.Index("idx_tasks_assigned_to").OnTable("tasks");
        Delete.Index("idx_tasks_created_by").OnTable("tasks");
        Delete.Index("idx_users_email").OnTable("users");
        
        Delete.Table("users");
        Delete.Table("task_statuses");
        Delete.Table("tasks");
        Delete.Table("task_logs");
        Delete.Table("task_comments");
    }
}