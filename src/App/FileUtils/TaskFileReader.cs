using ProjectManagementSystem.Core;
using TaskEntity = ProjectManagementSystem.Core.Task;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.App.FileUtils;

/// <summary>
/// Reads a CSV file containing tasks and returns a TaskCollection
/// </summary>
public static class TaskFileReader
{
    /// <summary>
    /// Reads a line from a CSV file and returns a TaskRecord
    /// </summary>
    /// <param name="line">The line from the file to parse as a task record.</param>
    /// <returns></returns>
    private static TaskRecord ReadTaskRecord(string line)
    {
        var taskRecord = new TaskRecord();
        var values = line.Split(',');
        for (var i = 0; i < values.Length; i++) values[i] = values[i].Trim();
        taskRecord.Id = values[0];
        taskRecord.TimeToComplete = int.Parse(values[1]);
        for (var i = 2; i < values.Length; i++) taskRecord.Dependencies.Add(values[i]);
        return taskRecord;
    }

    /// <summary>
    /// Reads a CSV file containing tasks and returns a TaskCollection
    /// </summary>
    /// <param name="filePath">The file to read the task collection from.</param>
    /// <returns>A new TaskCollection that was stored in the file.</returns>
    public static Task<TaskCollection> Read(string filePath)
    {
        var taskRecords = File.ReadAllLines(filePath).AsParallel().Select(ReadTaskRecord).ToList();

        var tasks = new SortedList<string, TaskEntity>();
        foreach (var taskRecord in taskRecords)
        {
            var dependencies = new List<TaskEntity>();
            foreach (var dependency in taskRecord.Dependencies)
            {
                var dependencyTask = tasks.TryGetValue(dependency, out var task) ? task : throw new Exception($"Task {dependency} does not exist.");;
                dependencies.Add(dependencyTask);
            }
            var newTask = new TaskEntity(taskRecord.Id, taskRecord.TimeToComplete, dependencies);
            tasks.Add(taskRecord.Id, newTask);
        }
        var taskCollection = new TaskCollection();
        taskCollection.AddRange(tasks.Values);
        return Task.FromResult(taskCollection);
    }
}