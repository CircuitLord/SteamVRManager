using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager
{
    public static class HandleStartup
    {

        public static void EnableRunAtStartup()
        {

			if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SteamVRManagerStartup.txt"))) {
				File.Move(Path.Combine(Directory.GetCurrentDirectory(), "SteamVRManagerStartup.txt"), Path.Combine(Directory.GetCurrentDirectory(), "SteamVRManagerStartup.lnk"));
			}

            try
            {

				if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "SteamVRManagerStartup.lnk"))) {
					Console.WriteLine("startup exists");
					return;
				}
				
				File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "SteamVRManagerStartup.lnk"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "SteamVRManagerStartup.lnk"), false);
				//Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                //Assembly curAssembly = Assembly.GetExecutingAssembly();
                //key.SetValue("SteamVRManager", curAssembly.Location);
            }
            catch
            {
                Console.WriteLine("Failed to add to run at startup.");
            }
        }

		public static void DisableRunAtStartup() {
			try {

				File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "SteamVRManagerStartup.lnk"));

				//RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

				//key.DeleteValue("SteamVRManager");
			} catch {
				Console.WriteLine("Failed to remove from run at startup.");
			}
		}





	}
}
