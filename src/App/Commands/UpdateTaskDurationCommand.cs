namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to update the duration of a task. 
/// </summary>
public class UpdateTaskDurationCommand
{
    private readonly string _id;
    private readonly int _duration;
    
    /// <summary>
    /// Creates a command that when executed will update the duration of a task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="duration">The new duration of the task.</param>
    public UpdateTaskDurationCommand(string id, int duration)
    {
        _id = id;
        _duration = duration;
    }
    
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public void Execute(State state)
    {
        state.UpdateTaskDuration(_id, _duration);
    }
}