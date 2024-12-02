using FluentMigrator;

namespace TaskManager.Dal.Migrations;

[Migration(20240112143200, TransactionBehavior.None)]
public class AddMonitorTimeToTaskComments : Migration
{
    public override void Up()
    {
        Alter.Table("task_comments")
            .AddColumn("modified_at").AsDateTimeOffset().Nullable()
            .AddColumn("deleted_at").AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete
            .Column("modified_at")
            .Column("deleted_at")
            .FromTable("task_comments");
    }
}
