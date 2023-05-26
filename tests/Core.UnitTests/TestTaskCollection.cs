using ProjectManagementSystem.Core;
using Task = ProjectManagementSystem.Core.Task;

namespace Core.UnitTests;

public class TestTaskCollection
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void TestCreateTaskCollection()
    {
        var tasks = new List<Task>
        {
            new Task("Task 1", 100, new List<Task>()),
            new Task("Task 2", 200, new List<Task>()),
            new Task("Task 3", 300, new List<Task>())
        };
        var taskCollection = new TaskCollection(tasks);
        Assert.Multiple(() =>
        {
            Assert.That(taskCollection.Tasks, Has.Count.EqualTo(3));
            Assert.That(taskCollection.Tasks[0].Id, Is.EqualTo("Task 1"));
            Assert.That(taskCollection.Tasks[1].Id, Is.EqualTo("Task 2"));
            Assert.That(taskCollection.Tasks[2].Id, Is.EqualTo("Task 3"));
        });
    }
    
    [Test]
    public void TestCreateTaskCollectionWithNoTasks()
    {
        var taskCollection = new TaskCollection(new List<Task>());
        Assert.That(taskCollection.Tasks, Is.Empty);
    }
    
    [Test]
    public void TestAddTaskToCollection()
    {
        var taskCollection = new TaskCollection(new List<Task>());
        taskCollection.Add(new Task("Task 1", 100, new List<Task>()));
        Assert.That(taskCollection.Tasks, Has.Count.EqualTo(1));
        Assert.That(taskCollection.Tasks[0].Id, Is.EqualTo("Task 1"));
    }
    
    [Test]
    public void TestRemoveTaskFromCollection()
    {
        var taskCollection = new TaskCollection(new List<Task>());
        var task = new Task("Task 1", 100, new List<Task>());
        taskCollection.Add(task);
        taskCollection.Remove(task);
        Assert.That(taskCollection.Tasks, Is.Empty);
    }
}