using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Enums;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class EnterCodeWorkViewModel : BaseViewModel
	{
		private readonly ModelDataContext context = new ModelDataContext();
		private string code = "";
		private ICommand enterCodeCommand;

		public string Code
		{
			get => code;
			set
			{
				if (code == value)
					return;

				code = value;
				OnPropertyChanged("Code");
			}
		}

		public ICommand EnterCodeCommand
		{
			get
			{
				enterCodeCommand = enterCodeCommand ?? new Command(EnterCode);
				return enterCodeCommand;
			}
		}

		private void EnterCode()
		{
			var work = context.CodeWorks.Include(cw => cw.VariantWork).Include(cw => cw.Student)
				.SingleOrDefault(w => w.Code.Contains(Code) && w.State.Contains(StateWork.NonComplete));
			if (work != null)
			{
				work.State = StateWork.Process;
				var controlPage = new ControlPage();
				controlPage.DataContext = new ControlViewModel(ref controlPage.Surface, work, context);
				context.SaveChanges();
				ManagerPage.ChangePage("Main", controlPage);
			}
		}
	}
}