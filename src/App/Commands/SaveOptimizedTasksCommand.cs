using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

public class SaveOptimizedTasksCommand
{
    public async Task Execute(State state)
    {
        await TaskStartTimesFileWriter.Write("EarliestTimes.txt", await state.OptimizedTasks());
    }
}