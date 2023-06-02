namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to remove a task from the list of tasks.
/// </summary>
public class RemoveTaskCommand
{
    private readonly string _id;
    
    /// <summary>
    /// Creates a command that when executed will remove a task from the list of tasks.
    /// The task will also be removed from the dependencies of any other tasks.
    /// </summary>
    /// <param name="id">The ID of the task to remove.</param>
    public RemoveTaskCommand(string id)
    {
        _id = id;
    }
    
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public void Execute(State state)
    {
        state.RemoveTask(_id);
    }
}