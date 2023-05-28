namespace ProjectManagementSystem.Core;

/// <summary>
/// Represents a task.
/// </summary>
/// <remarks> Implements custom comparable and equatable behaviour to enforce logical rules.</remarks>
public class Task : IComparable<Task>, IEquatable<Task>
{
    /// <summary>
    /// The unique identifier of the task.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The time the task will take to complete.
    /// </summary>
    public int TimeToComplete { get; set; }
    
    /// <summary>
    /// The tasks on which this task depends.
    /// </summary>
    public List<Task> Dependencies { get; }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <param name="timeToComplete">The time the task will take to complete.</param>
    /// <param name="dependencies">The tasks on which this task depends.</param>
    public Task(string id, int timeToComplete, List<Task> dependencies)
    {
        Id = id;
        TimeToComplete = timeToComplete;
        Dependencies = dependencies;
    }

    /// <summary>
    /// Compares the equality of two tasks.
    /// </summary>
    /// <param name="task">The task to compare to.</param>
    /// <returns>True if the tasks are equal, false otherwise.</returns>
    public bool Equals(Task task)
    {
        return Id == task.Id;
    }
    
    /// <summary>
    /// Compare the order of two tasks.
    /// </summary>
    /// <param name="task">The task to compare to.</param>
    /// <returns>Less than zero if this task precedes the other task in the sort order, greater than zero if this task follows the other task in the sort order, and 0 if the tasks are equal in the sort order.</returns>
    public int CompareTo(Task task)
    {
        Console.WriteLine($"Comparing {Id} to {task.Id}");
        return string.CompareOrdinal(Id, task.Id);
    }
}