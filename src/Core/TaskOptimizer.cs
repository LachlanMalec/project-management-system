using System.Diagnostics;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.Core;

public class TaskOptimizer
{
    private TaskCollection _tasks;
    private HashSet<Tuple<TaskEntity, TaskEntity>> _edges;
    
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
        Console.WriteLine($"{_tasks.Count} tasks.");
        Console.WriteLine($"{_edges.Count} edges.");
    }

    public TaskCollection Optimize()
    {
        // List of optimized tasks.
        var l = new TaskCollection();
        
        Console.WriteLine("Optimizing tasks.");
        // Set of tasks with no incoming edges.
        var s = new HashSet<TaskEntity>();
        foreach (var task in _tasks)
        {
            if (task.Dependencies.Count == 0)
            {
                s.Add(task);
            }
        }
        Console.WriteLine($"Found {s.Count} tasks with no dependencies.");

        // While there are nodes with no incoming edges.
        while (s.Any())
        {
            // Remove a node with no incoming edges.
            var n = s.First();
            s.Remove(n);
            
            // Add the node to the optimized task collection.
            l.Add(n);
            
            Console.WriteLine($"Optimizing node {n.Id}.");

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
        
        return l;
    }
}
