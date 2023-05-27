using ProjectManagementSystem.App.FileUtils;
using ProjectManagementSystem.Core;
namespace ProjectManagementSystem.App.Commands;

public class SaveOrderedTasksCommand
{
    public void Execute(State state)
    {
        if (state.TaskCollection == null)
        {
            throw new InvalidOperationException("No tasks to save.");
        }
        var taskCollection = new TaskCollection(state.TaskOptimizer.GetOrderedTasks());
        TaskFileWriter.Write("Sequence.txt", taskCollection);
    }
}