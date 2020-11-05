using System.Windows.Controls;
using Learning_System_Algebra_logic.ViewModels;

namespace Learning_System_Algebra_logic.Pages
{
	/// <summary>
	///     Interaction logic for Theory.xaml
	/// </summary>
	public partial class Theory : Page
	{
		public Theory()
		{
			InitializeComponent();
			DataContext = new TheoryViewModel();
		}
	}
}