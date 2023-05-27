using Task = ProjectManagementSystem.Core.Task;
namespace ProjectManagementSystem.App.Queries;

public class GetOrderedTasksQuery
{
    public List<Task> Execute(State state)
    {
        return state.TaskOptimizer.GetOrderedTasks();
    }
}