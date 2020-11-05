using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.ViewModels;

namespace Learning_System_Algebra_logic
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
			using (var db = new ModelDataContext())
			{
				try
				{
					if (!db.Users.Any())
					{
						db.Users.Add(new User {EMail = "sdf@mail.ru", Login = "admin", Password = "qwerty"});
						db.SaveChanges();
					}
				}
				catch (Exception e)
				{
					//db.Database.Delete();
					db.Database.CreateIfNotExists();
				}
			}
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