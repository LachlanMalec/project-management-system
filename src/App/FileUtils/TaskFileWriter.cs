using ProjectManagementSystem.Core;
using Task = ProjectManagementSystem.Core.Task;

namespace ProjectManagementSystem.App.FileUtils;

public static class TaskFileWriter
{
    /// <summary>
    /// Creates a task record to represent a task.
    /// </summary>
    /// <param name="task">The task to create a task record for.</param>
    /// <returns></returns>
    private static TaskRecord CreateTaskRecord(Task task)
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
    /// <param name="taskCollection">The task collection to write out to the file.</param>
    /// <param name="filePath">The file to write the task collection to.</param>
    public static void Write(string filePath, TaskCollection taskCollection)
    {
        var taskRecords = taskCollection.Tasks.Select(CreateTaskRecord).ToList();
        if (File.Exists(filePath)) File.Delete(filePath);
        using var writer = new StreamWriter(filePath);
        foreach (var line in from taskRecord in taskRecords
                 let line = $"{taskRecord.Id},{taskRecord.TimeToComplete}"
                 select taskRecord.Dependencies.Aggregate(line, (current, dependency) => current + $",{dependency}"))
            writer.WriteLine(line);
    }
}