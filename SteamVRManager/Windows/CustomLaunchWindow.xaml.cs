using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;

namespace SteamVRManager.Windows {
	/// <summary>
	/// Interaction logic for CustomLaunchWindow.xaml
	/// </summary>
	public partial class CustomLaunchWindow : Window {




		public CustomLaunchWindow() {


			InitializeComponent();



			InitUI();
		}




		private void InitUI() {

			if (Properties.Settings.Default.customLaunchPrograms == null) {
				Properties.Settings.Default.customLaunchPrograms = new System.Collections.Specialized.StringCollection();
			}

			UIProgramList.ItemsSource = Properties.Settings.Default.customLaunchPrograms;
		}

		private void UINew_Click(object sender, RoutedEventArgs e) {
			var openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Title = "Select a file to launch";
			; openFileDialog.ShowDialog();

			if (openFileDialog.FileName == null) return;

			Properties.Settings.Default.customLaunchPrograms.Add(openFileDialog.FileName);
			Properties.Settings.Default.Save();

			UIProgramList.ItemsSource = null;
			UIProgramList.ItemsSource = Properties.Settings.Default.customLaunchPrograms;




		}

		private void UIRemove_Click(object sender, RoutedEventArgs e) {

			//foreach (var item in UIProgramList.SelectedItems) {
			if (UIProgramList.SelectedIndex == -1) return;
			Properties.Settings.Default.customLaunchPrograms.RemoveAt(UIProgramList.SelectedIndex);
			Properties.Settings.Default.Save();

			UIProgramList.ItemsSource = null;
			UIProgramList.ItemsSource = Properties.Settings.Default.customLaunchPrograms;
			//}

		}
	}
}
