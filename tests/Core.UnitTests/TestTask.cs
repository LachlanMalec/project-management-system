using Task = ProjectManagementSystem.Core.Task;

namespace Core.UnitTests;

public class TestTask
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateTaskWithNoDeps()
    {
        var task = new Task("Test Task", 100, new List<Task>());
        Assert.Multiple(() =>
        {
            Assert.That(task.Id, Is.EqualTo("Test Task"));
            Assert.That(task.TimeToComplete, Is.EqualTo(100));
            Assert.That(task.Dependencies, Is.Empty);
        });
    }

    [Test]
    public void CreateTaskWithDeps()
    {
        var task = new Task("Test Task", 100, new List<Task> { new Task("Dependent Task", 50, new List<Task>()) });
        Assert.Multiple(() =>
        {
            Assert.That(task.Id, Is.EqualTo("Test Task"));
            Assert.That(task.TimeToComplete, Is.EqualTo(100));
            Assert.That(task.Dependencies, Has.Count.EqualTo(1));
        });
    }
    
    [Test]
    public void CompareIdenticalTasks()
    {
        var task1 = new Task("Test Task", 100, new List<Task>());
        var task2 = new Task("Test Task", 100, new List<Task>());
        Assert.That(task1, Is.EqualTo(task2));
    }
    
    [Test]
    public void CompareDifferentTasks()
    {
        var task1 = new Task("Test Task", 100, new List<Task>());
        var task2 = new Task("Test Task 2", 100, new List<Task>());
        Assert.That(task1, Is.Not.EqualTo(task2));
    }
    
    [Test]
    public void CompareIdenticalTasksWithDeps()
    {
        var task1 = new Task("Test Task", 100, new List<Task> { new Task("Dependent Task", 50, new List<Task>()) });
        var task2 = new Task("Test Task", 100, new List<Task> { new Task("Dependent Task", 50, new List<Task>()) });
        Assert.That(task1, Is.EqualTo(task2));
    }
    
    [Test]
    public void CompareDifferentTasksWithDeps()
    {
        var task1 = new Task("Test Task", 100, new List<Task> { new Task("Dependent Task", 50, new List<Task>()) });
        var task2 = new Task("Test Task 2", 100, new List<Task> { new Task("Dependent Task", 50, new List<Task>()) });
        Assert.That(task1, Is.Not.EqualTo(task2));
    }
}