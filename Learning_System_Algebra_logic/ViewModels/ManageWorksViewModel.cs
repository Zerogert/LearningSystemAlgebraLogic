using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class ManageWorksViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private ICommand addWorkCommand;
		private ICommand deleteWorkCommand;
		private ICommand editWorkCommand;
		private string filterName = "";
		private string selectedTypeWork = "";
		private WorkViewModel selectedWork;
		private ObservableCollection<string> typeWorkFilterList;
		private ObservableCollection<WorkViewModel> worksList;

		public ManageWorksViewModel(ModelDataContext context)
		{
			WorksList = new ObservableCollection<WorkViewModel>();
			TypeWorkFilterList = new ObservableCollection<string>();
			this.context = context;
			context.CodeWorks.Load();
			context.VariantWorks.Load();
			foreach (var work in context.Works.Include(w => w.VariantWorks).ToList())
				WorksList.Add(new WorkViewModel(work));
			typeWorkFilterList.Add("");
			typeWorkFilterList.Add("Контрольное задание");
			typeWorkFilterList.Add("Практическое задание");
		}

		public ICommand AddWorkCommand
		{
			get
			{
				addWorkCommand = addWorkCommand ?? new Command(AddWork);
				return addWorkCommand;
			}
		}

		public ICommand EditWorkCommand
		{
			get
			{
				editWorkCommand = editWorkCommand ?? new Command(EditWork);
				return editWorkCommand;
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

		public string FilterName
		{
			get => filterName;
			set
			{
				if (filterName == value)
					return;

				filterName = value;
				OnPropertyChanged("FilterName");
				Filter();
			}
		}

		public WorkViewModel SelectedWork
		{
			get => selectedWork;
			set
			{
				if (selectedWork == value)
					return;

				selectedWork = value;
				OnPropertyChanged("SelectedWork");
			}
		}

		public string SelectedTypeWork
		{
			get => selectedTypeWork;
			set
			{
				if (selectedTypeWork == value)
					return;

				selectedTypeWork = value;
				OnPropertyChanged("SelectedGroupName");
				Filter();
			}
		}

		public ObservableCollection<WorkViewModel> WorksList
		{
			get => worksList;
			set
			{
				if (worksList == value)
					return;

				worksList = value;
				OnPropertyChanged("WorksList");
			}
		}

		public ObservableCollection<string> TypeWorkFilterList
		{
			get => typeWorkFilterList;
			set
			{
				if (typeWorkFilterList == value)
					return;

				typeWorkFilterList = value;
				OnPropertyChanged("TypeWorkFilterList");
			}
		}

		private void AddWork()
		{
			ManagerPage.ChangePage("Work", new AddWorkPage {DataContext = new AddWorkViewModel(context)});
		}

		private void EditWork()
		{
			if (SelectedWork != null)
				ManagerPage.ChangePage("Work",
					new AddWorkPage {DataContext = new AddWorkViewModel(context, SelectedWork.Work)});
		}

		private void DeleteWork()
		{
			foreach (var work in WorksList) context.Works.Remove(work.Work);
			context.Works.Remove(selectedWork.Work);
			worksList.Remove(selectedWork);
			context.SaveChanges();
		}

		private void Filter()
		{
			WorksList.Clear();
			var name = FilterName;
			var filterWork = from work in context.Works.Include(w => w.VariantWorks).ToList()
				where work.Name.Contains(filterName) && work.TypeWork.Contains(SelectedTypeWork)
				select work;
			foreach (var variant in filterWork) WorksList.Add(new WorkViewModel(variant));
		}
	}
}