using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Simpass
{
	public partial class MainWindow : System.Windows.Window
	{
		private void MakeDraggable()
		{
			LayoutRootGrid.MouseDown += BorderGrid_MouseDown;
		}

		void BorderGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}

	}
}