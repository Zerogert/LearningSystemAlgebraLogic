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
	internal class ManageControlViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private ObservableCollection<ControlWorkViewModel> controlWorkList;
		private ICommand deleteWorkCommand;
		private ICommand exportToExcelCommand;
		private string filterLastName = "";
		private ICommand generateWorkCommand;
		private ObservableCollection<Group> groupFilterList;
		private DateTime selectedDateCreateFrom = DateTime.Today;
		private DateTime selectedDateCreateTo = DateTime.MaxValue;
		private DateTime selectedDateEndFrom = DateTime.MinValue;
		private DateTime selectedDateEndTo = DateTime.MaxValue;
		private string selectedGroupName = "";
		private string selectedState = "";
		private string selectedWork = "";
		private ObservableCollection<string> stateFilterList;
		private ObservableCollection<Work> workFilterList;

		public ManageControlViewModel(ModelDataContext context)
		{
			this.context = context;
			ControlWorkList = new ObservableCollection<ControlWorkViewModel>();
			GroupFilterList = new ObservableCollection<Group>(context.Groups.ToList());
			GroupFilterList.Insert(0, new Group {Name = ""});
			WorkFilterList = new ObservableCollection<Work>(context.Works.ToList());
			WorkFilterList.Insert(0, new Work {Name = ""});
			if (context.CodeWorks.Count() != 0)
			{
				SelectedDateCreateFrom = context.CodeWorks.Min(code => code.DateCreate);
				SelectedDateCreateTo = context.CodeWorks.Max(code => code.DateCreate);
				SelectedDateEndFrom = context.CodeWorks.Min(code => code.DateComplete);
				SelectedDateEndTo = context.CodeWorks.Max(code => code.DateComplete);
			}

			StateFilterList = new ObservableCollection<string>
				{"", StateWork.NonComplete, StateWork.Complete, StateWork.Process};
			ControlWorkList = new ObservableCollection<ControlWorkViewModel>();
			foreach (var codeWork in context.CodeWorks.Include(code => code.Student.Group)
				.Include(code => code.VariantWork.Work)
				.ToList()) ControlWorkList.Add(new ControlWorkViewModel(codeWork));
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

		public ObservableCollection<Group> GroupFilterList
		{
			get => groupFilterList;
			set
			{
				if (groupFilterList == value)
					return;

				groupFilterList = value;
				OnPropertyChanged("GroupFilterList");
			}
		}

		public ObservableCollection<string> StateFilterList
		{
			get => stateFilterList;
			set
			{
				if (stateFilterList == value)
					return;

				stateFilterList = value;
				OnPropertyChanged("StateFilterList");
			}
		}

		public ObservableCollection<Work> WorkFilterList
		{
			get => workFilterList;
			set
			{
				if (workFilterList == value)
					return;

				workFilterList = value;
				OnPropertyChanged("WorkFilterList");
			}
		}

		public string FilterLastName
		{
			get => filterLastName;
			set
			{
				if (filterLastName == value)
					return;

				filterLastName = value;
				OnPropertyChanged("FilterLastName");
				Filter();
			}
		}

		public string SelectedGroupName
		{
			get => selectedGroupName;
			set
			{
				if (selectedGroupName == value)
					return;

				selectedGroupName = value;
				OnPropertyChanged("SelectedGroupName");
				Filter();
			}
		}

		public string SelectedState
		{
			get => selectedState;
			set
			{
				if (selectedState == value)
					return;

				selectedState = value;
				OnPropertyChanged("SelectedState");
				Filter();
			}
		}

		public DateTime SelectedDateCreateFrom
		{
			get => selectedDateCreateFrom;
			set
			{
				if (selectedDateCreateFrom == value)
					return;

				selectedDateCreateFrom = value;
				OnPropertyChanged("SelectedDateCreateFrom");
				Filter();
			}
		}

		public DateTime SelectedDateCreateTo
		{
			get => selectedDateCreateTo;
			set
			{
				if (selectedDateCreateTo == value)
					return;

				selectedDateCreateTo = value;
				OnPropertyChanged("SelectedDateCreateTo");
				Filter();
			}
		}

		public string SelectedWork
		{
			get => selectedWork;
			set
			{
				if (selectedWork == value)
					return;

				selectedWork = value;
				OnPropertyChanged("SelectedWork");
				Filter();
			}
		}

		public DateTime SelectedDateEndFrom
		{
			get => selectedDateEndFrom;
			set
			{
				if (selectedDateEndFrom == value)
					return;

				selectedDateEndFrom = value;
				OnPropertyChanged("SelectedDateEndFrom");
				Filter();
			}
		}

		public DateTime SelectedDateEndTo
		{
			get => selectedDateEndTo;
			set
			{
				if (selectedDateEndTo == value)
					return;

				selectedDateEndTo = value;
				OnPropertyChanged("SelectedDateEndTo");
				Filter();
			}
		}

		public ICommand GenerateWorkCommand
		{
			get
			{
				generateWorkCommand = generateWorkCommand ?? new Command(GenerateWork);
				return generateWorkCommand;
			}
		}

		public ICommand ExportToExcelCommand
		{
			get
			{
				exportToExcelCommand = exportToExcelCommand ?? new Command(ExportToExcels);
				return exportToExcelCommand;
			}
		}


		public ICommand DeleteWorkCommand
		{
			get
			{
				deleteWorkCommand = deleteWorkCommand ?? new Command(DeleteWork);
				return deleteWorkCommand;
			}
		}

		private void Filter()
		{
			ControlWorkList.Clear();
			var filterCodeWorks = from work in context.CodeWorks.Include(code => code.Student.Group)
					.Include(code => code.VariantWork.Work).ToList()
				where work.Student.LastName.Contains(FilterLastName) &&
				      work.DateCreate >= SelectedDateCreateFrom && work.DateCreate <= SelectedDateCreateTo &&
				      work.DateComplete <= SelectedDateEndTo && work.DateComplete <= SelectedDateEndTo &&
				      work.State.Contains(SelectedState) &&
				      work.Student.Group.ToString().Contains(SelectedGroupName) &&
				      work.VariantWork.Work.Name.Contains(SelectedWork)
				select work;
			foreach (var codeWork in filterCodeWorks.ToList()) ControlWorkList.Add(new ControlWorkViewModel(codeWork));
		}

		private void GenerateWork()
		{
			ManagerPage.ChangePage("Work", new AddControlPage {DataContext = new AddControlViewModel()});
		}

		private void ExportToExcels()
		{
			var fileDialog = new SaveFileDialog();
			fileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";
			fileDialog.ShowDialog();
			if (fileDialog.FileName.Length>0)
				ExportToExcel.ExportCodesToExcelFile(ControlWorkList, fileDialog.FileName);
		}

		private void DeleteWork()
		{
			foreach (var work in ControlWorkList) context.CodeWorks.Remove(work.CodeWork);
			ControlWorkList.Clear();
			context.SaveChanges();
		}
	}
}