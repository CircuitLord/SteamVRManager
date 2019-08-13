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

			AudioManager.GetAudioOutputDevices();

			InitializeComponent();

            CheckSteamVR.MonitorSteamVR();

            CheckSteamVR.steamVRChanged += SteamVRChanged;



			LoadUIValues();


			Log("Waiting for SteamVR...");



			AutoUpdater.Start("https://github.com/CircuitLord/SteamVRManager/blob/master/version.xml");




		}


		public void LoadUIValues() {
			UIRescanHardware.IsChecked = Properties.Settings.Default.rescanOnStart;
			UIDashboardQualityFix.IsChecked = Properties.Settings.Default.useDashboardFix;

			List<int> pollingRates = new List<int>() { 2000, 5000, 7000, 15000 };
			UIPollingRate.ItemsSource = pollingRates;
			UIPollingRate.SelectedIndex = Properties.Settings.Default.svrManagerPollingRateIndex;

			UIRunOnWindowsStart.IsChecked = Properties.Settings.Default.launchWithWindows;


			uiEnabled = true;
		}


		public void Log(string log) {
			UIStatus.Content = "STATUS: " + log;
		}


        void SteamVRChanged(bool active) {

			

			if (active) {

				Log("SteamVR detected!");


				Console.WriteLine("Running devcon rescan");
				


			} else {
				Log("SteamVR closed... :(");
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

	}
}
