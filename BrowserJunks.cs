using Spectre.Console;
using Spectre.Console.Extensions;
using System.Runtime.CompilerServices;

namespace PCCleanerPro_spectre
{
    internal class BrowserJunks
    {
        public static void CleanBrowserData()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[white]Which browser data do you want to delete?[/] [red](WARNING: It will delete data from all installed browsers!)[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[blue]1.[/] Cache");
            AnsiConsole.MarkupLine("[blue]2.[/] Cookies");
            AnsiConsole.MarkupLine("[blue]3.[/] Both");
            AnsiConsole.WriteLine();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Choose an option:[/]")
                    .PageSize(3)
                    .AddChoices(new[]
                    {
                        "Cache",
                        "Cookies",
                        "Both"
                    }
                ));

            switch (choice)
            {
                case "Cache":
                    DeleteCache();
                    break;
                case "Cookies":
                    DeleteCookies();
                    break;
                case "Both":
                    DeleteCache();
                    DeleteCookies();
                    break;
                default:
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[red]Invalid option.[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("Press any key to continue...");
                    break;
            }
        }

        private static void DeleteCache()
        {
            // Code to delete Firefox cache
            string firefoxCacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles";
            if (Directory.Exists(firefoxCacheFolder))
            {
                DirectoryInfo firefoxProfilesFolder = new DirectoryInfo(firefoxCacheFolder);
                foreach (var dir in firefoxProfilesFolder.GetDirectories())
                {
                    DirectoryInfo cacheFolder = new DirectoryInfo(dir.FullName + @"\cache2");
                    if (cacheFolder.Exists)
                    {
                        cacheFolder.Delete(true);
                    }
                    AnsiConsole.MarkupLine("[green]Firefox cache have been deleted[/]");
                }
            }

            // Code to delete Edge cache
            string edgeCacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.MicrosoftEdge_xxxxxx\AC\";
            if (Directory.Exists(edgeCacheFolder))
            {
                DirectoryInfo edgeCacheDataFolder = new DirectoryInfo(edgeCacheFolder);
                DirectoryInfo cacheFolder = new DirectoryInfo(edgeCacheDataFolder.FullName + @"\MicrosoftEdge\Cache\");
                if (cacheFolder.Exists)
                {
                    cacheFolder.Delete(true);
                    AnsiConsole.MarkupLine("[green]Edge cache deleted[/].");
                }
            }

            // Code to delete Chrome cache
            string chromeCacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Cache\";
            if (Directory.Exists(chromeCacheFolder))
            {
                DirectoryInfo cacheFolder = new DirectoryInfo(chromeCacheFolder);
                if (cacheFolder.Exists)
                {
                    foreach (FileInfo file in cacheFolder.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in cacheFolder.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    AnsiConsole.MarkupLine("[green]Chrome cache deleted.[/]");
                }
            }

            // Code to delete Opera cache
            string operaCacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Opera Software\Opera Stable\Cache\";
            if (Directory.Exists(operaCacheFolder))
            {
                DirectoryInfo cacheFolder = new DirectoryInfo(operaCacheFolder);
                if (cacheFolder.Exists)
                {
                    foreach (FileInfo file in cacheFolder.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in cacheFolder.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    AnsiConsole.MarkupLine("[green]Opera cache deleted.[/]");
                }
            }
            Thread.Sleep(1000);
            Program.ShowOptions();
        }

        private static void DeleteCookies()
        {
            // Firefox
            string firefoxCookiesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                        @"\Mozilla\Firefox\Profiles\";
            if (Directory.Exists(firefoxCookiesPath))
            {
                string[] profiles = Directory.GetDirectories(firefoxCookiesPath);
                foreach (string profile in profiles)
                {
                    string cookieFile = profile + "\\cookies.sqlite";
                    if (File.Exists(cookieFile))
                    {
                        File.Delete(cookieFile);
                    }
                }
            }

            // Edge
            string edgeCookiesPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                     @"\Packages\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\AC\MicrosoftEdge\User\Default\Cookies";
            if (File.Exists(edgeCookiesPath))
            {
                File.Delete(edgeCookiesPath);
            }

            // Chrome
            string chromeCookiesPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                       @"\Google\Chrome\User Data\Default\Cookies";
            if (File.Exists(chromeCookiesPath))
            {
                File.Delete(chromeCookiesPath);
            }

            // Opera
            string operaCookiesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      @"\Opera Software\Opera Stable\Cookies";
            if (File.Exists(operaCookiesPath))
            {
                File.Delete(operaCookiesPath);
            }

            Thread.Sleep(1000);
            Program.ShowOptions();
        }
    }
}
