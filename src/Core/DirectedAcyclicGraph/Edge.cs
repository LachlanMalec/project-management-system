namespace ProjectManagementSystem.Core.DirectedAcyclicGraph;

/// <summary>
/// Represents an Edge in a Directed Acyclic Graph
/// </summary>
public class Edge
{
    /// <summary>
    /// The Id of the Node that the Edge points from.
    /// </summary>
    public int From { get; }
    
    /// <summary>
    /// The Id of the Node that the Edge points to.
    /// </summary>
    public int To { get; }
    
    /// <summary>
    /// Create a new edge between the specified nodes.
    /// </summary>
    /// <param name="from">The Id of the Node that the Edge points from.</param>
    /// <param name="to">The Id of the Node that the Edge points to.</param>
    public Edge(int from, int to)
    {
        From = from;
        To = to;
    }
}