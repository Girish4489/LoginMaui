using LoginMaui.Views.Dashboard;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using static LoginMaui.Views.Auth.SignupPage;

namespace LoginMaui.Views.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		string email = emailEntry.Text;
		string password = passwordEntry.Text;

		// Get user data JSON from local storage
		string userDataJson = await SecureStorage.GetAsync("UserData") ?? "[]";

		// Deserialize JSON to List<User>
		List<Auth.SignupPage.User> userList = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userDataJson)!;

		// Check if user data exists
		if (!userList!.Any(user => (email == user.Email || email == user.Username) && password == user.Password))
		{
			await DisplayAlert("Error", "Invalid email or password", "OK");
		}
		else
		{
			await DisplayAlert("Success", "Login successful", "OK");

			emailEntry.Text = "";
			passwordEntry.Text = "";
			// Access the AppShell and enable the Flyout
			if (Application.Current!.MainPage is AppShell appShell)
			{

				appShell.FlyoutBehavior = FlyoutBehavior.Flyout;

				// Add or update "Logout" MenuFlyoutItem
				var logoutMenuItem = appShell.Items.OfType<MenuFlyoutItem>().FirstOrDefault(item => item.Text == "Logout");
				if (logoutMenuItem == null)
				{
					appShell.Items.Insert(-1, new MenuFlyoutItem
					{
						Text = "Logout",
						Command = new Command(() =>
						{
							// new AppShell(); and push to the appshell
							Application.Current!.MainPage = new AppShell();
						})
					});
				}
				else
				{
					logoutMenuItem.Command = new Command(() => Application.Current!.MainPage = new AppShell());
				}

				// Remove the "Login" FlyoutItem
				var loginFlyoutItem = appShell.Items.OfType<FlyoutItem>().FirstOrDefault(item => item.Title == "Login");
				if (loginFlyoutItem != null)
				{
					appShell.Items.Remove(loginFlyoutItem);
				}

			}

		}
	}
	private async void SignupButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new SignupPage());
	}
}
