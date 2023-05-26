using System.Text;

namespace ProjectManagementSystem.Core.DirectedAcyclicGraph;

/// <summary>
/// Represents a Directed Acyclic Graph
/// </summary>
public class Graph<T>
{
    /// <summary>
    /// The Layers of Nodes in the Graph
    /// </summary>
    public List<Layer<T>> Layers { get; private set; }

    /// <summary>
    /// The Edges in the Graph
    /// </summary>
    public List<Edge> Edges { get; private set; }

    /// <summary>
    /// The Nodes in the Graph
    /// </summary>
    public List<Node<T>> Nodes => Layers.SelectMany(l => l.Nodes).ToList();

    public Graph(List<Edge> edges, List<Node<T>> nodes)
    {
        Edges = edges;
        Layers = new List<Layer<T>>();
        ComputeLayers(nodes);
    }

    /// <summary>
    /// Hungrily Places Nodes in Layers of the Graph
    /// </summary>
    /// <param name="remainingNodes"></param>
    private void ComputeLayers(List<Node<T>> remainingNodes)
    {
        // The level of the first layer is 0
        var level = 0;

        // Determine which nodes have already been added to a Layer, so they don't get added again
        var alreadyAdded = Layers.SelectMany(l => l.Nodes).ToList();

        while (remainingNodes.Any())
        {
            var layer = new Layer<T>(level);
            // Add all nodes that have no predecessors in the graph,
            // or whose predecessors have already been added to a Layer,
            // and which have not already been added to a Layer
            foreach (var node in remainingNodes.Where(node =>
                         node.Predecessors(Edges).All(p => alreadyAdded.Contains(new Node<T>(p, default)))))
            {
                if (alreadyAdded.Contains(node)) continue;
                layer.Nodes.Add(node);
                alreadyAdded.Add(node);
            }

            Layers.Add(layer);
            remainingNodes = remainingNodes.Except(alreadyAdded).ToList();
            level++;
        }
    }

    /// <summary>
    /// Topologically Sorts the Nodes in the Graph, and returns them in that order, using Kahn's Algorithm
    /// </summary>
    /// <returns> A List of Nodes in Topological Order </returns>
    public List<Node<T>> TopologicalSort()
    {
        var sortedNodes = new List<Node<T>>();
        var nodesWithNoPredecessors = Nodes.Where(n => Edges.All(e => e.To != n.Id)).ToList();
        while (nodesWithNoPredecessors.Any())
        {
            var node = nodesWithNoPredecessors.First();
            sortedNodes.Add(node);
            nodesWithNoPredecessors.Remove(node);
            var successors = node.Successors(Edges).ToList();
            foreach (var successor in successors)
            {
                Edges.Remove(Edges.First(e => e.From == node.Id && e.To == successor));
                if (Edges.All(e => e.To != successor)) nodesWithNoPredecessors.Add(Nodes.First(n => n.Id == successor));
            }
        }

        return sortedNodes;
    }

    /// <summary>
    /// Returns a string representation of the Graph
    /// </summary>
    /// <returns>A string representation of the Graph</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var layer in Layers)
        {
            sb.Append($"Layer {layer.Level}: ");
            foreach (var node in layer.Nodes) sb.Append($"{node.Id} ");
            sb.Append('\n');
        }

        return sb.ToString();
    }
}