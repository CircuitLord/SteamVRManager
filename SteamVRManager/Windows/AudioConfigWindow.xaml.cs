using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SteamVRManager {
	/// <summary>
	/// Interaction logic for AudioConfigWindow.xaml
	/// </summary>
	public partial class AudioConfigWindow : Window {

		public static bool overrideVol = false;


		public AudioConfigWindow() {



			InitializeComponent();

			UIOverrideVol.IsChecked = Properties.Settings.Default.overrideAudioVol;
			UpdateVolEnabled();

			UIEnterVRVol.Value = Properties.Settings.Default.volEnterVR;
			UIExitVRVol.Value = Properties.Settings.Default.volExitVR;



		}

		private void UIOverrideVol_Checked(object sender, RoutedEventArgs e) {

			UpdateVolEnabled();

		}
		private void UIOverrideVol_Unchecked(object sender, RoutedEventArgs e) {
			UpdateVolEnabled();
		}

		private void UpdateVolEnabled() {

			if (UIOverrideVol.IsChecked == true) {
				UIEnterVRVol.IsEnabled = true;
				UIExitVRVol.IsEnabled = true;
				UILabel1.IsEnabled = true;
				UILabel2.IsEnabled = true;
				Properties.Settings.Default.overrideAudioVol = true;
				Properties.Settings.Default.Save();
			} else {
				UIEnterVRVol.IsEnabled = false;
				UIExitVRVol.IsEnabled = false;
				UILabel1.IsEnabled = false;
				UILabel2.IsEnabled = false;
				Properties.Settings.Default.overrideAudioVol = false;
				Properties.Settings.Default.Save();
			}

		}


		private void UISave_Click(object sender, RoutedEventArgs e) {
			Properties.Settings.Default.volEnterVR = (int)Math.Round(UIEnterVRVol.Value);
			Properties.Settings.Default.volExitVR = (int)Math.Round(UIExitVRVol.Value);
			Properties.Settings.Default.Save();
		}
	}
}
