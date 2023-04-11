namespace TaskListAPIProject.Models;

public class TaskItemUpdate
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}