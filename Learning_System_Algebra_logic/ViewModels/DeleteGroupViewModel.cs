using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class DeleteGroupViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private bool allowDelete;

		private ICommand cancelCommand;

		private ICommand deleteCommand;
		private ObservableCollection<Group> groupList;

		private Group selectedGroup;
		private ObservableCollection<StudentViewModel> students = new ObservableCollection<StudentViewModel>();

		public DeleteGroupViewModel(ModelDataContext context)
		{
			this.context = context;
			GroupList = new ObservableCollection<Group>(context.Groups.ToList());
		}

		public Group SelectedGroup
		{
			get => selectedGroup;
			set
			{
				if (selectedGroup == value)
					return;

				selectedGroup = value;
				AllowDelete = CheckAllowDelete();
				LoadStudents();
				OnPropertyChanged("SelectedGroup");
			}
		}

		public bool AllowDelete
		{
			get => allowDelete;
			set
			{
				if (allowDelete == value)
					return;

				allowDelete = value;
				OnPropertyChanged("AllowDelete");
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

		public ICommand DeleteCommand
		{
			get
			{
				deleteCommand = deleteCommand ?? new Command(Delete);
				return deleteCommand;
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

		private bool CheckAllowDelete()
		{
			if (SelectedGroup == null) return false;

			return true;
		}

		private void LoadStudents()
		{
			Students.Clear();
			foreach (var student in context.Students.Include(st => st.Group).ToList())
				if (student.Group.GroupId == SelectedGroup.GroupId)
					Students.Add(new StudentViewModel(student));
		}

		private void Delete()
		{
			using (var db = new ModelDataContext())
			{
				db.Groups.Remove(db.Groups.Include(gr => gr.Students).ToList()
					.FirstOrDefault(gr => gr.GroupId == SelectedGroup.GroupId));
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