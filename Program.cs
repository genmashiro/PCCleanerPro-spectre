using System;
using System.Runtime.CompilerServices;
using Octokit;
using System.Security.Principal;
using PCCleanerPro_spectre;
using Spectre.Console;
using System.Diagnostics;

namespace PCCleanerPro_spectre
{
    internal class Program
    {
        public static void CheckForUpdates()
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("PCCleanerPro"));
            string repoUrl = "https://github.com/genmashiro/PCCleanerPro-spectre";
            var owner = "genmashiro";
            var repoName = "PCCleanerPro-spectre";
            Release latestRelease = null;

            try
            {
                latestRelease = githubClient.Repository.Release.GetLatest(owner, repoName).Result;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine("Error checking for updates: " + ex.Message);
                return;
            }

            if (latestRelease != null)
            {
                Version currentVersion = new Version("0.0.4"); // Replace with your program's current version

                Version latestVersion;
                if (Version.TryParse(latestRelease.TagName, out latestVersion))
                {
                    if (latestVersion > currentVersion)
                    {
                        AnsiConsole.MarkupLine($"[yellow]A newer version ({latestRelease.TagName}) is available.[/]");
                        OpenRepositoryLink(repoUrl);
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[green]You are using the latest version.[/]");
                        Console.WriteLine();
                        AnsiConsole.MarkupLine("[bold]Please wait 3 sec...[/]");
                        Thread.Sleep(3000);
                        ShowOptions();
                    }
                }
                else
                {
                    AnsiConsole.WriteLine("Unable to parse the version from the latest release tag.");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                AnsiConsole.WriteLine("Unable to check for updates at this time.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public static void OpenRepositoryLink(string repositoryUrl)
        {
            // Replace "repositoryUrl" with the actual URL of your repository
            string url = repositoryUrl;

            // Use the default system browser to open the URL
            Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }

        static void Main(string[] args)
        {
            // Disable for better debugging
            if (!IsRunAdmin()) 
            {
                AnsiConsole.WriteLine("Please run the program as an administrator");
                return;
            }
            CheckForUpdates();
        }

        public static void ShowOptions()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("PCCleanerPro")
                    .LeftJustified()
                    .Color(Color.Red));

            AnsiConsole.WriteLine($"\nWelcome to PCCleanerPro v0.0.4, {Environment.UserName}");

            var framework = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSelect [green]option[/] you want to use. (use arrow keys to navigate)")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Clean temporary files",
                        "2. Clean event log files",
                        "3. Clean startup",
                        "4. Detect broken hard drives",
                        "5. Clean browser data",
                        "6. Exit"
                }));


            switch (framework)
            {
                case "1. Clean temporary files":
                    TempFiles.DeleteTempFiles();
                    break;
                case "2. Clean event log files":
                    TempFiles.DeleteEventLogs();
                    break;
                case "3. Clean startup":
                    Autostart.CleanStartup();
                    break;
                case "4. Detect broken hard drives":
                    HardDrives.DetectBrokenHardDrives();
                    break;
                case "5. Clean browser data":
                    BrowserJunks.CleanBrowserData();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        private static bool IsRunAdmin()
        {
            try
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}