using System.Diagnostics;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.Core;

public class TaskOptimizer
{
    private Dictionary<string, TaskEntity> _tasks;
    private HashSet<Tuple<string, string>> _edges;
    
    public TaskOptimizer(TaskCollection tasks)
    {
        _tasks = new Dictionary<string, TaskEntity>();
        foreach (var task in tasks)
        {
            _tasks.Add(task.Id, task);
        }
        _edges = new HashSet<Tuple<string, string>>();
        foreach (var task in tasks)
        {
            foreach (var dependency in task.Dependencies)
            {
                _edges.Add(new Tuple<string, string>(dependency.Id, task.Id));
            }
        }
    }

    public TaskCollection Optimize()
    {
        // List of optimized tasks.
        var l = new List<string>();
        
        Console.WriteLine("Creating set of indegree 0 nodes.");
        var s = new HashSet<string>();
        foreach (var (id, task) in _tasks.Select(x => (x.Key, x.Value)))
        {
            if (task.Dependencies.Count == 0)
            {
                s.Add(id);
            }
        }
        Console.WriteLine("Created set of indegree 0 nodes.");
        
        // While there are nodes with no incoming edges.
        while (s.Any())
        {
            // Remove a node with no incoming edges.
            var n = s.First();
            s.Remove(n);
            
            // Add the node to the optimized task collection.
            l.Add(n);
            
            Console.WriteLine($"Optimizing node {n}.");

            // Remove all outgoing edges from the node.
            var toRemove = new List<Tuple<string, string>>();
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
                if (_edges.All(x => x.Item2 != edge.Item2))
                {
                    s.Add(edge.Item2);
                }
            }
        }
        
        // If there are still edges, then there is a cycle.
        if (_edges.Any())
        {
            throw new Exception("Cycle detected.");
        }

        // Create the optimized task collection.
        var taskCollection = new TaskCollection();
        taskCollection.AddRange(l.Select(optimizedTask => _tasks[optimizedTask]));
        return taskCollection;
    }
}
