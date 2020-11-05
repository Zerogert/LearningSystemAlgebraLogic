using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class SettingsViewModel : BaseViewModel
	{
		private ModelDataContext context;
		private User user;
		private ICommand changeLoginCommand;
		private ICommand changePasswordCommand;
		private bool allowChangeLogin;
		private bool allowChangePassword;
		private string login;
		private string password;
		private string newPassword;
		public SettingsViewModel(ModelDataContext context, User user)
		{
			this.context = context;
			this.user = user;
		}

		public string Login {
			get => login;
			set {
				if (login == value)
					return;

				login = value;
				AllowChangeLogin = CheckAllowChangeLogin();
				OnPropertyChanged("Login");
			}
		}

		public string NewPassword {
			get => newPassword;
			set {
				if (newPassword == value)
					return;

				newPassword = value;
				AllowChangePassword = CheckAllowChangePassword();
				OnPropertyChanged("NewPassword");
			}
		}

		public string Password {
			get => password;
			set {
				if (password == value)
					return;

				password = value;
				AllowChangePassword = CheckAllowChangePassword();
				OnPropertyChanged("Password");
			}
		}

		public ICommand ChangeLoginCommand {
			get {
				changeLoginCommand = changeLoginCommand ?? new Command(ChangeLogin);
				return changeLoginCommand;
			}
		}

		public ICommand ChangePasswordCommand {
			get {
				changePasswordCommand = changePasswordCommand ?? new Command(ChangePassword);
				return changePasswordCommand;
			}
		}

		public bool AllowChangeLogin {
			get => allowChangeLogin;
			set {
				if (allowChangeLogin == value)
					return;

				allowChangeLogin = value;
				OnPropertyChanged("AllowChangeLogin");
			}
		}

		public bool AllowChangePassword {
			get => allowChangePassword;
			set {
				if (allowChangePassword == value)
					return;

				allowChangePassword = value;
				OnPropertyChanged("AllowChangePassword");
			}
		}

		private bool CheckAllowChangeLogin()
		{
			if (Login.Length==0)
			{
				return false;
			}
			if (Login==user.Login)
			{
				return false;
			}
			return true;
		}

		private bool CheckAllowChangePassword()
		{
			if (NewPassword==user.Password)
			{
				return false;
			}

			if (Password!=user.Password)
			{
				return false;
			}
			return true;
		}

		private void ChangeLogin()
		{
			user = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
			user.Login = Login;
			context.SaveChanges();
			AllowChangeLogin = CheckAllowChangeLogin();
		}

		private void ChangePassword() {
			user = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
			if (Password== user.Password)
			{
				user.Password = NewPassword;
				context.SaveChanges();
			}
			AllowChangePassword = CheckAllowChangePassword();
		}
	}
}