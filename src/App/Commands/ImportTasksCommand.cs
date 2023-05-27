using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class ImportTasksCommand
{
    private readonly string _filePath;
    public ImportTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Execute(State state)
    {
        var taskCollection = TaskFileReader.Read(_filePath);
        state.TaskCollection = taskCollection;
    }
}