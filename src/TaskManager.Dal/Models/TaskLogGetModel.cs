namespace TaskManager.Dal.Models;

public record TaskLogGetModel
{
    public required long[] TaskIds { get; init; }
}