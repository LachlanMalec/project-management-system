using Task = ProjectManagementSystem.Core.Task;
using ProjectManagementSystem.Core;
namespace Core.UnitTests;

public class TestTaskOptimizer
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestTasksWithNoDependencies()
    {
        var tasks = new List<Task>
        {
            new Task("Task 1", 100, new List<Task>()),
            new Task("Task 2", 200, new List<Task>()),
            new Task("Task 3", 300, new List<Task>())
        };
        
        var taskCollection = new TaskCollection(tasks);
        
        var taskOptimizer = new TaskOptimizer(taskCollection);
        
        var optimizedTasks = taskOptimizer.Optimize();
        
        Assert.Multiple(() =>
        {
            Assert.That(optimizedTasks, Has.Count.EqualTo(3));
            Assert.That(optimizedTasks[0].Id, Is.EqualTo("Task 1"));
            Assert.That(optimizedTasks[1].Id, Is.EqualTo("Task 2"));
            Assert.That(optimizedTasks[2].Id, Is.EqualTo("Task 3"));
        });
    }
    
    [Test]
    public void TestTasksWithSimpleDependencies()
    {
        var tasks = new List<Task>
        {
            new Task("Task 1", 100, new List<Task>()),
            new Task("Task 2", 200, new List<Task>()),
            new Task("Task 3", 300, new List<Task>())
        };
        
        tasks[1].Dependencies.Add(tasks[2]);
        tasks[2].Dependencies.Add(tasks[0]);
        
        var taskCollection = new TaskCollection(tasks);
        
        var taskOptimizer = new TaskOptimizer(taskCollection);
        
        var optimizedTasks = taskOptimizer.Optimize();
        
        Assert.Multiple(() =>
        {
            Assert.That(optimizedTasks, Has.Count.EqualTo(3));
            Assert.That(optimizedTasks[0].Id, Is.EqualTo("Task 1"));
            Assert.That(optimizedTasks[1].Id, Is.EqualTo("Task 3"));
            Assert.That(optimizedTasks[2].Id, Is.EqualTo("Task 2"));
        });
    }

    [Test]
    public void TestTasksWithComplexDependencies()
    {
        var tasks = new List<Task>
        {
            new Task("Task 1", 100, new List<Task>()),
            new Task("Task 2", 200, new List<Task>()),
            new Task("Task 3", 300, new List<Task>()),
            new Task("Task 4", 400, new List<Task>()),
            new Task("Task 5", 500, new List<Task>()),
            new Task("Task 6", 600, new List<Task>()),
        };

        tasks[1].Dependencies.Add(tasks[2]);
        tasks[2].Dependencies.Add(tasks[0]);
        tasks[3].Dependencies.Add(tasks[4]);
        tasks[4].Dependencies.Add(tasks[2]);
        tasks[4].Dependencies.Add(tasks[5]);
        tasks[5].Dependencies.Add(tasks[0]);

        var taskCollection = new TaskCollection(tasks);

        var taskOptimizer = new TaskOptimizer(taskCollection);
        
        var optimizedTasks = taskOptimizer.Optimize();
        
        Assert.Multiple(() =>
        {
            Assert.That(optimizedTasks, Has.Count.EqualTo(6));
            Assert.That(optimizedTasks[0].Id, Is.EqualTo("Task 1"));
            Assert.That(optimizedTasks[1].Id, Is.EqualTo("Task 3"));
            Assert.That(optimizedTasks[2].Id, Is.EqualTo("Task 6"));
            Assert.That(optimizedTasks[3].Id, Is.EqualTo("Task 2"));
            Assert.That(optimizedTasks[4].Id, Is.EqualTo("Task 5"));
            Assert.That(optimizedTasks[5].Id, Is.EqualTo("Task 4"));
        });
    }
}