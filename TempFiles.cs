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
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"\n[red]Error deleting {file}: {ex.Message}[/]"));
                }
            }

            foreach (DirectoryInfo subdirectory in tempDirectory.GetDirectories())
            {
                try
                {
                    subdirectory.Delete();
                    AnsiConsole.Write(new Markup($"\n[green]Deleted file: {subdirectory.FullName}[/]"));
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"\n[red]Error deleting {subdirectory}: {ex.Message}[/]"));
                }
            }

            AnsiConsole.WriteLine("\nTemporary files have been deleted.");
            Thread.Sleep(1000);
            Program.ShowOptions();
        }

        public static void DeletePrefetchFiles()
        {
            string prefetchFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
            DirectoryInfo prefetchDirectory = new DirectoryInfo(prefetchFolderPath);

            if (!prefetchDirectory.Exists)
            {
                AnsiConsole.MarkupLine("\n[red]Prefetch directory not found.[/]");
                return;
            }

            foreach (FileInfo file in prefetchDirectory.GetFiles())
            {
                try
                {
                    file.Delete();
                    AnsiConsole.MarkupLine($"\n[green]Deleted file: {file.FullName}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"\n[red]Error deleting {file}: {ex.Message}");
                }
            }

            foreach (DirectoryInfo subdirectory in prefetchDirectory.GetDirectories())
            {
                try
                {
                    subdirectory.Delete(true);
                    AnsiConsole.MarkupLine($"\n[green]Deleted folder: {subdirectory.FullName}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"\n[red]Error deleting {subdirectory}: {ex.Message}");
                }
            }

            AnsiConsole.MarkupLine("\nPrefetch files and folders have been deleted.");
            Thread.Sleep(1000);
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
                Thread.Sleep(1000);
                Program.ShowOptions();
            }
            catch (Exception ex)
            {
                AnsiConsole.Write(new Markup($"\n[red]Error deleting event log: {ex.Message}[/]"));
                Thread.Sleep(1000);
                Program.ShowOptions();
            }
        }
    }
}
