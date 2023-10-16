using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCleanerPro_spectre
{
    internal class TempFiles
    {
        public static void DeleteTempFiles()
        {
            string tempFolderPath = Path.GetTempPath();
            DirectoryInfo tempDirectory = new DirectoryInfo(tempFolderPath);

            foreach (FileInfo file in tempDirectory.GetFiles())
            {
                try
                {
                    file.Delete();
                    AnsiConsole.Write(new Markup($"\n[green]Deleted file: {file.FullName}[/]"));
                }
                catch (IOException ex)
                {
                    AnsiConsole.Write(new Markup($"\n[yellow]File {file.FullName} is in use and cannot be deleted. Skipping...[/]"));
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"\n[red]Error deleting file {file}: {ex.Message}[/]"));
                }
            }

            foreach (DirectoryInfo subdirectory in tempDirectory.GetDirectories())
            {
                foreach (FileInfo file in subdirectory.GetFiles())
                {
                    try
                    {
                        file.Delete();
                        AnsiConsole.Write(new Markup($"\n[green]Deleted file: {file.FullName}[/]"));
                    }
                    catch (IOException ex)
                    {
                        AnsiConsole.Write(new Markup($"\n[yellow]File {file.FullName} is in use and cannot be deleted. Skipping...[/]"));
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Write(new Markup($"\n[red]Error deleting file {file}: {ex.Message}[/]"));
                    }
                }

                // Delete the empty directory
                try
                {
                    subdirectory.Delete();
                    AnsiConsole.Write(new Markup($"\n[green]Deleted directory: {subdirectory.FullName}[/]"));
                }
                catch (IOException ex)
                {
                    AnsiConsole.Write(new Markup($"\n[yellow]Directory {subdirectory.FullName} is in use and cannot be deleted. Skipping...[/]"));
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"\n[red]Error deleting directory {subdirectory}: {ex.Message}[/]"));
                }
            }

            AnsiConsole.WriteLine("\nTemporary files have been deleted.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Program.ShowOptions();
        }

        public static void DeleteEventLogs()
        {
            try
            {
                EventLog[] eventLogs = EventLog.GetEventLogs();
                foreach (EventLog eventLog in eventLogs)
                {
                    eventLog.Clear();
                }

                AnsiConsole.Write(new Markup("\n[green]All logs have been cleared.[/]"));
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Program.ShowOptions();
            }
            catch (Exception ex)
            {
                AnsiConsole.Write(new Markup($"\n[red]Error deleting event log: {ex.Message}[/]"));
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Program.ShowOptions();
            }
        }
    }
}
