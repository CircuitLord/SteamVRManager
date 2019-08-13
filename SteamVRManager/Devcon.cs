using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager {
	public class Devcon {


		string devconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "devcon.exe");

		static Process devconProcess = new Process();

		public Devcon() {
			devconProcess.StartInfo.FileName = devconPath;
			devconProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		}


		public static void RescanHardware() {
			devconProcess.StartInfo.Arguments = "rescan";
			devconProcess.Start();
		}

		public void SetDefaultAudioDevice(string deviceName) {
			devconProcess.StartInfo.Arguments = "setdefaultsounddevice \"" + deviceName + "\" 2";
			devconProcess.Start();
		}

	}
}
