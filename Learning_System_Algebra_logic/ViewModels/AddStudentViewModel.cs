using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;
using Learning_System_Algebra_logic.Windows;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class AddStudentViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;
		private readonly Student student;
		private ICommand addGroupCommand;
		private ICommand exitCommand;
		private string firstName = "";
		private ObservableCollection<Group> grouprList;
		private string lastName = "";
		private string middleName = "";
		private string note = "";
		private ICommand saveCommand;
		private Group selectedGroup;

		public AddStudentViewModel(ModelDataContext context)
		{
			this.context = context;
			GroupsList = new ObservableCollection<Group>(context.Groups.ToList());
			if (GroupsList.Count > 0) SelectedGroup = GroupsList[0];
		}

		public AddStudentViewModel(ModelDataContext context, StudentViewModel studentViewModel) : this(context)
		{
			var student = studentViewModel.Student;
			LastName = student.LastName;
			MiddleName = student.MiddleName;
			FirstName = student.FirstName;
			Note = student.Note;
			SelectedGroup = student.Group;
			this.student = student;
		}

		public ICommand SaveCommand
		{
			get
			{
				saveCommand = saveCommand ?? new Command(Save);
				return saveCommand;
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

		public ICommand AddGroupCommand
		{
			get
			{
				addGroupCommand = addGroupCommand ?? new Command(AddGroup);
				return addGroupCommand;
			}
		}

		public string LastName
		{
			get => lastName;
			set
			{
				if (lastName == value)
					return;

				lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get => firstName;
			set
			{
				if (firstName == value)
					return;

				firstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string MiddleName
		{
			get => middleName;
			set
			{
				if (middleName == value)
					return;

				middleName = value;
				OnPropertyChanged("MiddleName");
			}
		}

		public string Note
		{
			get => note;
			set
			{
				if (note == value)
					return;

				note = value;
				OnPropertyChanged("Note");
			}
		}

		public ObservableCollection<Group> GroupsList
		{
			get => grouprList;
			set
			{
				if (grouprList == value)
					return;

				grouprList = value;
				OnPropertyChanged("GroupsList");
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
				OnPropertyChanged("SelectedGroup");
			}
		}

		private void Save()
		{
			if (student != null)
			{
				student.LastName = LastName;
				student.MiddleName = MiddleName;
				student.FirstName = FirstName;
				student.Note = Note;
				student.Group = selectedGroup;
			}
			else
			{
				var student = new Student
				{
					FirstName = firstName, LastName = lastName, MiddleName = middleName, Note = note,
					Group = SelectedGroup
				};
				context.Students.Add(student);
			}

			context.SaveChanges();
			Exit();
		}

		private void Exit()
		{
			ManagerPage.ChangePage("Work", new ManageGroupsPage {DataContext = new ManageGroupViewModel(context)});
		}

		private void AddGroup()
		{
			var addGroupWindow = new AddGroupWindow();
			addGroupWindow.DataContext = new AddGroupViewModel(context) {CloseAction = addGroupWindow.Close};
			addGroupWindow.ShowDialog();
			GroupsList = new ObservableCollection<Group>(context.Groups.ToList());
		}
	}
}