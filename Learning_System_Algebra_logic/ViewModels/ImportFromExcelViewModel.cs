using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.Services;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;
using Microsoft.Win32;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class ImportFromExcelViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private bool allowImport;
		private ICommand cancelCommand;
		private ICommand importCommand;
		private string nameFile = string.Empty;
		private string nameGroup = string.Empty;
		private ICommand selectExcelCommand;

		private ObservableCollection<StudentViewModel> students;

		public ImportFromExcelViewModel(ModelDataContext context)
		{
			this.context = context;
		}

		public string NameGroup
		{
			get => nameGroup;
			set
			{
				if (nameGroup == value)
					return;

				nameGroup = value;
				AllowImport = CheckAllowImport();
				OnPropertyChanged("NameGroup");
			}
		}

		public string NameFile
		{
			get => nameFile;
			set
			{
				if (nameFile == value)
					return;

				nameFile = value;
				AllowImport = CheckAllowImport();
				OnPropertyChanged("NameFile");
			}
		}

		public bool AllowImport
		{
			get => allowImport;
			set
			{
				if (allowImport == value)
					return;

				allowImport = value;
				OnPropertyChanged("AllowImport");
			}
		}

		public ObservableCollection<StudentViewModel> Students
		{
			get => students;
			set
			{
				if (students == value)
					return;

				students = value;
				OnPropertyChanged("Students");
			}
		}

		public ICommand SelectExcelCommand
		{
			get
			{
				selectExcelCommand = selectExcelCommand ?? new Command(SelectFile);
				return selectExcelCommand;
			}
		}

		public ICommand ImportCommand
		{
			get
			{
				importCommand = importCommand ?? new Command(Import);
				return importCommand;
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

		private bool CheckAllowImport()
		{
			if (nameGroup.Length == 0 && nameFile.Length == 0) return false;

			return true;
		}

		private void SelectFile()
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
			openFileDialog.ShowDialog();
			if (openFileDialog.CheckFileExists)
			{
				NameFile = openFileDialog.FileName;
				Students = new ObservableCollection<StudentViewModel>(
					ImportFromExcel.ImportStudentsFromExcel(openFileDialog.FileName));
			}
		}

		private void Import()
		{
			using (var db = new ModelDataContext())
			{
				var group = new Group {DateCreate = DateTime.Now.Year, Name = nameGroup};
				foreach (var student in Students)
				{
					var newStudent = new Student
					{
						FirstName = student.FirstName,
						LastName = student.LastName,
						MiddleName = student.MiddleName,
						Group = group,
						Note = student.Note
					};
					db.Students.Add(newStudent);
				}

				db.Groups.Add(group);
				db.SaveChanges();
			}
			Cancel();
		}

		private void Cancel()
		{
			ManagerPage.ChangePage("Work", new ManageGroupsPage {DataContext = new ManageGroupViewModel(context)});
		}
	}
}