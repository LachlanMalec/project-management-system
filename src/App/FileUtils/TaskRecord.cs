namespace ProjectManagementSystem.App.FileUtils;

/// <summary>
/// Represents a Task stored in a CSV file.
/// </summary>
public class TaskRecord
{
    /// <summary>
    /// The ID of the task.
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// The time to complete the task.
    /// </summary>
    public int TimeToComplete { get; set; }
    /// <summary>
    /// The IDs of the tasks on which this task depends.
    /// </summary>
    public List<string> Dependencies { get; set; } = new List<string>();
}