using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager
{
    public class CheckSteamVR
    {

        public static bool steamVRActive = false;

        public static int rateMonitor = 7000;

        
        



        public static async Task MonitorSteamVR()
        {
            while (true)
            {
                IsSteamVRRunning();
                await Task.Delay(rateMonitor);
            }
        }


        private static void IsSteamVRRunning()
        {
            if (true)
            {

                OnSteamVREnabled();
            }

        }


        private static void OnSteamVREnabled()
        {

        }


    }
}
