using ProjectManagementSystem.Core;
using Task = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.Queries;

public class GetOptimizedTasksQuery
{
    public TaskCollection Execute(State state)
    {
        return new TaskCollection(state.OptimizedTasks);
    }
}