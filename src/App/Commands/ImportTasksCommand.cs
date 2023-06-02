using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to import tasks from a file.
/// </summary>
public class ImportTasksCommand
{
    private readonly string _filePath;
    
    /// <summary>
    /// Creates a command that when executed will import tasks from a file.
    /// </summary>
    /// <param name="filePath">The path to the file to import tasks from.</param>
    public ImportTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public async Task Execute(State state)
    {
        state.Tasks = await TaskFileReader.Read(_filePath);
    }
}