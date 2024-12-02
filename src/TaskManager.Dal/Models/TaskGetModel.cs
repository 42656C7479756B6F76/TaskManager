namespace TaskManager.Dal.Models;

public record TaskGetModel
{
    public required long[] TaskIds { get; init; }
}