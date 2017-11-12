using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Simpass
{
	class Displayer
	{
		MainWindow mainWindow;
		public Password currentPassword;

		public Displayer(MainWindow _mainWindow)
		{
			mainWindow = _mainWindow;
		}

		public void DisplayPasswordList(List<Password> passwords)
		{
			var passwordList = mainWindow.PasswordList.Children;

			passwordList.Clear();
			for (int i = 0; i < passwords.Count; i++ )
			{
				Password password = passwords[i];
				Label label = new Label();
				label.Content = password.name;
				label.FontSize = 18;
				label.Margin = new Thickness(0, 0, 0, 0);
				label.Padding = new Thickness(10, 5, 5, 5);
				if ((i & 1) == 0)
				{
					label.Background = new SolidColorBrush(Color.FromArgb(255, 187, 155, 90));
				}
				else
				{
					label.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
				}
				label.MouseDown += mainWindow.passwordList_MouseDown;
				label.DataContext = password;
				passwordList.Add(label);
			}
		}

		public void DisplayCurrentPassword()
		{
			DisplayPasswordEdit(currentPassword);
		}

		public void DisplayPasswordEdit(Password password)
		{
			currentPassword = password;

			mainWindow.PasswordEdit.DataContext = password.id;

			ClearPasswordEdit();

			mainWindow.passwordEditNameTextBox.Text = currentPassword.name;

			mainWindow.passwordEditUsernameTextBox.Text = password.username;

			mainWindow.passwordEditPasswordTextBox.Text = currentPassword.password;

			mainWindow.passwordEditWebsiteTextBox.Text = password.website;
			
			mainWindow.passwordEditNoteTextBox.Text = password.note;

			mainWindow.passwordEditTagTextBox.Text = String.Join(", ", password.tags);

			Label lastModifiedLabel = new Label();
			lastModifiedLabel.Content = password.modified.ToString();
			lastModifiedLabel.FontSize = 18;
			lastModifiedLabel.Margin = new Thickness(30, 0, 0, 0);
			lastModifiedLabel.VerticalAlignment = VerticalAlignment.Center;
			mainWindow.PasswordEditMetadata.Children.Add(lastModifiedLabel);

			mainWindow.PasswordEdit.Visibility = Visibility.Visible;
		}

		public void displayStartup()
		{
			mainWindow.PasswordEdit.Visibility = Visibility.Collapsed;
			ClearPasswordEdit();
		}

		private void ClearPasswordEdit()
		{
			mainWindow.passwordEditNameTextBox.Text = "Name";
			mainWindow.passwordEditUsernameTextBox.Text = "Username";
			mainWindow.passwordEditPasswordTextBox.Text = "Password";
			mainWindow.passwordEditWebsiteTextBox.Text = "https://";
			mainWindow.passwordEditNoteTextBox.Text = "Notes";
			mainWindow.passwordEditTagTextBox.Text = "Tags";
			mainWindow.PasswordEditMetadata.Children.Clear();
		}
	}
	
}
