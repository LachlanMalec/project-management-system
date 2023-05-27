using ProjectManagementSystem.App.FileUtils;
using ProjectManagementSystem.Core;
namespace ProjectManagementSystem.App.Commands;

public class SaveOptimizedTasksCommand
{
    public void Execute(State state)
    {
        if (state.OptimizedTasks == null)
        {
            throw new InvalidOperationException("No tasks to save.");
        }
        var taskCollection = new TaskCollection(state.OptimizedTasks);
        TaskFileWriter.Write("EarliestTimes.txt", taskCollection);
    }
}