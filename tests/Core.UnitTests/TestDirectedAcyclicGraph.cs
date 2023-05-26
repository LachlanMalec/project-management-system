using ProjectManagementSystem.Core.DirectedAcyclicGraph;

namespace Core.UnitTests;

public class TestDirectedAcyclicGraph
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void TestCreateDirectedAcyclicGraph()
    {
        var nodes = new List<Node<int>>
        {
            new Node<int>(1, 1),
            new Node<int>(2, 2),
            new Node<int>(3, 3),
            new Node<int>(4, 4),
            new Node<int>(5, 5),
        };
        
        var edges = new List<Edge>
        {
            new Edge(1, 2),
            new Edge(1, 3),
            new Edge(2, 4),
            new Edge(3, 4),
            new Edge(4, 5),
        };
        
        var graph = new Graph<int>(edges, nodes);

        Assert.Multiple(() =>
        {
            Assert.That(graph.Nodes, Has.Count.EqualTo(5));
            Assert.That(graph.Edges, Has.Count.EqualTo(5));
            Assert.That(graph.Nodes[0].Id, Is.EqualTo(1));
            Assert.That(graph.Nodes[1].Id, Is.EqualTo(2));
            Assert.That(graph.Nodes[2].Id, Is.EqualTo(3));
            Assert.That(graph.Nodes[3].Id, Is.EqualTo(4));
            Assert.That(graph.Nodes[4].Id, Is.EqualTo(5));
            Assert.That(graph.Edges[0].From, Is.EqualTo(1));
            Assert.That(graph.Edges[0].To, Is.EqualTo(2));
            Assert.That(graph.Edges[1].From, Is.EqualTo(1));
            Assert.That(graph.Edges[1].To, Is.EqualTo(3));
            Assert.That(graph.Edges[2].From, Is.EqualTo(2));
            Assert.That(graph.Edges[2].To, Is.EqualTo(4));
            Assert.That(graph.Edges[3].From, Is.EqualTo(3));
            Assert.That(graph.Edges[3].To, Is.EqualTo(4));
            Assert.That(graph.Edges[4].From, Is.EqualTo(4));
            Assert.That(graph.Edges[4].To, Is.EqualTo(5));
        });
    }
}