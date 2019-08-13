using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.IO;
using System.Diagnostics;
using SteamVRManager.Windows;
using AutoUpdaterDotNET;
using System.Collections.Specialized;

namespace SteamVRManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

		AudioConfigWindow audioConfigWindow = new AudioConfigWindow();

		CustomLaunchWindow customLaunchWindow = new CustomLaunchWindow();


		public bool uiEnabled = false;






		public MainWindow() {

			System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
			ni.Icon = new System.Drawing.Icon("Main.ico");
			ni.Visible = true;
			ni.DoubleClick +=
				delegate (object sender, EventArgs args) {
					this.Show();
					this.WindowState = WindowState.Normal;
				};

			AudioManager.GetAudioOutputDevices();

			InitializeComponent();


			if (Properties.Settings.Default.startMinimized) {
				this.WindowState = WindowState.Minimized;
			}


            CheckSteamVR.MonitorSteamVR();

            CheckSteamVR.steamVRChanged += SteamVRChanged;



			LoadUIValues();


			Log("Waiting for SteamVR...");



			AutoUpdater.Start("https://raw.githubusercontent.com/CircuitLord/SteamVRManager/master/version.xml");


			if (Properties.Settings.Default.launchWithWindows) {
				HandleStartup.EnableRunAtStartup();
			}

			if (Properties.Settings.Default.useDashboardFix) {
				SVRSettings.ApplyDashboardFix(true);
			}


			




		}


		protected override void OnStateChanged(EventArgs e) {
			if (WindowState == System.Windows.WindowState.Minimized)
				this.Hide();

			base.OnStateChanged(e);
		}

		public void LoadUIValues() {
			UIRescanHardware.IsChecked = Properties.Settings.Default.rescanOnStart;
			UIDashboardQualityFix.IsChecked = Properties.Settings.Default.useDashboardFix;

			List<int> pollingRates = new List<int>() { 2000, 5000, 7000, 15000 };
			UIPollingRate.ItemsSource = pollingRates;
			UIPollingRate.SelectedIndex = Properties.Settings.Default.svrManagerPollingRateIndex;

			UIRunOnWindowsStart.IsChecked = Properties.Settings.Default.launchWithWindows;

			UIStartMinimized.IsChecked = Properties.Settings.Default.startMinimized;


			uiEnabled = true;
		}


		public void Log(string log) {
			UIStatus.Content = "STATUS: " + log;
		}


        void SteamVRChanged(bool active) {

			

			if (active) {

				Log("SteamVR detected!");

				//Rescan audio devices
				if (Properties.Settings.Default.rescanOnStart)
					Devcon.RescanHardware();

				//Launch custom programs
				StringCollection launch = Properties.Settings.Default.customLaunchPrograms;

				if (launch != null && launch.Count > 0) {
					foreach (string item in launch) {
						Process.Start(item);
					}
				}


				//Set volumes
				if (Properties.Settings.Default.overrideAudioVol) {
					AudioManager.SetVolumeOfDefault(Properties.Settings.Default.volEnterVR);
				}







			} else {
				Log("SteamVR closed... :(");

				//Reapply dashboard fix to keep it at high quality.
				if (Properties.Settings.Default.useDashboardFix)
					SVRSettings.ApplyDashboardFix(true);


				//Set volumes
				if (Properties.Settings.Default.overrideAudioVol) {
					AudioManager.SetVolumeOfDefault(Properties.Settings.Default.volExitVR);
				}
			}

        }



		private void UIRescanHardware_Click(object sender, RoutedEventArgs e) {
			if (!uiEnabled) return;
			Properties.Settings.Default.rescanOnStart = (bool)UIRescanHardware.IsChecked;
			Properties.Settings.Default.Save();
		}

		private void UIDashboardQualityFix_Click(object sender, RoutedEventArgs e) {
			if (!uiEnabled) return;
			Properties.Settings.Default.useDashboardFix = (bool)UIDashboardQualityFix.IsChecked;
			Properties.Settings.Default.Save();

			try {
				SVRSettings.ApplyDashboardFix(Properties.Settings.Default.useDashboardFix);
				Log("Dashboard fix " + Properties.Settings.Default.useDashboardFix.ToString());

			} catch {
				Log("Error applying dashboard fix.");

			}


			
		}


		private void UIPollingRate_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {

			Properties.Settings.Default.svrManagerPollingRateIndex = UIPollingRate.SelectedIndex;

			Properties.Settings.Default.svrMangerPollingRate = Int32.Parse(UIPollingRate.SelectedValue.ToString());
			Console.WriteLine(Int32.Parse(UIPollingRate.SelectedValue.ToString()));
			Properties.Settings.Default.Save();

			Log("Polling rate will apply after restart.");


		}


		private void UIAudioSettings_Click(object sender, RoutedEventArgs e) {

			if (audioConfigWindow.IsLoaded == false) {
				audioConfigWindow = new AudioConfigWindow();
			}

			audioConfigWindow.Show();
			audioConfigWindow.Focus();
		}

		private void UICustomStartupPrograms_Click(object sender, RoutedEventArgs e) {

			if (customLaunchWindow.IsLoaded == false) {
				customLaunchWindow = new CustomLaunchWindow();
			}

			customLaunchWindow.Show();
			customLaunchWindow.Focus();
		}
		private void UIControllerBindings_Click(object sender, RoutedEventArgs e) {
			Process.Start("http://127.0.0.1:8998/dashboard/controllerbinding.html");
		}


		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Application.Current.Shutdown();
		}

		private void UIChangeSVRSettingsLocation_Click(object sender, RoutedEventArgs e) {
			var openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Filter = "VRSettings File|*.vrsettings";
			openFileDialog.ShowDialog();

			if (openFileDialog.FileName != "") {
				Properties.Settings.Default.svrSettingsPath = openFileDialog.FileName;
				Properties.Settings.Default.Save();
				Log("Saved new SVRSettings location.");
			}
		}

		private void UIRunOnWindowsStart_Click(object sender, RoutedEventArgs e) {
			if ((bool)UIRunOnWindowsStart.IsChecked) {
				HandleStartup.EnableRunAtStartup();
				Properties.Settings.Default.launchWithWindows = true;
				Properties.Settings.Default.Save();
				Log("Added to startup programs...");
			} else {
				HandleStartup.DisableRunAtStartup();
				Properties.Settings.Default.launchWithWindows = false;
				Properties.Settings.Default.Save();
				Log("Removed from startup programs...");
			}

		}
		private void UIStartMinimized_Click(object sender, RoutedEventArgs e) {

			if (!uiEnabled) return;

			if ((bool)UIStartMinimized.IsChecked) {
				Properties.Settings.Default.startMinimized = true;
				Properties.Settings.Default.Save();
			} else {
				Properties.Settings.Default.startMinimized = false;
				Properties.Settings.Default.Save();
			}
		}

	}
}
