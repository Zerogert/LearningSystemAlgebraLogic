using System.Windows;
using System.Windows.Controls;

namespace Learning_System_Algebra_logic.Pages
{
	/// <summary>
	///     Interaction logic for ManageControlPage.xaml
	/// </summary>
	public partial class ManageControlPage : Page
	{
		public ManageControlPage()
		{
			InitializeComponent();
		}

		private void OnDataGridPrinting(object sender, RoutedEventArgs e)
		{
			var Printdlg = new PrintDialog();
			if (Printdlg.ShowDialog().GetValueOrDefault())
			{
				var pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
				// sizing of the element.
				Works.Measure(pageSize);
				Works.Arrange(new Rect(25, 25, pageSize.Width, pageSize.Height));
				Printdlg.PrintVisual(Works, Title);
			}
		}
	}
}