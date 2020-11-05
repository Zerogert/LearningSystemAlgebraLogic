using System.Windows.Controls;
using System.Windows.Input;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class MainViewModel : BaseViewModel
	{
		private Page _currentPage;
		private ICommand exitCommand;
		private bool isLoading;

		public MainViewModel()
		{
			CurrentPage = new SelectUserPage {DataContext = new SelectUserViewModel()};
			ManagerPage.AddFrame("Main", ChangePage);
			ManagerPage.SetSpinner(StartSpinner, StopSpinner);
		}

		public ICommand ExitCommand
		{
			get
			{
				exitCommand = exitCommand ?? new Command(Exit);
				return exitCommand;
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

		public bool IsLoading
		{
			get => isLoading;
			set
			{
				if (isLoading == value)
					return;

				isLoading = value;
				OnPropertyChanged("IsLoading");
			}
		}

		public void LongRunningMethod()
		{
		}

		public void StartSpinner()
		{
			IsLoading = true;
		}

		public void StopSpinner()
		{
			IsLoading = false;
		}

		public void ChangePage(Page page)
		{
			CurrentPage = page;
		}

		public void Exit()
		{
		}

		~MainViewModel()
		{
			ManagerPage.DeleteFrame("Main");
		}
	}
}