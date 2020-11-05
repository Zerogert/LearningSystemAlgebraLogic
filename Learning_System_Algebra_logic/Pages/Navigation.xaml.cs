using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Learning_System_Algebra_logic.Pages
{
	/// <summary>
	///     Interaction logic for Navigation.xaml
	/// </summary>
	public partial class Navigation : Page
	{
		public Navigation()
		{
			InitializeComponent();
		}

		private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Visible;
			ButtonOpenMenu.Visibility = Visibility.Collapsed;
		}

		private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Collapsed;
			ButtonOpenMenu.Visibility = Visibility.Visible;
		}

		private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			KeyGesture backKeyGesture = null;
			foreach (var gesture in NavigationCommands.BrowseBack.InputGestures)
			{
				var keyGesture = gesture as KeyGesture;
				if (keyGesture != null &&
				    keyGesture.Key == Key.Back &&
				    keyGesture.Modifiers == ModifierKeys.None)
					backKeyGesture = keyGesture;
			}

			if (backKeyGesture != null) NavigationCommands.BrowseBack.InputGestures.Remove(backKeyGesture);
		}

		private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
		{
			KeyGesture backKeyGesture = null;
			foreach (var gesture in NavigationCommands.BrowseBack.InputGestures)
			{
				var keyGesture = gesture as KeyGesture;
				if (keyGesture != null &&
				    keyGesture.Key == Key.Back &&
				    keyGesture.Modifiers == ModifierKeys.None)
					backKeyGesture = keyGesture;
			}

			if (backKeyGesture != null) NavigationCommands.BrowseBack.InputGestures.Remove(backKeyGesture);
		}
	}
}