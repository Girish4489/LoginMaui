using System.Collections.Generic;
using System.Linq;
using LoginMaui.Views.Dashboard;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoginMaui.Views.Auth
{
	public partial class SignupPage : ContentPage
	{
		private static List<User> users = new List<User>();

		public SignupPage()
		{
			InitializeComponent();

			// Load existing user data when SignupPage is initialized
			Task task = LoadUserDataAsync(); ;
		}

		private async void LoginButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}

		private async void SignupButton_Clicked(object sender, EventArgs e)
		{
			string username = usernameEntry.Text;
			string email = emailEntry.Text;
			string password = passwordEntry.Text;
			DateTime dob = datePicker.Date;
			string address = addressEditor.Text;

			// Validate input (add your own validation logic)

			// Create a user object
			User newUser = new User
			{
				Username = username,
				Email = email,
				Password = password,
				DOB = dob,
				Address = address
			};

			// Save user data to the collection
			users.Add(newUser);
			SaveUserData(users);

			await DisplayAlert("Success", "User data saved successfully", "OK");

			usernameEntry.Text = "";
			emailEntry.Text = "";
			passwordEntry.Text = "";
			datePicker.Date = DateTime.Now;
			addressEditor.Text = "";

			// Navigate to Login page
			await Navigation.PushAsync(new LoginPage());
		}

		private async Task LoadUserDataAsync()
		{
			try
			{
				// Retrieve user data from Secure Storage asynchronously
				string userDataJson = await SecureStorage.GetAsync("UserData") ?? "[]";

				// Deserialize JSON to List<User>
				users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userDataJson) ?? new List<User>();
			}
			catch (Exception ex)
			{
				// Handle exceptions (add your own error handling logic)
				Console.WriteLine($"Error loading user data: {ex.Message}");
				Debug.WriteLine($"Error loading user data: {ex.Message}");
			}
		}

		private void SaveUserData(List<User> userList)
		{
			// Convert List<User> to JSON using System.Text.Json
			string userDataJson = System.Text.Json.JsonSerializer.Serialize(userList);

			SecureStorage.SetAsync("UserData", userDataJson);
			Console.WriteLine("Saving user data to secure storage", userDataJson);
			Debug.WriteLine("Saving user data to secure storage", userDataJson);
		}

		// Define your User class (customize based on your needs)
		public class User
		{
			public required string Username { get; set; }
			public required string Email { get; set; }
			public required string Password { get; set; }
			public DateTime DOB { get; set; }
			public string? Address { get; set; }
		}
	}
}
