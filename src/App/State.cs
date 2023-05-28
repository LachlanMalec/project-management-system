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
    
    // Memoization of ordered tasks.
    private TaskCollection? _orderedTasks;

    // Memoization of optimized tasks.
    private TaskCollection? _optimizedTasks;
    
    /// <summary>
    /// Initializes application state.
    /// </summary>
    public State()
    {
        _tasks = new TaskCollection();
        _orderedTasks = null;
        _optimizedTasks = null;
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
            _optimizedTasks = null;
            _orderedTasks = null;
        }
    }
    
    /// <summary>
    /// Adds a task to the list of tasks.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void AddTask(TaskEntity task)
    {
        _tasks.Add(task);
        _optimizedTasks = null;
        _orderedTasks = null;
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
        // TODO: Possibly clear memoized tasks orderings.
        //_optimizedTasks = null;
        //_orderedTasks = null;
    }

    public Task<TaskCollection> OptimizedTasks()
    {
        if (_optimizedTasks != null) return Task.FromResult(_optimizedTasks);
        //TODO: Optimize the tasks.
        //_optimizedTasks = Tasks.Optimized();
        _optimizedTasks = Tasks;
        return Task.FromResult(_optimizedTasks);
    }
    
    public Task<TaskCollection> OrderedTasks()
    {
        if (_orderedTasks != null) return Task.FromResult(_orderedTasks);
        //TODO: Order the tasks.
        //_orderedTasks = Tasks.Ordered();
        _orderedTasks = Tasks;
        return Task.FromResult(_orderedTasks);
    }
}