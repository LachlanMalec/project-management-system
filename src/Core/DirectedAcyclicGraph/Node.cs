namespace ProjectManagementSystem.Core.DirectedAcyclicGraph;

/// <summary>
/// Represents a Node in a Directed Acyclic Graph
/// </summary>
public class Node<T> : IEquatable<Node<T>>
{
    /// <summary>
    /// The Id of the Node
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// The Value of the Node
    /// </summary>
    public T Value { get; }

    public Node(int id, T data)
    {
        Id = id;
        Value = data;
    }
    
    /// <summary>
    /// Get the Node Ids of the Predecessors of this Node (the nodes that point to this node)
    /// </summary>
    /// <param name="edges">The edges present in the graph, described by Node Ids</param>
    /// <returns></returns>
    public IEnumerable<int> Predecessors(IEnumerable<Edge> edges)
    {
        return edges.Where(e => e.To == Id).Select(e => e.From);
    }
    
    /// <summary>
    /// Get the Node Ids of the Successors of this Node (the nodes that this node points to)
    /// </summary>
    /// <param name="edges">The edges present in the graph, described by Node Ids</param>
    /// <returns></returns>
    public IEnumerable<int> Successors(IEnumerable<Edge> edges)
    {
        return edges.Where(e => e.From == Id).Select(e => e.To);
    }
    
    /// <summary>
    /// Gets the Level of this Node in the Graph
    /// </summary>
    /// <param name="layers">The layers of nodes present in the graph, described by lists of Nodes</param>
    /// <returns></returns>
    public int Level(IEnumerable<Layer<T>> layers)
    {
        return layers.First(l => l.Nodes.Contains(this)).Level;
    }
    
    /// <summary>
    /// Gets the Position of this Node in the Layer
    /// </summary>
    /// <param name="layers">The layers of nodes present in the graph, described by lists of Nodes</param>
    /// <returns></returns>
    public int Position(IEnumerable<Layer<T>> layers)
    {
        return layers.First(l => l.Nodes.Contains(this)).Nodes.IndexOf(this);
    }
    
    /// <summary>
    /// Determines if the provided Node is equal to this Node
    /// </summary>
    /// <param name="other">The Node to compare to this Node</param>
    /// <returns>Boolean indicating if the provided Node is equal to this Node</returns>
    public bool Equals(Node<T> other)
    {
        return Id == other.Id;
    }
}