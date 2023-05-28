using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class ImportTasksCommand
{
    private readonly string _filePath;
    public ImportTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    public async Task Execute(State state)
    {
        state.Tasks = await TaskFileReader.Read(_filePath);
    }
}