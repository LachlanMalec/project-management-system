using System.Diagnostics;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.Core;

public class TaskOptimizer
{
    private TaskCollection _tasks;
    private HashSet<Tuple<TaskEntity, TaskEntity>> _edges;
    
    // Memoization of topologically sorted tasks.
    private TaskCollection? _topologicalOrder;

    public TaskOptimizer(TaskCollection tasks)
    {
        _tasks = tasks;
        _edges = new HashSet<Tuple<TaskEntity, TaskEntity>>();
        foreach (var task in tasks)
        {
            foreach (var dependency in task.Dependencies)
            {
                _edges.Add(new Tuple<TaskEntity, TaskEntity>(dependency, task));
            }
        }
    }

    public TaskCollection Order()
    {
        if (_topologicalOrder == null)
        {
            TopologicalSort();
        }
        return _topologicalOrder!;
    }

    public Dictionary<TaskEntity, int> Optimize()
    {
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
        
        return startTimes;
    }
    
    /// <summary>
    /// Calculates the end time of a task.
    /// </summary>
    /// <param name="task">The task to calculate the end time of.</param>
    /// <param name="endTimes">The dictionary of end already calculated end times.</param>
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
    /// <exception cref="Exception"></exception>
    private void TopologicalSort()
    {
        // List of optimized tasks.
        var l = new TaskCollection();
        
        // Set of tasks with no incoming edges.
        var s = new HashSet<TaskEntity>();
        foreach (var task in _tasks)
        {
            if (task.Dependencies.Count == 0)
            {
                s.Add(task);
            }
        }

        // While there are nodes with no incoming edges.
        while (s.Any())
        {
            // Remove a node with no incoming edges.
            var n = s.First();
            s.Remove(n);
            
            // Add the node to the optimized task collection.
            l.Add(n);

            // Remove all outgoing edges from the node.
            var toRemove = new List<Tuple<TaskEntity, TaskEntity>>();
            foreach (var edge in _edges)
            {
                if (edge.Item1 == n)
                {
                    toRemove.Add(edge);
                }
            }
            
            // Remove the edges.
            foreach (var edge in toRemove)
            {
                _edges.Remove(edge);
            }
            
            // For each node that had an edge removed, if it now has no incoming edges, add it to the set of nodes with no incoming edges.
            foreach (var edge in toRemove)
            {
                var m = edge.Item2;
                if (_edges.All(e => e.Item2 != m))
                {
                    s.Add(m);
                }
            }
        }
        
        // If there are still edges, then there is a cycle.
        if (_edges.Any())
        {
            throw new Exception("Cycle detected.");
        }
        
        _topologicalOrder = l;
    }
}
