namespace ProjectManagementSystem.App.Commands;

public class RemoveTaskCommand
{
    private readonly string _id;
    
    public RemoveTaskCommand(string id)
    {
        _id = id;
    }
    
    public void Execute(State state)
    {
        if (state.TaskCollection == null)
        {
            throw new InvalidOperationException("No tasks to remove from.");
        }
        
        var task = state.TaskCollection.Find(_id);
        
        if (task == null)
        {
            throw new InvalidOperationException($"No task with ID {_id} exists.");
        }
        
        state.TaskCollection.Remove(task);
        state.FlushOptimizedTasks();
    }
}