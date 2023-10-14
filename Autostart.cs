using Microsoft.Win32;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static PCCleanerPro_spectre.Autostart;

namespace PCCleanerPro_spectre
{
    internal class Autostart
    {
        public class StartupProgram
        {
            public string Name { get; set; }
            public string Path { get; set; }

            public StartupProgram(string name, string path)
            {
                Name = name;
                Path = path;
            }
        }

        public static void CleanStartup()
        {
            // Get the registry key for the current user's startup programs
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // Get a list of all the startup programs
            var startupPrograms = new List<StartupProgram>();
            foreach (string program in regKey.GetValueNames())
            {
                var name = program;
                var path = regKey.GetValue(program) as string;
                startupPrograms.Add(new StartupProgram(name, path));
            }

            // Display the startup programs in a table
            var table = new Table()
                .AddColumn("Name")
                .AddColumn("Path");

            foreach (var startupProgram in startupPrograms)
            {
                table.AddRow(startupProgram.Name, startupProgram.Path);
            }

            string titleMarkup = "\n[green]Active Startup Programs:[/]\n[grey]------------------------[/]\n";
            AnsiConsole.Markup(titleMarkup);

            AnsiConsole.Write(table);

            var selectionPrompt = new TextPrompt<string>("Select a startup program to delete: ");
            var selectedProgram = AnsiConsole.Prompt(selectionPrompt);

            switch (selectedProgram)
            {
                case "Delete":
                    // Delete the startup program from the registry
                    regKey.DeleteValue(selectedProgram);
                    Console.WriteLine($"{selectedProgram} has been deleted from the startup programs.");
                    break;
                default:
                    // Invalid selection
                    Console.WriteLine($"Invalid selection: {selectedProgram}");
                    break;
            }

            // Close the registry key
            regKey.Close();
        }
    }
}
