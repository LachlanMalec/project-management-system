using ProjectManagementSystem.App;
using ProjectManagementSystem.App.Commands;
namespace ProjectManagementSystem.ConsoleUI;

public class Program
{
    public static void Main(string[] args)
    {
        var state = new State();
        string? currentFilePath = null;
        Interface.ShowSplash();

        while (true)
        {
            var mainMenuChoice = Interface.ShowMainMenu();
            switch (mainMenuChoice)
            {
                case "Create Task":
                    var newTaskId = Interface.ShowCreateTaskIdPrompt();
                    var newTaskTimeToComplete = int.Parse(Interface.ShowCreateTaskTimeToCompletePrompt());
                    var newTaskDependencies = Interface.ShowCreateTaskDependenciesPrompt().Split(",").ToList();
                    newTaskDependencies.RemoveAll(s => s == "");
                    new CreateTaskCommand(newTaskId, newTaskTimeToComplete, newTaskDependencies).Execute(state);
                    break;
                case "Import Tasks (from file)":
                    switch (Interface.ShowImportTasksConfirmation())
                    {
                        case "Continue":
                            var filePath = Interface.ShowImportTasksFilePrompt();
                            new ImportTasksCommand(filePath).Execute(state);
                            currentFilePath = filePath;
                            break;
                        case "Back":
                            break;
                    }
                    break;
                case "Save Tasks (to file)":
                    switch(Interface.ShowSaveTasksConfirmation())
                    {
                        case "Continue":
                            if (currentFilePath == null)
                            {
                                var filePath = Interface.ShowSaveTasksFilePrompt();
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
        }
    }
}
