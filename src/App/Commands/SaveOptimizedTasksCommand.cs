using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to save the optimized tasks to a file.
/// </summary>
public class SaveOptimizedTasksCommand
{
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public async Task Execute(State state)
    {
        await TaskStartTimesFileWriter.Write("EarliestTimes.txt", await state.OptimizedTasks());
    }
}