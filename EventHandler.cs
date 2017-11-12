using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Simpass
{
	public partial class MainWindow : System.Windows.Window
	{
		private void AddEventHandlers()
		{
			HomeButtonImage.Click += HomeButtonImage_Click;
			ExitButtonImage.Click += ExitButtonImage_Click;
			AddButtonImage.Click += AddButtonImage_Click;
			ConfigButtonImage.Click += ConfigButtonImage_Click;
			DeleteButtonImage.Click += DeleteButtonImage_Click;
			GenerateButtonImage.Click += GenerateButtonImage_Click;
			passwordEditNameTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;
			passwordEditUsernameTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;
			passwordEditPasswordTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;
			passwordEditWebsiteTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;
			passwordEditNoteTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;
			passwordEditTagTextBox.PreviewTextInput += passwordEdit_PreviewTextInput;

		}

		private void HomeButtonImage_Click(object sender, RoutedEventArgs e)
		{
			displayer.displayStartup();
		}

		private void GenerateButtonImage_Click(object sender, RoutedEventArgs e)
		{
			displayer.currentPassword.password = manager.GeneratePassword();
			displayer.DisplayCurrentPassword();
		}

		private void ExitButtonImage_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void AddButtonImage_Click(object sender, EventArgs e)
		{
			Password password = new Password();
			manager.addPassword(password);

			displayer.DisplayPasswordEdit(password);
			displayer.DisplayPasswordList(manager.passwords);
		}

		private void ConfigButtonImage_Click(object sender, EventArgs e)
		{
		}

		public void passwordList_MouseDown(object sender, EventArgs e)
		{
			Label label = (Label)sender;
			displayer.DisplayPasswordEdit((Password)label.DataContext);
		}

		private void passwordEdit_PreviewTextInput(object sender, EventArgs e)
		{
			Guid id = (Guid)PasswordEdit.DataContext;
			foreach (Password password in manager.passwords)
			{
				if (password.id == id)
				{
					password.name = passwordEditNameTextBox.Text;
					password.username = passwordEditUsernameTextBox.Text;
					password.password = passwordEditPasswordTextBox.Text;
					password.website = passwordEditWebsiteTextBox.Text;
					password.note = passwordEditNoteTextBox.Text;
					password.tags = new List<String>(passwordEditTagTextBox.Text.Split(','));
				}
			}
			displayer.DisplayPasswordList(manager.passwords);
		}

		private void DeleteButtonImage_Click(object sender, RoutedEventArgs e)
		{
			manager.deletePassword(displayer.currentPassword);
			displayer.DisplayPasswordList(manager.passwords);
			displayer.displayStartup();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}
	}
}