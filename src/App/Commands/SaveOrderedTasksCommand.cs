using ProjectManagementSystem.App.FileUtils;
namespace ProjectManagementSystem.App.Commands;

/// <summary>
/// A command to import tasks from a file.
/// </summary>
public class SaveOrderedTasksCommand
{
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="state">The state to execute the command on.</param>
    public async Task Execute(State state)
    {
        await TaskSequenceFileWriter.Write("Sequence.txt", await state.OrderedTasks());
    }
}