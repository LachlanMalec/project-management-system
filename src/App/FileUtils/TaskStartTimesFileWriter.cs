using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.FileUtils;

public static class TaskStartTimesFileWriter
{
    /// <summary>
    /// Writes a task collection to the specified file, overwriting the file if it already exists.
    /// </summary>
    /// <param name="taskStartTimes">The task entity and it's start time.</param>
    /// <param name="filePath">The file to write the task collection to.</param>
    public static async Task Write(string filePath, Dictionary<TaskEntity, int> taskStartTimes)
    {
        if (File.Exists(filePath)) File.Delete(filePath);
        await using var writer = new StreamWriter(filePath);
        foreach (var task in taskStartTimes)
        {
            var line = $"{task.Key.Id}, {task.Value}";
            await writer.WriteLineAsync(line);
        }
    }
}