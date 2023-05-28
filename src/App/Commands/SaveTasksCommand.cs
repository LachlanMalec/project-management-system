using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class SaveTasksCommand
{
    private readonly string _filePath;
    public SaveTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    public async Task Execute(State state)
    {
        await TaskFileWriter.Write(_filePath, state.Tasks);
    }
}