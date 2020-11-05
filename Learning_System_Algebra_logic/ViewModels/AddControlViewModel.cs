using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Enums;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.Services;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;
using Microsoft.Win32;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class AddControlViewModel : BaseViewModel
	{
		private readonly ModelDataContext context = new ModelDataContext();
		private bool allowAddControl;
		private bool allowGenerate;
		private ICommand cancelCommand;
		private ObservableCollection<ControlWorkViewModel> controlWorkList;
		private ICommand exportToExcelCommand;
		private ICommand exportToWordCommand;
		private ICommand generateCommand;
		private ObservableCollection<Group> groupList;
		private ICommand printCommand;
		private ICommand runCommand;
		private Group selectedGroup;
		private Work selectedWork;
		private ObservableCollection<Work> workList;

		public AddControlViewModel()
		{
			ControlWorkList = new ObservableCollection<ControlWorkViewModel>();
			using (var db = new ModelDataContext())
			{
				var workList = from work in db.Works
					where work.TypeWork == TypeWork.Control
					select work;
				WorkList = new ObservableCollection<Work>(workList.ToList());
				GroupList = new ObservableCollection<Group>(db.Groups.ToList());
			}
		}

		public bool AllowAddControl
		{
			get => allowAddControl;
			set
			{
				if (allowAddControl == value)
					return;

				allowAddControl = value;
				OnPropertyChanged("AllowAddControl");
			}
		}

		public bool AllowGenerate
		{
			get => allowGenerate;
			set
			{
				if (allowGenerate == value)
					return;

				allowGenerate = value;
				OnPropertyChanged("AllowGenerate");
			}
		}

		public ObservableCollection<Group> GroupList
		{
			get => groupList;
			set
			{
				if (groupList == value)
					return;

				groupList = value;
				OnPropertyChanged("GroupList");
			}
		}

		public ObservableCollection<Work> WorkList
		{
			get => workList;
			set
			{
				if (workList == value)
					return;

				workList = value;
				OnPropertyChanged("WorkList");
			}
		}

		public ObservableCollection<ControlWorkViewModel> ControlWorkList
		{
			get => controlWorkList;
			set
			{
				if (controlWorkList == value)
					return;

				controlWorkList = value;
				OnPropertyChanged("ControlWorkList");
			}
		}

		public Work SelectedWork
		{
			get => selectedWork;
			set
			{
				if (selectedWork == value)
					return;
				selectedWork = value;
				AllowGenerate = CheckAllowGenerate();
				AllowAddControl = CheckAllowAddControl();
				OnPropertyChanged("SelectedWork");
			}
		}

		public Group SelectedGroup
		{
			get => selectedGroup;
			set
			{
				if (selectedGroup == value)
					return;
				selectedGroup = value;
				AllowGenerate = CheckAllowGenerate();
				AllowAddControl = CheckAllowAddControl();
				OnPropertyChanged("SelectedGroup");
			}
		}

		public ICommand GenerateCommand
		{
			get
			{
				generateCommand = generateCommand ?? new Command(Generate);
				return generateCommand;
			}
		}

		public ICommand PrintCommand
		{
			get
			{
				printCommand = printCommand ?? new Command(Print);
				return printCommand;
			}
		}

		public ICommand ExportToWordCommand
		{
			get
			{
				exportToWordCommand = exportToWordCommand ?? new Command(ExportToWord);
				return exportToWordCommand;
			}
		}

		public ICommand ExportToExcelCommand
		{
			get
			{
				exportToExcelCommand = exportToExcelCommand ?? new Command(ExportToExcel);
				return exportToExcelCommand;
			}
		}

		public ICommand AddControlCommand
		{
			get
			{
				runCommand = runCommand ?? new Command(AddControl);
				return runCommand;
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

		private void Generate()
		{
			ControlWorkList.Clear();
			var selectedStudents = context.Students.Where(student => student.Group.Name == SelectedGroup.Name)
				.Include(st => st.Group).ToList();
			var selectedVariantWorks = context.VariantWorks.Where(variant => variant.Work.Name == SelectedWork.Name)
				.Include(var => var.Work).ToList();
			var ran = new Random();
			foreach (var student in selectedStudents)
			{
				var variant = selectedVariantWorks.ToList()[ran.Next(selectedVariantWorks.Count())];
				var codeWork = new CodeWork
				{
					DateCreate = DateTime.Now,
					Student = student,
					VariantWork = variant,
					State = StateWork.NonComplete,
					Result = "",
					Code = Guid.NewGuid().ToString().Substring(0, 10)
				};
				ControlWorkList.Add(new ControlWorkViewModel(codeWork));
			}

			AllowAddControl = CheckAllowAddControl();
		}

		private void Print()
		{
			PrintDocument.Print(GenerateDocument.GenerateDocumentCodes(ControlWorkList));
		}

		private void ExportToWord()
		{
		}

		private void ExportToExcel()
		{
			var fileDialog = new SaveFileDialog();
			fileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";
			fileDialog.ShowDialog();
			if (!fileDialog.CheckFileExists)
				Services.ExportToExcel.ExportCodesToExcelFile(ControlWorkList, fileDialog.FileName);
		}

		private void AddControl()
		{
			foreach (var control in ControlWorkList) context.CodeWorks.Add(control.CodeWork);
			context.SaveChanges();
			Cancel();
		}

		private bool CheckAllowGenerate()
		{
			if (SelectedGroup == null || SelectedWork == null) return false;
			Generate();
			return true;
		}

		private bool CheckAllowAddControl()
		{
			if (SelectedGroup == null || SelectedWork == null) return false;

			if (!ControlWorkList.Any()) return false;
			return true;
		}

		private void Cancel()
		{
			ManagerPage.ChangePage("Work", new ManageControlPage {DataContext = new ManageControlViewModel(context)});
		}
	}
}