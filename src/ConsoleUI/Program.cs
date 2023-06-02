using ProjectManagementSystem.App;
using ProjectManagementSystem.App.Commands;
using Spectre.Console;

namespace ProjectManagementSystem.ConsoleUI;

public class Program
{
    static async Task Main(string[] args)
    {
        var state = new State();
        string? currentFilePath = null;

        while (true)
        {
            try
            {
                var mainMenuChoice = Interface.ShowMainMenu();
                switch (mainMenuChoice)
                {
                    case "Create Task":
                        var newTaskId = Interface.ShowCreateTaskIdPrompt();
                        if (state.Tasks.FindById(newTaskId) != null)
                        {
                            Interface.ShowError($"Task with id {newTaskId} already exists.");
                            break;
                        }
                        var newTaskTimeToComplete = int.Parse(Interface.ShowCreateTaskTimeToCompletePrompt());
                        var newTaskDependencies = Interface.ShowCreateTaskDependenciesPrompt().Split(",").ToList();
                        newTaskDependencies.RemoveAll(s => s == "");
                        new CreateTaskCommand(newTaskId, newTaskTimeToComplete, newTaskDependencies).Execute(state);
                        break;
                    
                    case "Update Task":
                        var updateTaskId = Interface.ShowUpdateTaskIdPrompt();
                        var updateTask = state.Tasks.FindById(updateTaskId);
                        if (updateTask == null)
                        {
                            Interface.ShowError($"Task with id {updateTaskId} does not exist.");
                            break;
                        }
                        var updateTaskTimeToCompleteConfirmation = Interface.ShowUpdateTaskTimeToCompleteConfirmation();
                        if (updateTaskTimeToCompleteConfirmation == "Continue")
                        {
                            var updateTaskTimeToComplete = int.Parse(Interface.ShowUpdateTaskTimeToCompletePrompt());
                            new UpdateTaskDurationCommand(updateTaskId, updateTaskTimeToComplete).Execute(state);
                        }

                        break;
                    case "Remove Task":
                        var removeTaskId = Interface.ShowRemoveTaskIdPrompt();
                        var removeTask = state.Tasks.FindById(removeTaskId);
                        if (removeTask == null)
                        {
                            Interface.ShowError($"Task with id {removeTaskId} does not exist.");
                            break;
                        }
                        var removeTaskConfirmation = Interface.ShowRemoveTaskConfirmation();
                        if (removeTaskConfirmation == "Continue")
                        {
                            new RemoveTaskCommand(removeTaskId).Execute(state);
                        }
                        
                        break;
                    case "Import Tasks (from file)":
                        switch (Interface.ShowImportTasksConfirmation())
                        {
                            case "Continue":
                                var filePath = Interface.ShowImportTasksFilePrompt();
                                await AnsiConsole.Status()
                                    .Spinner(Spinner.Known.Dots2)
                                    .StartAsync("Importing tasks...", async ctx =>
                                    {
                                        await new ImportTasksCommand(filePath).Execute(state);
                                        currentFilePath = filePath;
                                    });
                                break;
                            case "Back":
                                break;
                        }

                        break;
                    case "Save Tasks (to file)":
                        switch (Interface.ShowSaveTasksConfirmation())
                        {
                            case "Continue":
                                if (currentFilePath == null)
                                {
                                    var filePath = Interface.ShowSaveTasksFilePrompt();
                                    await new SaveTasksCommand(filePath).Execute(state);
                                    currentFilePath = filePath;
                                }
                                else
                                {
                                    await new SaveTasksCommand(currentFilePath).Execute(state);
                                }

                                break;
                            case "Back":
                                break;
                        }

                        break;
                    case "Save Ordered Tasks (to file)":
                        switch (Interface.ShowSaveOrderedTasksConfirmation())
                        {
                            case "Continue":
                                await AnsiConsole.Status()
                                    .Spinner(Spinner.Known.Dots2)
                                    .StartAsync("Saving valid task order...", async ctx =>
                                    {
                                        await new SaveOrderedTasksCommand().Execute(state);
                                    });
                                break;
                            case "Back":
                                break;
                        }

                        break;
                    case "Save Optimized Tasks (to file)":
                        switch (Interface.ShowSaveOptimizedTasksConfirmation())
                        {
                            case "Continue":
                                await new SaveOptimizedTasksCommand().Execute(state);
                                break;
                            case "Back":
                                break;
                        }

                        break;
                    case "Exit":
                        switch (Interface.ShowExitConfirmation())
                        {
                            case "Exit":
                                Environment.Exit(0);
                                break;
                            case "Back":
                                break;
                        }

                        break;
                }
            } catch (Exception e)
            {
                Interface.ShowError(e.Message);
            }
        }
    }
}
