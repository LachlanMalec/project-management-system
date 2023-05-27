using ProjectManagementSystem.Core;
using Task = ProjectManagementSystem.Core.Task;

namespace ProjectManagementSystem.App;

/// <summary>
/// Stores the state of the application.
/// </summary>
public class State
{
    private TaskCollection _taskCollection;
    
    // Memoization of optimized tasks.
    private List<Task>? _optimizedTasks;
    
    /// <summary>
    /// Initializes application state.
    /// </summary>
    public State()
    {
        _taskCollection = new TaskCollection(new List<Task>());
        _optimizedTasks = null;
    }
    
    /// <summary>
    /// The global task collection.
    /// </summary>
    public TaskCollection? TaskCollection
    {
        get => _taskCollection;
        set
        {
            _taskCollection = value;
            FlushOptimizedTasks();
        }
    }
    
    /// <summary>
    /// Flushes the memoized optimized tasks.
    /// </summary>
    public void FlushOptimizedTasks()
    {
        _optimizedTasks = null;
    }
    
    /// <summary>
    /// Gets the optimized tasks. If the tasks have not been optimized yet, they will be optimized.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public List<Task> OptimizedTasks
    {
        get
        {
            if (_optimizedTasks != null)
            {
                return _optimizedTasks;
            }
            if (_taskCollection == null)
            {
                throw new InvalidOperationException("No tasks to optimize.");
            }
            var taskOptimizer = new TaskOptimizer(_taskCollection);
            _optimizedTasks = taskOptimizer.Optimize();

            return _optimizedTasks;
        }
    }
}