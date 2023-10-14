using Spectre.Console;
using System.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCCleanerPro_spectre
{
    internal class HardDrives
    {
        public static void DetectBrokenHardDrives()
        {
            var table = new Table();
            table.AddColumn("Model");
            table.AddColumn("Serial Number");

            bool detectedBrokenHardDrive = false;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Status='Error'");
            foreach (ManagementObject drive in searcher.Get())
            {
                detectedBrokenHardDrive = true;
                table.AddRow(drive["Model"].ToString(), drive["SerialNumber"].ToString());
            }

            if (detectedBrokenHardDrive)
            {
                AnsiConsole.Write(table);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nEverything is fine.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Program.ShowOptions();
            }
        }
    }
}
