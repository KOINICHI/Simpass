using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
		
		public void DisplayPasswordEdit(Password password)
		{
			currentPassword = password;

			mainWindow.PasswordEdit.DataContext = password.id;

			ClearPasswordEdit();

			TextBox nameTextBox = createPasswordFieldTextBox(password.name, 22, 33);
			nameTextBox.Name = "passwordEditNameTextBox";
			mainWindow.PasswordEditName.Children.Add(nameTextBox);

			TextBox usernameTextBox = createPasswordFieldTextBox(password.username);
			usernameTextBox.Name = "passwordEditUsernameTextBox";
			mainWindow.PasswordEditUsername.Children.Add(usernameTextBox);

			TextBox passwordTextBox = createPasswordFieldTextBox(password.password);
			passwordTextBox.Name = "passwordEditPasswordTextBox";
			mainWindow.PasswordEditPassword.Children.Add(passwordTextBox);

			TextBox websiteTextBox = createPasswordFieldTextBox(password.website);
			websiteTextBox.Name = "passwordEditWebsiteTextBox";
			mainWindow.PasswordEditWebsite.Children.Add(websiteTextBox);

			TextBox noteTextBox = createPasswordFieldTextBox(password.note);
			noteTextBox.Height = 81;
			noteTextBox.BorderThickness = new Thickness(1);
			noteTextBox.Padding = new Thickness(3);
			noteTextBox.Name = "passwordEditNoteTextBox";
			mainWindow.PasswordEditNote.Children.Add(noteTextBox);

			TextBox tagTextBox = createPasswordFieldTextBox(String.Join(", ", password.tags));
			tagTextBox.Name = "passwordEditTagTextBox";
			mainWindow.PasswordEditTag.Children.Add(tagTextBox);

			Label lastModifiedLabel = new Label();
			lastModifiedLabel.Content = password.modified.ToString();
			lastModifiedLabel.FontSize = 18;
			lastModifiedLabel.Margin = new Thickness(30, 0, 0, 0);
			lastModifiedLabel.VerticalAlignment = VerticalAlignment.Center;
			mainWindow.PasswordEditMetadata.Children.Add(lastModifiedLabel);

			mainWindow.DeleteButtonImage.Visibility = Visibility.Visible;
		}

		public void displayStartup()
		{
			ClearPasswordEdit();
		}

		private void ClearPasswordEdit()
		{
			mainWindow.PasswordEditName.Children.Clear();
			mainWindow.PasswordEditUsername.Children.Clear();
			mainWindow.PasswordEditPassword.Children.Clear();
			mainWindow.PasswordEditWebsite.Children.Clear();
			mainWindow.PasswordEditNote.Children.Clear();
			mainWindow.PasswordEditTag.Children.Clear();
			mainWindow.PasswordEditMetadata.Children.Clear();
			mainWindow.DeleteButtonImage.Visibility = Visibility.Collapsed;
		}
		
		private TextBox createPasswordFieldTextBox(string text, double fontSize = 18, double height = 27)
		{
			TextBox textBox = new TextBox();
			textBox.Text = text;
			textBox.FontSize = fontSize;
			textBox.Width = 530.0;
			textBox.Height = height;
			textBox.VerticalAlignment = VerticalAlignment.Center;
			textBox.Margin = new Thickness(30, 0, 0, 0);
			textBox.BorderThickness = new Thickness(0, 0, 0, 1);
			textBox.TextChanged += mainWindow.passwordEdit_TextChanged;

			return textBox;
		}
	}
	
}
