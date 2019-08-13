using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue("SteamVRManager", curAssembly.Location);
            }
            catch
            {
                Console.WriteLine("Failed to add to run at startup.");
            }
        }

		public static void DisableRunAtStartup() {
			try {

				RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

				key.DeleteValue("SteamVRManager");
			} catch {
				Console.WriteLine("Failed to remove from run at startup.");
			}
		}





	}
}
