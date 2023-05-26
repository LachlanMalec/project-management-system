namespace ProjectManagementSystem.Core.DirectedAcyclicGraph;

/// <summary>
/// Represents a Layer in a Directed Acyclic Graph
/// </summary>
public class Layer<T>
{
    /// <summary>
    /// The Level of the Layer in the Graph
    /// </summary>
    public int Level { get; }
    
    /// <summary>
    /// The Nodes in the Layer
    /// </summary>
    public List<Node<T>> Nodes { get; }
    
    public Layer(int level)
    {
        Level = level;
        Nodes = new List<Node<T>>();
    }
}
