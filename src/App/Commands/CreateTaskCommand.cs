using Task = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.Commands;

public class CreateTaskCommand
{
    private readonly string _id;
    private readonly int _timeToComplete;
    private readonly List<string> _dependencies;
    
    public CreateTaskCommand(string id, int timeToComplete, List<string> dependencies)
    {
        _id = id;
        _timeToComplete = timeToComplete;
        _dependencies = dependencies;
    }
    
    public void Execute(State state)
    {
        if (state.TaskCollection == null)
        {
            throw new InvalidOperationException("No tasks to add to.");
        }
        
        var dependencies = new List<Task>();

        if (_dependencies.Count > 0)
        {
            foreach (var dependency in _dependencies)
            {
                var task = state.TaskCollection.Find(dependency);

                if (task == null)
                {
                    throw new InvalidOperationException($"No task with ID {dependency} exists.");
                }

                dependencies.Add(task);
            }
        }

        var newTask = new Task(_id, _timeToComplete, dependencies);
        
        state.TaskCollection.Add(newTask);
        state.FlushOptimizedTasks();
    }
}