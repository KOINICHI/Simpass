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
			ExitButtonImage.Click += ExitButtonImage_Click;
			AddButtonImage.Click += AddButtonImage_Click;
			ConfigButtonImage.Click += ConfigButtonImage_Click;
			DeleteButtonImage.Click += DeleteButtonImage_Click;
		}

		void ExitButtonImage_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void AddButtonImage_Click(object sender, EventArgs e)
		{
			Password password = new Password();
			manager.addPassword(password);

			displayer.DisplayPasswordEdit(password);
			displayer.DisplayPasswordList(manager.passwords);
		}

		void ConfigButtonImage_Click(object sender, EventArgs e)
		{
		}

		public void passwordList_MouseDown(object sender, EventArgs e)
		{
			Label label = (Label)sender;
			displayer.DisplayPasswordEdit((Password)label.DataContext);
		}

		public void passwordEdit_TextChanged(object sender, EventArgs e)
		{
			Guid id = (Guid)PasswordEdit.DataContext;
			foreach (Password password in manager.passwords)
			{
				if (password.id == id)
				{
					password.name = ((TextBox)PasswordEditName.Children[0]).Text;
					password.username = ((TextBox)PasswordEditUsername.Children[0]).Text;
					password.password = ((TextBox)PasswordEditPassword.Children[0]).Text;
					password.website = ((TextBox)PasswordEditWebsite.Children[0]).Text;
					password.note = ((TextBox)PasswordEditNote.Children[0]).Text;
					password.tags = new List<String>(((TextBox)PasswordEditTag.Children[0]).Text.Split(','));
				}
			}
			displayer.DisplayPasswordList(manager.passwords);
		}

		void DeleteButtonImage_Click(object sender, RoutedEventArgs e)
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