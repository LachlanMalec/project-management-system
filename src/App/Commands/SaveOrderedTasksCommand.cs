using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class SaveOrderedTasksCommand
{
    public async Task Execute(State state)
    {
        await TaskFileWriter.Write("Sequence.txt", await state.OrderedTasks());
    }
}