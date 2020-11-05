using System;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class AddGroupViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private ICommand addCommand;
		private ICommand cancelCommand;
		private string name = string.Empty;

		public AddGroupViewModel(ModelDataContext context)
		{
			this.context = new ModelDataContext();
		}

		public Action CloseAction { get; set; }

		public string Name
		{
			get => name;
			set
			{
				if (name == value)
					return;

				name = value;
				OnPropertyChanged("Name");
			}
		}

		public ICommand AddCommand
		{
			get
			{
				addCommand = addCommand ?? new Command(Add);
				return addCommand;
			}
		}

		public ICommand CancelCommand
		{
			get
			{
				cancelCommand = cancelCommand ?? new Command(Cancel);
				return cancelCommand;
			}
		}

		private void Add()
		{
			if (Name.Length > 0)
			{
				context.Groups.Add(new Group {DateCreate = DateTime.Now.Year, Name = name});
				context.SaveChanges();
				Cancel();
			}
		}

		private void Cancel()
		{
			CloseAction();
		}
	}
}