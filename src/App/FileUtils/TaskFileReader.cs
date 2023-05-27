using ProjectManagementSystem.Core;
using Task = ProjectManagementSystem.Core.Task;

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
    public static TaskCollection Read(string filePath)
    {
        var taskRecords = new List<TaskRecord>();
        using var reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var taskRecord = ReadTaskRecord(line);
            taskRecords.Add(taskRecord);
        }

        var tasks = new List<Task>();
        foreach (var taskRecord in taskRecords)
        {
            var dependencies = new List<Task>();
            foreach (var dependency in taskRecord.Dependencies)
            {
                var task = tasks.Find(t => t.Id == dependency);
                if (task == null) throw new InvalidOperationException($"No task with ID {dependency} exists.");
                dependencies.Add(task);
            }

            var newTask = new Task(taskRecord.Id, taskRecord.TimeToComplete, dependencies);
            tasks.Add(newTask);
        }
        return new TaskCollection(tasks);
    }
}