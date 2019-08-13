using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager {
	public static class AudioManager {

		public static List<string> devices = new List<string>();


		public static void GetAudioOutputDevices() {

			for (int n = -1; n < WaveOut.DeviceCount; n++) {
				var caps = WaveOut.GetCapabilities(n);
				devices.Add(caps.ProductName);
				
			}

		}


		public static void SetVolumeOfDefault(int vol) {
			CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
			//Console.WriteLine("Current Volume:" + defaultPlaybackDevice.Volume);
			defaultPlaybackDevice.Volume = vol;
		}



	}
}
