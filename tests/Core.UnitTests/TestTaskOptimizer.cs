using Task = ProjectManagementSystem.Core.Task;
using TaskCollection = ProjectManagementSystem.Core.TaskCollection;
using TaskOptimizer = ProjectManagementSystem.Core.TaskOptimizer;
namespace Core.UnitTests;

public class TestTaskOptimizer
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void TasksWithoutDeps()
    {
        var task = new Task("Test Task", 100, new List<Task>());
        var task2 = new Task("Test Task 2", 100, new List<Task>());
        var task3 = new Task("Test Task 3", 100, new List<Task>());
        
        var taskCollection = new TaskCollection { task, task2, task3 };
        var taskOptimizer = new TaskOptimizer(taskCollection);
        var expectedEarliestStartTimes = new Dictionary<Task, int>
        {
            { task, 0 },
            { task2, 0 },
            { task3, 0 }
        };
        Assert.Multiple(() =>
        {
            Assert.That(taskOptimizer, Is.Not.Null);
            Assert.That(taskOptimizer.Order(), Is.EqualTo(taskCollection));
            Assert.That(taskOptimizer.Optimize(), Is.EqualTo(expectedEarliestStartTimes));
        });
    }

    [Test]
    public void TestWithDeps()
    {
        var task1 = new Task("T1", 100, new List<Task>());
        var task2 = new Task("T2", 30, new List<Task>());
        var task3 = new Task("T3", 50, new List<Task>());
        var task4 = new Task("T4", 90, new List<Task>());
        var task5 = new Task("T5", 70, new List<Task>());
        var task6 = new Task("T6", 55, new List<Task>());
        var task7 = new Task("T7", 50, new List<Task>());
        
        var taskCollection = new TaskCollection { task1, task2, task3, task4, task5, task6, task7 };
        
        task2.Dependencies.Add(task1);
        
        task3.Dependencies.Add(task2);
        task3.Dependencies.Add(task5);
        
        task4.Dependencies.Add(task1);
        task4.Dependencies.Add(task7);
        
        task5.Dependencies.Add(task2);
        task5.Dependencies.Add(task4);
        
        task6.Dependencies.Add(task5);
        
        var taskOptimizer = new TaskOptimizer(taskCollection);
        
        var expectedSequenceOrder = new TaskCollection { task1, task2, task7, task4, task5, task3, task6 };
        
        var expectedEarliestStartTimes = new Dictionary<Task, int>
        {
            { task1, 0 },
            { task2, 100 },
            { task3, 260 },
            { task4, 100 },
            { task5, 190 },
            { task6, 260 },
            { task7, 0 }
        };
        
        Assert.Multiple(() =>
        {
            Assert.That(taskOptimizer, Is.Not.Null);
            Assert.That(taskOptimizer.Order(), Is.EqualTo(expectedSequenceOrder));
            Assert.That(taskOptimizer.Optimize(), Is.EqualTo(expectedEarliestStartTimes));
        });
    }
}