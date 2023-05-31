using ProjectManagementSystem.Core;
using Task = System.Threading.Tasks.Task;
using TaskEntity = ProjectManagementSystem.Core.Task;
using TaskCollection = ProjectManagementSystem.Core.TaskCollection;

namespace ProjectManagementSystem.App;

/// <summary>
/// Stores the state of the application.
/// </summary>
public class State
{
    /// <summary>
    /// The collection of tasks.
    /// </summary>
    private TaskCollection _tasks;
    
    /// <summary>
    /// The TaskOptimizer instance. It will internally memoize the results of its operations.
    /// </summary>
    private TaskOptimizer? _optimizer;

    /// <summary>
    /// Initializes application state.
    /// </summary>
    public State()
    {
        _tasks = new TaskCollection();
        _optimizer = null;
    }
    
    /// <summary>
    /// The collection of tasks.
    /// </summary>
    public TaskCollection Tasks
    {
        get => _tasks;
        set
        {
            _tasks = value;
            _optimizer = null;
        }
    }
    
    /// <summary>
    /// Adds a task to the list of tasks.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void AddTask(TaskEntity task)
    {
        _tasks.Add(task);
        _optimizer = null;
    }

    /// <summary>
    /// Updates the duration of a task in the list of tasks.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="duration">The new duration of the task.</param>
    public void UpdateTaskDuration(string id, int duration)
    {
        var index = _tasks.FindIndex(t => t.Id == id);
        _tasks[index].TimeToComplete = duration;
        _optimizer = null;
    }

    public Task<Dictionary<TaskEntity, int>> OptimizedTasks()
    {
        if (_optimizer != null) return Task.FromResult(_optimizer.Optimize());
        _optimizer = new TaskOptimizer(Tasks);
        return Task.FromResult(_optimizer.Optimize());
    }
    
    public Task<TaskCollection> OrderedTasks()
    {
        if (_optimizer != null) return Task.FromResult(_optimizer.Order());
        _optimizer = new TaskOptimizer(Tasks);
        return Task.FromResult(_optimizer.Order());
    }
}