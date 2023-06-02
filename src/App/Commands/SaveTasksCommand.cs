using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to save the tasks to a file.
/// </summary>
public class SaveTasksCommand
{
    private readonly string _filePath;
    
    /// <summary>
    /// Creates a command that when executed will save the tasks to a file.
    /// </summary>
    /// <param name="filePath">The path to the file to save the tasks to.</param>
    public SaveTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public async Task Execute(State state)
    {
        await TaskFileWriter.Write(_filePath, state.Tasks);
    }
}