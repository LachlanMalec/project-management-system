using TaskCollection = ProjectManagementSystem.Core.TaskCollection;
using TaskEntity = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.FileUtils;

public class TaskSequenceFileWriter
{
    /// <summary>
    /// Writes a task collection to the specified file, overwriting the file if it already exists.
    /// </summary>
    /// <param name="tasks">The ordered task collection to write out to the file.</param>
    /// <param name="filePath">The file to write the task order to.</param>
    public static async Task Write(string filePath, TaskCollection tasks)
    {
        // Get the IDs of the tasks in the order they appear in the collection
        var taskIds = tasks.Select(task => task.Id).ToList();
        // Add a space to the end of every task ID as per the spec
        for(var i = 0; i < taskIds.Count; i++)
        {
            taskIds[i] += " ";
        }
        if (File.Exists(filePath)) File.Delete(filePath);
        await using var writer = new StreamWriter(filePath);
        await writer.WriteLineAsync(string.Join(',', taskIds));
    }
}