using TaskCollection = ProjectManagementSystem.Core.TaskCollection;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.FileUtils;

public static class TaskFileWriter
{
    /// <summary>
    /// Creates a task record to represent a task.
    /// </summary>
    /// <param name="task">The task to create a task record for.</param>
    /// <returns></returns>
    private static TaskRecord CreateTaskRecord(TaskEntity task)
    {
        var taskRecord = new TaskRecord
        {
            Id = task.Id,
            TimeToComplete = task.TimeToComplete
        };
        foreach (var dependency in task.Dependencies) taskRecord.Dependencies.Add(dependency.Id);
        return taskRecord;
    }
    
    /// <summary>
    /// Writes a task collection to the specified file, overwriting the file if it already exists.
    /// </summary>
    /// <param name="tasks">The task collection to write out to the file.</param>
    /// <param name="filePath">The file to write the task collection to.</param>
    public static async Task Write(string filePath, TaskCollection tasks)
    {
        var taskRecords = tasks.AsParallel().Select(CreateTaskRecord).ToList();
        if (File.Exists(filePath)) File.Delete(filePath);
        await using var writer = new StreamWriter(filePath);
        foreach (var taskRecord in taskRecords)
        {
            var line = $"{taskRecord.Id}, {taskRecord.TimeToComplete}";
            foreach (var dependency in taskRecord.Dependencies) line += $", {dependency}";
            await writer.WriteLineAsync(line);
        }
    }
}