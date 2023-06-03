using System.Diagnostics;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.Core;

/// <summary>
/// An optimizer for a collection of tasks.
/// The input task collection will not be modified.
/// </summary>
public class TaskOptimizer
{
    private readonly TaskCollection _tasks;

    // Memoization of topological order.
    private TaskCollection? _topologicalOrder;
    
    // Memoization of earliest start times.
    private Dictionary<TaskEntity, int>? _earliestStartTimes;

    /// <summary>
    /// Creates a new task optimizer.
    /// </summary>
    /// <param name="tasks">The task collection that will be optimized.</param>
    public TaskOptimizer(TaskCollection tasks)
    {
        _tasks = tasks;
    }

    /// <summary>
    /// Computes a valid execution order of the tasks.
    /// </summary>
    /// <returns>A task collection in valid order.</returns>
    public TaskCollection Order()
    {
        if (_topologicalOrder == null)
        {
            TopologicalSort();
        }
        return _topologicalOrder!;
    }

    /// <summary>
    /// Computes the earliest start time for each task.
    /// </summary>
    /// <returns>A dictionary of tasks and their earliest start times.</returns>
    public Dictionary<TaskEntity, int> Optimize()
    {
        // Check if the earliest start times have already been calculated.
        if (_earliestStartTimes != null)
        {
            return _earliestStartTimes;
        }
        
        // Get the topological order.
        if (_topologicalOrder == null)
        {
            TopologicalSort();
        }

        var endTimes = new Dictionary<TaskEntity, int>();
        
        // Calculate the end time of each task.
        foreach (var task in _topologicalOrder!)
        {
            CalculateEndTime(task, endTimes);
        }
        
        var startTimes = new Dictionary<TaskEntity, int>();
        
        // Calculate the start time of each task.
        foreach (var task in endTimes)
        {
            startTimes.Add(task.Key, task.Value - task.Key.TimeToComplete);
        }
        
        // Order the tasks by the user created collection order.
        var orderedStartTimes = new Dictionary<TaskEntity, int>();
        foreach (var task in _tasks)
        {
            orderedStartTimes.Add(task, startTimes[task]);
        }

        _earliestStartTimes = orderedStartTimes;
        return orderedStartTimes;
    }
    
    /// <summary>
    /// Calculates the end time of a task.
    /// </summary>
    /// <param name="task">The task to calculate the end time of.</param>
    /// <param name="endTimes">The dictionary of already calculated end times.</param>
    private void CalculateEndTime(TaskEntity task, Dictionary<TaskEntity, int> endTimes)
    {
        if (endTimes.TryGetValue(task, out var time))
        {
            return;
        }
        
        var max = 0;
        foreach (var dependency in task.Dependencies)
        {
            var dependencyStartTime = endTimes[dependency];
            if (dependencyStartTime > max)
            {
                max = dependencyStartTime;
            }
        }
        
        var startTime = max + task.TimeToComplete;
        
        endTimes.Add(task, startTime);
    }

    /// <summary>
    /// A topological sort using Kahn's algorithm.
    /// </summary>
    /// <exception cref="Exception">Thrown if the graph is not a valid DAG.</exception>
    private void TopologicalSort()
    {
        // Create a dictionary to store in-degrees of all vertices.
        Dictionary<Task, int> inDegree = _tasks.ToDictionary(task => task, task => 0);

        // Traverse dependencies of each task to fill in-degrees of vertices.
        foreach (var task in _tasks) {
            foreach (var dep in task.Dependencies) {
                inDegree[dep]++;
            }
        }

        // Create a queue and enqueue all vertices with in-degree 0.
        Queue<Task> queue = new Queue<Task>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));

        // Initialize count of visited vertices.
        var count = 0;

        // Create a list to store result (A topological ordering of the vertices).
        TaskCollection topOrder = new TaskCollection();

        while (queue.Count > 0) {
            // Extract front of queue and add it to topological order.
            Task u = queue.Dequeue();
            topOrder.Add(u);

            // Iterate through all its dependencies (neighboring nodes).
            // If in-degree of neighboring nodes is reduced to zero, then add it to the queue.
            foreach (Task neighbor in u.Dependencies) {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0) {
                    queue.Enqueue(neighbor);
                }
            }

            count++;
        }

        // Check if there was a cycle.
        if (count != inDegree.Count) {
            throw new Exception("Graph is not a valid DAG.");
        }

        var taskCollection = new TaskCollection();
        foreach (var task in topOrder.Reverse()) {
            taskCollection.Add(task);
        }
        
        _topologicalOrder = taskCollection;
    }
}
