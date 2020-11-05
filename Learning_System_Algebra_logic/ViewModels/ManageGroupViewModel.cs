using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Pages;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class ManageGroupViewModel : BaseViewModel
	{
		private readonly ModelDataContext _context;
		private ICommand addGroupCommand;
		private ICommand addStudentCommand;
		private ICommand deleteGroupCommand;
		private ICommand deleteStudentsCommand;
		private ICommand editStudentCommand;
		private string filterFirstName = "";
		private string filterLastName = "";
		private string filterMiddleName = "";
		private ObservableCollection<string> groupsFilterList;
		private ICommand importFromExcelCommand;
		private ICommand reportListGroupCommand;
		private ICommand reportStudentCommand;
		private string selectedGroupName = "";
		private StudentViewModel selectedStudent;
		private string selectedYear = "";
		private ICommand selectYearFilterCommand;
		private ObservableCollection<StudentViewModel> students;
		private ObservableCollection<string> yearsCreateFilterList;

		public ManageGroupViewModel(ModelDataContext context)
		{
			_context = context;
			Students = new ObservableCollection<StudentViewModel>();
			YearsCreateFilterList = new ObservableCollection<string>();
			GroupsFilterList = new ObservableCollection<string>();
			foreach (var student in _context.Students.Include(st => st.Group.Students).Include(s => s.CodeWorks)
				.ToList()) Students.Add(new StudentViewModel(student));
			var years = from gr in _context.Groups
				select gr.DateCreate;
			years = years.Distinct();
			YearsCreateFilterList.Add("");
			foreach (var year in years.ToArray()) YearsCreateFilterList.Add(year.ToString());
			var groups = from gr in _context.Groups
				select gr.Name;
			groups = groups.Distinct();
			GroupsFilterList.Add("");
			foreach (var group in groups.ToArray()) GroupsFilterList.Add(group);
		}


		public ObservableCollection<string> GroupsFilterList
		{
			get => groupsFilterList;
			set
			{
				if (groupsFilterList == value)
					return;

				groupsFilterList = value;
				OnPropertyChanged("GroupsFilterList");
			}
		}

		public ObservableCollection<string> YearsCreateFilterList
		{
			get => yearsCreateFilterList;
			set
			{
				if (yearsCreateFilterList == value)
					return;

				yearsCreateFilterList = value;
				OnPropertyChanged("YearsCreateFilterList");
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

		public string FilterFirstName
		{
			get => filterFirstName;
			set
			{
				if (filterFirstName == value)
					return;

				filterFirstName = value;
				OnPropertyChanged("FilterFirstName");
				Filter();
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

		public string FilterMiddleName
		{
			get => filterMiddleName;
			set
			{
				if (filterMiddleName == value)
					return;

				filterMiddleName = value;
				OnPropertyChanged("FilterMiddleName");
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

		public string SelectedYear
		{
			get => selectedYear;
			set
			{
				if (selectedYear == value)
					return;

				selectedYear = value;
				OnPropertyChanged("SelectedYear");
				Filter();
			}
		}

		public StudentViewModel SelectedStudent
		{
			get => selectedStudent;
			set
			{
				if (selectedStudent == value)
					return;

				selectedStudent = value;
				OnPropertyChanged("SelectedStudent");
			}
		}

		public ICommand SelectYearFilterCommand
		{
			get
			{
				selectYearFilterCommand = selectYearFilterCommand ?? new Command(SelectYearFilter);
				return selectYearFilterCommand;
			}
		}

		public ICommand AddStudentCommand
		{
			get
			{
				addStudentCommand = addStudentCommand ?? new Command(AddStudent);
				return addStudentCommand;
			}
		}

		public ICommand EditStudentCommand
		{
			get
			{
				editStudentCommand = editStudentCommand ?? new Command(EditStudent);
				return editStudentCommand;
			}
		}

		public ICommand DeleteStudentsCommand
		{
			get
			{
				deleteStudentsCommand = deleteStudentsCommand ?? new Command(DeleteStudents);
				return deleteStudentsCommand;
			}
		}

		public ICommand DeleteGroupCommand
		{
			get
			{
				deleteGroupCommand = deleteGroupCommand ?? new Command(DeleteGroup);
				return deleteGroupCommand;
			}
		}

		public ICommand ImportFromExcelCommand
		{
			get
			{
				importFromExcelCommand = importFromExcelCommand ?? new Command(ImportStudentFromExcel);
				return importFromExcelCommand;
			}
		}

		private void ImportStudentFromExcel()
		{
			ManagerPage.ChangePage("Work",
				new ImportFromExcelPage {DataContext = new ImportFromExcelViewModel(_context)});
		}

		private void DeleteGroup()
		{
			ManagerPage.ChangePage("Work", new DeleteGroupPage {DataContext = new DeleteGroupViewModel(_context)});
		}

		private void AddStudent()
		{
			ManagerPage.ChangePage("Work", new AddStudentPage {DataContext = new AddStudentViewModel(_context)});
		}

		private void DeleteStudents()
		{
			
			if (SelectedStudent.Student.Group.Students.Count == 1)
				_context.Groups.Remove(SelectedStudent.Student.Group);
			else
				_context.Students.Remove(SelectedStudent.Student);
			Students.Remove(SelectedStudent);
			_context.SaveChanges();
		}

		private void Filter()
		{
			var firstName = FilterFirstName;
			var lastName = FilterLastName;
			var middleName = FilterMiddleName;
			var groupName = SelectedGroupName;
			var year = SelectedYear;
			Students.Clear();
			var filterStudents =
				from stud in _context.Students.Include(gt => gt.Group.Students).Include(s => s.CodeWorks).ToList()
				where stud.FirstName.Contains(firstName) && stud.MiddleName.Contains(middleName) &&
				      stud.LastName.Contains(lastName) && stud.Group.Name.Contains(groupName) &&
				      stud.Group.DateCreate.ToString().Contains(year)
				select stud;
			foreach (var student in filterStudents) Students.Add(new StudentViewModel(student));
		}

		private void SelectYearFilter()
		{
			Filter();
		}

		private void EditStudent()
		{
			if (SelectedStudent != null)
				ManagerPage.ChangePage("Work",
					new AddStudentPage {DataContext = new AddStudentViewModel(_context, SelectedStudent)});
		}
	}
}