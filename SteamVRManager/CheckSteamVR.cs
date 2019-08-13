using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager {


    public delegate void OnSteamVRChanged(bool isEnabled);

    public class CheckSteamVR {

        public static bool steamVRActive = false;

		public static int rateMonitor = Properties.Settings.Default.svrMangerPollingRate;


        public static event OnSteamVRChanged steamVRChanged;


        private static bool previousActive = false;


        public static async Task MonitorSteamVR() {
            while (true) {
                Console.WriteLine("checking active...");
                IsSteamVRRunning();
                await Task.Delay(rateMonitor);

            }
        }


        private static void IsSteamVRRunning() {


            bool active = Process.GetProcessesByName("vrmonitor").Any();

            if (active && !previousActive) {
                Console.WriteLine("SteamVR found!");

                steamVRChanged.Invoke(true);

            } else if (!active && previousActive) {
                Console.WriteLine("SteamVR Closed :(");

                steamVRChanged.Invoke(false);
            }


            previousActive = active;
        }


    }
}