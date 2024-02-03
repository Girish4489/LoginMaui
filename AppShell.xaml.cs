using System.Diagnostics;

namespace LoginMaui
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent(); 
		}

		private async void OnExitMenuItemClicked(object sender, EventArgs e)
		{
			// Close the app			
			bool answer = await DisplayAlert("Exit", "Do you want to exit from the app or stay?", "Exit", "Cancel");
			if (answer)
			{
				//close the app
				Application.Current!.Quit();
				//System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
			}
		}
	}
}
