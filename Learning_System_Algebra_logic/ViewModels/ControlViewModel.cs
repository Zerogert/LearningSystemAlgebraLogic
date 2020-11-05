using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Enums;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class ControlViewModel : Logical_cxem.ViewModels.MainViewModel, IDisposable
	{
		private readonly ModelDataContext context;
		private readonly CodeWork work;
		private ICommand exitCommand;
		private Visibility isShowCheck = Visibility.Visible;
		private Visibility isShowExit = Visibility.Collapsed;

		public ControlViewModel(ref Canvas canvas, CodeWork work, ModelDataContext context) : base(ref canvas)
		{
			this.work = work;
			this.context = context;
			MakeControl(work.VariantWork.Attachment);
		}

		public Visibility IsShowCheck
		{
			get => isShowCheck;
			set
			{
				if (isShowCheck == value)
					return;
				isShowCheck = value;
				OnPropertyChanged();
			}
		}

		public Visibility IsShowExit
		{
			get => isShowExit;
			set
			{
				if (isShowExit == value)
					return;
				isShowExit = value;
				OnPropertyChanged();
			}
		}

		public ICommand ExitCommand
		{
			get
			{
				exitCommand = exitCommand ?? new Command(Exit);
				return exitCommand;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public override void CheckCommand()
		{
			base.CheckCommand();
			var result = CheckGroup();
			work.Result = MakeMark(result.Count - result.Error, result.Count).ToString();
			work.State = StateWork.Complete;
			work.DateComplete = DateTime.Now;
			context.SaveChanges();
			IsShowCheck = Visibility.Collapsed;
			IsShowExit = Visibility.Visible;
			var resultWork = new ResultWork
			{
				DataContext =
					new ResultWorkViewModel(work.Student.ToString(), result.Count, result.Count - result.Error)
			};
			resultWork.ShowDialog();
		}

		public void ShowErrors()
		{
		}

		private int MakeMark(int countTrue, int countSchemes)
		{
			var procent = (int) ((float) countTrue / countSchemes * 100);
			if (procent > 55)
			{
				if (procent > 70)
				{
					if (procent > 85)
						return 5;
					return 4;
				}
				return 3;
			}

			return 2;
		}

		private void Exit()
		{
			ManagerPage.ChangePage("Main", new Navigation {DataContext = new NavigationViewModel("Student")});
		}

		~ControlViewModel()
		{
			Dispose(false);
		}

		private void ReleaseUnmanagedResources()
		{
			// TODO release unmanaged resources here
		}

		private void Dispose(bool disposing)
		{
			work.State = StateWork.Complete;
			work.DateComplete = DateTime.Now;
			context.SaveChanges();
			ReleaseUnmanagedResources();
			if (disposing) context?.Dispose();
		}
	}
}