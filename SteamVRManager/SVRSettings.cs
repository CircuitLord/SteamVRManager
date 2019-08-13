using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamVRManager {
	public static class SVRSettings {

		
		

		private static dynamic Load() {
			return JObject.Parse(File.ReadAllText(Properties.Settings.Default.svrSettingsPath));
		}

		private static void Save(dynamic d) {
			File.WriteAllText(Properties.Settings.Default.svrSettingsPath, JsonConvert.SerializeObject(d, Formatting.Indented));
		}


		public static void ApplyDashboardFix(bool enable) {



			dynamic d = Load();

			if (enable) {
				d.GpuSpeed.gpuSpeed0 = 15000;
				d.GpuSpeed.gpuSpeed1 = 15000;
				d.GpuSpeed.gpuSpeed2 = 15000;
				d.GpuSpeed.gpuSpeed3 = 15000;
				d.GpuSpeed.gpuSpeed4 = 15000;
				d.GpuSpeed.gpuSpeed5 = 15000;
				d.GpuSpeed.gpuSpeed6 = 15000;
				d.GpuSpeed.gpuSpeed7 = 15000;
				d.GpuSpeed.gpuSpeed8 = 15000;
				d.GpuSpeed.gpuSpeed9 = 15000;
				d.GpuSpeed.gpuSpeedHorsepower = 15000;
				d.GpuSpeed.gpuSpeedCount = 10;

			} else {
				d.GpuSpeed.gpuSpeedCount = 0;
			}

			Save(d);
		}

		public static void SetSVRRefreshRate(int rate) {
			dynamic d = Load();
			d.steamvr.preferredRefreshRate = rate;
			Save(d);
		}





	}


}
