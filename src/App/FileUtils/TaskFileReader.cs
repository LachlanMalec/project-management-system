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
        var taskRecords = File.ReadAllLines(filePath).Select(ReadTaskRecord).ToList();
        
        var taskOrder = new List<string>();

        var tasks = new Dictionary<string, TaskEntity>();
        foreach (var taskRecord in taskRecords)
        {
            var newTask = new TaskEntity(taskRecord.Id, taskRecord.TimeToComplete, new List<TaskEntity>());
            tasks.Add(taskRecord.Id, newTask);
        }
        foreach (var taskRecord in taskRecords)
        {
            var task = tasks[taskRecord.Id];
            foreach (var dependencyId in taskRecord.Dependencies)
            {
                tasks.TryGetValue(dependencyId, out var dependencyTask);
                if (dependencyTask == null) throw new Exception($"Task {taskRecord.Id} depends on {dependencyId} which does not exist.");
                task.Dependencies.Add(dependencyTask);
            }
        }
        var taskCollection = new TaskCollection();
        foreach (var task in tasks.Values) taskCollection.Add(task);
        return Task.FromResult(taskCollection);
    }
}