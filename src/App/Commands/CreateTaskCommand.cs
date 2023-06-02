using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.Commands;

public class CreateTaskCommand
{
    private readonly string _id;
    private readonly int _timeToComplete;
    private readonly List<string> _dependencies;
    
    /// <summary>
    /// A command to create a task.
    /// </summary>
    /// <param name="id">The ID of the task.</param>
    /// <param name="timeToComplete">The time to complete the task.</param>
    /// <param name="dependencies">The IDs of the tasks that this task depends on.</param>
    public CreateTaskCommand(string id, int timeToComplete, List<string> dependencies)
    {
        _id = id;
        _timeToComplete = timeToComplete;
        _dependencies = dependencies;
    }
    
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public void Execute(State state)
    {
        var dependencies = new List<TaskEntity>();

        if (_dependencies.Count > 0)
        {
            foreach (var dependency in _dependencies)
            {
                var task = state.Tasks.FindById(dependency);

                if (task == null)
                {
                    throw new InvalidOperationException($"No task with ID {dependency} exists.");
                }

                dependencies.Add(task);
            }
        }

        var newTask = new TaskEntity(_id, _timeToComplete, dependencies);
        
        state.AddTask(newTask);
    }
}