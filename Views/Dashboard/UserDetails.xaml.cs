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
	private void LoadUserData()
	{
		// Retrieve user data from Secure Storage
		string userDataJson = SecureStorage.GetAsync("UserData").Result ?? "[]";

		// Deserialize JSON to List<User>
		List<SignupPage.User> userList = System.Text.Json.JsonSerializer.Deserialize<List<SignupPage.User>>(userDataJson)!;

		// Set the List<User> as the ItemsSource for the ListView
		usersListView.ItemsSource = userList;
	}

	private void LogoutButton_Clicked(object sender, EventArgs e)
	{
		// crete a new NavigationPage with LoginPage as the root page
		var loginPage = new LoginPage();
		var navigationPage = new NavigationPage(loginPage);

		// Set the new NavigationPage as the main page
		Application.Current!.MainPage = navigationPage;
	}
}