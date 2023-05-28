namespace ProjectManagementSystem.Core;

public class TaskCollection : List<Task>
{
    /// <summary>
    /// Finds a task by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The task with the matching id, or null if it does not exist.</returns>
    public Task? FindById(string id)
    {
        var index = FindIndex(task => task.Id == id);
        return index < 0 ? null : this[index];
    }
}