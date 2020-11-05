using System.Windows;
using System.Windows.Controls;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class NavigationViewModel : BaseViewModel
	{
		private readonly ModelDataContext context = new ModelDataContext();
		private Page _currentPage;

		private Visibility isVisibilityStudent;
		private Visibility isVisibilityTeacher;

		private int selectedIndexMenu;

		private ListViewItem selectedItemMenu;
		private ListViewItem selectedItemMenuBottom;
		private User user;


		public NavigationViewModel(string role, User user) {
			ManagerPage.AddFrame("Work", ChangePage);
			IsVisibilityTeacher = Visibility.Visible;
			IsVisibilityStudent = Visibility.Collapsed;
			this.user = user;
			CurrentPage = new ManageGroupsPage { DataContext = new ManageGroupViewModel(context) };
		}

		public NavigationViewModel(string role)
		{
			ManagerPage.AddFrame("Work", ChangePage);
			IsVisibilityTeacher = Visibility.Collapsed;
			IsVisibilityStudent = Visibility.Visible;
			CurrentPage = new Theory { DataContext = new TheoryViewModel() };
		}

		public NavigationViewModel()
		{
			ManagerPage.AddFrame("Work", ChangePage);
			CurrentPage = new Theory {DataContext = new TheoryViewModel()};
		}

		public Visibility IsVisibilityTeacher
		{
			get => isVisibilityTeacher;
			set
			{
				if (isVisibilityTeacher == value)
					return;

				isVisibilityTeacher = value;
				OnPropertyChanged("IsVisibilityTeacher");
			}
		}

		public Visibility IsVisibilityStudent
		{
			get => isVisibilityStudent;
			set
			{
				if (isVisibilityStudent == value)
					return;

				isVisibilityStudent = value;
				OnPropertyChanged("IsVisibilityStudent");
			}
		}

		public Page CurrentPage
		{
			get => _currentPage;
			set
			{
				if (_currentPage == value)
					return;

				_currentPage = value;
				OnPropertyChanged("CurrentPage");
			}
		}

		public int SelectedIndexMenu
		{
			get => selectedIndexMenu;
			set
			{
				if (selectedIndexMenu == value)
					return;

				selectedIndexMenu = value;
				OnPropertyChanged("SelectedIndexMenu");
			}
		}

		public ListViewItem SelectedItemMenu
		{
			get => selectedItemMenu;
			set
			{
				if (selectedItemMenu == value)
					return;

				selectedItemMenu = value;
				SelectedMenu(value.Name);
				OnPropertyChanged("SelectedItemMenu");
			}
		}

		public ListViewItem SelectedItemMenuBottom
		{
			get => selectedItemMenuBottom;
			set
			{
				if (selectedItemMenuBottom == value)
					return;

				selectedItemMenuBottom = value;
				SelectedMenuBottom(value.Name);
				OnPropertyChanged("SelectedItemMenuBottom");
			}
		}

		public void ChangePage(Page page)
		{
			CurrentPage = page;
		}

		public void SelectedMenu(string name)
		{
			switch (name)
			{
				case "Theory":
					CurrentPage = new Theory {DataContext = new TheoryViewModel()};
					break;
				case "Practice":
					CurrentPage = new Practice {DataContext = null};
					CurrentPage.DataContext =
						new PracticeViewModel(ref ((Practice) CurrentPage).Surface, context);
					break;
				case "Test":
					CurrentPage = new EnterCodeWork {DataContext = new EnterCodeWorkViewModel()};
					break;
				case "CreateScheme":
					CurrentPage = new CreateScheme();
					CurrentPage.DataContext =
						new Logical_cxem.ViewModels.MainViewModel(ref ((CreateScheme) CurrentPage).Surface);
					break;
				case "ManageGroup":
					CurrentPage = new ManageGroupsPage {DataContext = new ManageGroupViewModel(context)};
					break;
				case "ManageWorks":
					CurrentPage = new ManageWorksPage {DataContext = new ManageWorksViewModel(context)};
					break;
				case "ManageControl":
					CurrentPage = new ManageControlPage {DataContext = new ManageControlViewModel(context)};
					break;
				case "Setting":
					CurrentPage = new Settings() {DataContext = new SelectUserViewModel()};
					break;
				case "Exit":
					ManagerPage.ChangePage("Main", new SelectUserPage {DataContext = new SelectUserViewModel()});
					break;
			}
		}

		public void SelectedMenuBottom(string name)
		{
			switch (name)
			{
				case "Setting":
					CurrentPage = new Settings() { DataContext = new SettingsViewModel(context, user) };
					break;
				case "Exit":
					ManagerPage.ChangePage("Main", new SelectUserPage {DataContext = new SelectUserViewModel()});
					break;
			}
		}

		~NavigationViewModel()
		{
			ManagerPage.DeleteFrame("Work");
		}
	}
}