namespace ProjectManagementSystem.Core;

/// <summary>
/// A collection of tasks.
/// </summary>
public class TaskCollection
{
    /// <summary>
    /// The tasks in the collection.
    /// </summary>
    public List<Task> Tasks { get; }
    
    /// <summary>
    /// Creates a new task collection.
    /// </summary>
    /// <param name="tasks">The tasks in the collection.</param>
    public TaskCollection(List<Task> tasks)
    {
        Tasks = tasks;
    }
    
    /// <summary>
    /// Adds a task to the collection.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void Add(Task task)
    {
        Tasks.Add(task);
    }
    
    /// <summary>
    /// Removes a task from the collection.
    /// </summary>
    /// <param name="task">The task to remove.</param>
    public void Remove(Task task)
    {
        Tasks.Remove(task);
    }
}