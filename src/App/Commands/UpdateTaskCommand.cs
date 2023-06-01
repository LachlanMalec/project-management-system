namespace ProjectManagementSystem.App.Commands;

public class UpdateTaskCommand
{
    private readonly string _id;
    private readonly int _duration;
    
    public UpdateTaskCommand(string id, int duration)
    {
        _id = id;
        _duration = duration;
    }
    
    public void Execute(State state)
    {
        state.UpdateTaskDuration(_id, _duration);
    }
}