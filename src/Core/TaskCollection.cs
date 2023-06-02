using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.Core;

/// <summary>
/// A collection of tasks.
/// </summary>
public class TaskCollection : HashSet<TaskEntity>
{
    /// <summary>
    /// Finds a task by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The task with the matching id, or null if it does not exist.</returns>
    public TaskEntity? FindById(string id)
    {
        return this.FirstOrDefault(task => task.Id == id);
    }
}