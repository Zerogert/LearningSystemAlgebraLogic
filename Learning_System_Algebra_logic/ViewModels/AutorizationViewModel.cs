using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class AutorizationViewModel : BaseViewModel
	{
		private ICommand loginCommand;

		private ICommand returnSelectCommand;
		public string Login { get; set; }
		public string Password { get; set; }

		public ICommand LoginCommand
		{
			get
			{
				loginCommand = loginCommand ?? new Command(Loging);
				return loginCommand;
			}
		}

		public ICommand ReturnSelectCommand
		{
			get
			{
				returnSelectCommand = returnSelectCommand ?? new Command(ReturnSelect);
				return returnSelectCommand;
			}
		}

		public void Loging()
		{
			using (var db = new ModelDataContext())
			{
				var user = db.Authorization(Login, Password);
				if (user != null)
					//AuthUser = user;
					//isAuth = true;
					ManagerPage.ChangePage("Main", new Navigation {DataContext = new NavigationViewModel("Teacher", user)});
			}
		}

		public void ReturnSelect()
		{
			ManagerPage.ChangePage("Main", new SelectUserPage {DataContext = new SelectUserViewModel()});
		}
	}
}