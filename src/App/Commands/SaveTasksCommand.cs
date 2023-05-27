using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class SaveTasksCommand
{
    private readonly string _filePath;
    public SaveTasksCommand(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Execute(State state)
    {
        if (state.TaskCollection == null)
        {
            throw new InvalidOperationException("No tasks to save.");
        }
        
        TaskFileWriter.Write(_filePath, state.TaskCollection);
    }
}