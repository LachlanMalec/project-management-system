namespace ProjectManagementSystem.ConsoleUI;
using Spectre.Console;
public static class Interface
{
    public static void ShowSplash()
    {
        AnsiConsole.Write(new FigletText("Project Management System").LeftJustified().Color(Color.Aqua));
    }
    
    public static string ShowMainMenu()
    {
        var menu = new SelectionPrompt<string>()
            .Title("Main Menu")
            .AddChoices(new[] { "Create Task", "Import Tasks (from file)", "Save Tasks (to file)", "Exit" });
        return menu.Show(AnsiConsole.Console);
    }
    
    public static string ShowCreateTaskIdPrompt()
    {
        var prompt = new TextPrompt<string>("Enter the Id of the task.")
            .PromptStyle("blue");
        return prompt.Show(AnsiConsole.Console);
    }
    
    public static string ShowCreateTaskTimeToCompletePrompt()
    {
        var prompt = new TextPrompt<string>("Enter the time to complete the task.")
            .PromptStyle("blue")
            .ValidationErrorMessage("Invalid time to complete.")
            .Validate(timeToComplete => int.TryParse(timeToComplete, out _) ? ValidationResult.Success() : ValidationResult.Error("Invalid time to complete."));
        return prompt.Show(AnsiConsole.Console);
    }
    
    public static string ShowCreateTaskDependenciesPrompt()
    {
        var prompt = new TextPrompt<string>("Enter the dependencies of the task, separated by commas. Leave blank for no dependencies.")
            .PromptStyle("blue")
            .AllowEmpty();
        return prompt.Show(AnsiConsole.Console);
    }
    
    public static string ShowImportTasksConfirmation()
    {
        var menu = new SelectionPrompt<string>()
            .Title("Are you sure you want to import tasks? This will overwrite any unsaved changes.")
            .AddChoices(new[] { "Continue", "Back" });
        return menu.Show(AnsiConsole.Console);
    }
    
    public static string ShowImportTasksFilePrompt()
    {
        var prompt = new TextPrompt<string>("Enter the path to the file to import tasks from.")
            .PromptStyle("blue")
            .ValidationErrorMessage("File does not exist.")
            .Validate(path => File.Exists(path) ? ValidationResult.Success() : ValidationResult.Error("File does not exist."));
        return prompt.Show(AnsiConsole.Console);
    }
    
    public static string ShowSaveTasksFilePrompt()
    {
        var prompt = new TextPrompt<string>("Enter the path to the file to save tasks to.")
            .PromptStyle("blue")
            .ValidationErrorMessage("Invalid file name.")
            .Validate(path => path.EndsWith(".txt") ? ValidationResult.Success() : ValidationResult.Error("Invalid file name."));
        return prompt.Show(AnsiConsole.Console);
    }
    
    public static string ShowSaveTasksConfirmation()
    {
        var menu = new SelectionPrompt<string>()
            .Title("Are you sure you want to save tasks? This will overwrite the current save file.")
            .AddChoices(new[] { "Continue", "Back" });
        return menu.Show(AnsiConsole.Console);
    }
    
    public static string ShowExitConfirmation()
    {
        var menu = new SelectionPrompt<string>()
            .Title("Are you sure you want to exit? Any unsaved changes will be lost.")
            .AddChoices(new[] { "Exit", "Back" });
        return menu.Show(AnsiConsole.Console);
    }
}