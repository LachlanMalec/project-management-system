namespace ProjectManagementSystem.Core;
using DirectedAcyclicGraph;
/// <summary>
/// A tool for optimizing the order in which tasks are performed.
/// </summary>
public class TaskOptimizer
{
    /// <summary>
    /// A directed acyclic graph of tasks.
    /// </summary>
    private readonly Graph<Task> _taskGraph;

    /// <summary>
    /// Creates a new task optimizer.
    /// </summary>
    /// <param name="taskCollection"></param>
    public TaskOptimizer(TaskCollection taskCollection)
    {
        var taskEdges = new List<Edge>();
        var taskNodes = taskCollection.Tasks.Select((t, i) => new Node<Task>(i, t)).ToList();

        // Create edges between tasks that have a dependency
        for (var i = 0; i < taskNodes.Count; i++)
        {
            var task = taskNodes[i].Value;
            taskEdges.AddRange(task.Dependencies.Select(dependency => taskNodes.FindIndex(n => n.Value == dependency))
                .Select(dependencyIndex => new Edge(dependencyIndex, i)));
        }

        _taskGraph = new Graph<Task>(taskEdges, taskNodes);
    }

    /// <summary>
    /// Returns the tasks in the order in which they should be performed.
    /// </summary>
    /// <returns>A list of tasks in the order in which they should be performed.</returns>
    public List<Task> Optimize()
    {
        var sortedNodes = _taskGraph.TopologicalSort();
        return sortedNodes.Select(n => n.Value).ToList();
    }

    /// <summary>
    /// Gets the layers of tasks from the task graph.
    /// </summary>
    /// <returns>A list of layers of tasks.</returns>
    private List<List<Task>> GetLayers() => _taskGraph.Layers.Select(layer => layer.Nodes.Select(node => node.Value).ToList()).ToList();
    
    /// <summary>
    /// Gets the tasks in an order where no task is dependent on a task that comes after it.
    /// </summary>
    /// <returns>A list of tasks in the order in which they could be performed.</returns>
    public List<Task> GetOrderedTasks()
    {
        var layers = GetLayers();
        var orderedTasks = new List<Task>();
        foreach (var layer in layers)
        {
            foreach (var task in layer)
            {
                if (orderedTasks.Contains(task)) continue;
                orderedTasks.Add(task);
            }
        }
        return orderedTasks;
    }
}