using LoginMaui.Views.Auth;
using Microsoft.Maui.Controls.StyleSheets;

namespace LoginMaui.Views.Dashboard;

public partial class UserDetails : ContentPage
{
	public UserDetails()
	{
		InitializeComponent();
		LoadUserData();
	}
	private async void LoadUserData()
	{
		try
		{
			// Retrieve user data from Secure Storage
			string userDataJson = await SecureStorage.GetAsync("UserData") ?? "[]";

			// Deserialize JSON to List<User>
			List<SignupPage.User> userList = System.Text.Json.JsonSerializer.Deserialize<List<SignupPage.User>>(userDataJson)!;

			// Set the List<User> as the ItemsSource for the ListView
			usersListView.ItemsSource = userList;
		}
		catch (Exception ex)
		{
			// Handle the exception, e.g., log it or display an error message
			Console.WriteLine($"Error loading user data: {ex.Message}");
		}
	}
}