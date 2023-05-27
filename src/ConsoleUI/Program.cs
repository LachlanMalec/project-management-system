using ProjectManagementSystem.App;
using ProjectManagementSystem.App.Commands;
namespace ProjectManagementSystem.ConsoleUI;

public class Program
{
    public static void Main(string[] args)
    {
        var state = new State();
        string? currentFilePath = null;
        var view = new Interface();
        view.ShowSplash();

        while (true)
        {
            var mainMenuChoice = view.ShowMainMenu();
            switch (mainMenuChoice)
            {
                case "Create Task":
                    var newTaskId = view.ShowCreateTaskIdPrompt();
                    var newTaskTimeToComplete = int.Parse(view.ShowCreateTaskTimeToCompletePrompt());
                    var newTaskDependencies = view.ShowCreateTaskDependenciesPrompt().Split(",").ToList();
                    newTaskDependencies.RemoveAll(s => s == "");
                    new CreateTaskCommand(newTaskId, newTaskTimeToComplete, newTaskDependencies).Execute(state);
                    break;
                case "Import Tasks (from file)":
                    switch (view.ShowImportTasksConfirmation())
                    {
                        case "Continue":
                            var filePath = view.ShowImportTasksFilePrompt();
                            new ImportTasksCommand(filePath).Execute(state);
                            currentFilePath = filePath;
                            break;
                        case "Back":
                            break;
                    }
                    break;
                case "Save Tasks (to file)":
                    switch(view.ShowSaveTasksConfirmation())
                    {
                        case "Continue":
                            if (currentFilePath == null)
                            {
                                var filePath = view.ShowSaveTasksFilePrompt();
                                new SaveTasksCommand(filePath).Execute(state);
                                currentFilePath = filePath;
                            }
                            else
                            {
                                new SaveTasksCommand(currentFilePath).Execute(state);
                            }
                            break;
                        case "Back":
                            break;
                    }
                    break;
                case "Exit":
                    switch (view.ShowExitConfirmation())
                    {
                        case "Exit":
                            Environment.Exit(0);
                            break;
                        case "Back":
                            break;
                    }
                    break;
            }
        }
    }
}
