using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Enums;
using Learning_System_Algebra_logic.Pages;
using Microsoft.Win32;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class AddWorkViewModel : BaseViewModel
	{
		private readonly ModelDataContext context;

		private readonly Work work;
		private ICommand addVariantCommand;

		private ICommand addWorkCommand;
		private bool allowAddWork;

		private ICommand cancelCommand;
		private int count = 1;

		private ICommand deleteVariantCommand;

		private string nameWork = "";

		private string selectedTypeWork;

		private VariantWork selectedVariant;

		private ObservableCollection<string> typeWorkList;

		private ObservableCollection<VariantWork> variantsList;

		public AddWorkViewModel(ModelDataContext context)
		{
			this.context = context;
			TypeWorkList = new ObservableCollection<string> {"", TypeWork.Control, TypeWork.Practice};
			VariantsList = new ObservableCollection<VariantWork>();
			SelectedTypeWork = typeWorkList[0];
		}

		public AddWorkViewModel(ModelDataContext context, Work work) : this(context)
		{
			this.work = work;
			SelectedTypeWork = work.TypeWork;
			NameWork = work.Name;
			VariantsList = new ObservableCollection<VariantWork>(work.VariantWorks.ToList());
		}

		public ObservableCollection<string> TypeWorkList
		{
			get => typeWorkList;
			set
			{
				if (typeWorkList == value)
					return;

				typeWorkList = value;
				OnPropertyChanged("TypeWorkList");
			}
		}

		public ObservableCollection<VariantWork> VariantsList
		{
			get => variantsList;
			set
			{
				if (variantsList == value)
					return;
				variantsList = value;
				AllowAddWork = CheckAllowAddWork();
				OnPropertyChanged("VariantsList");
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
				OnPropertyChanged("SelectedTypeWork");
			}
		}

		public bool AllowAddWork
		{
			get => allowAddWork;
			set
			{
				if (allowAddWork == value)
					return;

				allowAddWork = value;
				OnPropertyChanged("AllowAddWork");
			}
		}

		public VariantWork SelectedVariant
		{
			get => selectedVariant;
			set
			{
				if (selectedVariant == value)
					return;
				selectedVariant = value;
				OnPropertyChanged("SelectedVariant");
			}
		}

		public string NameWork
		{
			get => nameWork;
			set
			{
				if (nameWork == value)
					return;
				nameWork = value;
				AllowAddWork = CheckAllowAddWork();
				OnPropertyChanged("NameWork");
			}
		}

		public ICommand AddVariantCommand
		{
			get
			{
				addVariantCommand = addVariantCommand ?? new Command(AddVariant);
				return addVariantCommand;
			}
		}

		public ICommand DeleteVariantCommand
		{
			get
			{
				deleteVariantCommand = deleteVariantCommand ?? new Command(DeleteVariant);
				return deleteVariantCommand;
			}
		}

		public ICommand AddWorkCommand
		{
			get
			{
				addWorkCommand = addWorkCommand ?? new Command(AddWork);
				return addWorkCommand;
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

		private void AddVariant()
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "Logical cxem files (*.lcx)|*.lcx";
			openFileDialog.ShowDialog();
			foreach (var fileName in openFileDialog.FileNames)
			{
				var fileInfo = new FileInfo(fileName);
				var file = File.ReadAllBytes(fileName);

				var variant = new VariantWork
					{Attachment = LoadFile(fileName), Variant = $"Вариант {count++} ({fileInfo.Name})"};
				variantsList.Add(variant);
				if (work != null) work.VariantWorks.Add(variant);
			}

			AllowAddWork = CheckAllowAddWork();
		}

		public static string LoadFile(string name)
		{
			using (var fs = new StreamReader(name))
			{
				try
				{
					return fs.ReadToEnd();
				}
				catch (Exception e)
				{
					MessageBox.Show("Файл поврежден", "Повреждение файла",
						MessageBoxButton.OK, MessageBoxImage.Error);
					throw;
				}
			}
		}

		private bool CheckAllowAddWork()
		{
			if (NameWork.Length == 0) return false;

			if (VariantsList.Count == 0) return false;

			return true;
		}

		private void AddWork() //Добавляет варианты т.е. работу  в БД и закрывает форму
		{
			if (this.work == null)
			{
				var work = new Work {Name = NameWork, TypeWork = SelectedTypeWork};
				context.Works.Add(work);
				foreach (var variant in variantsList)
				{
					variant.Work = work;
					context.VariantWorks.Add(variant);
				}
			}
			else
			{
				work.Name = NameWork;
				work.TypeWork = selectedTypeWork;
			}

			context.SaveChanges();
			ManagerPage.ChangePage("Work", new ManageWorksPage {DataContext = new ManageWorksViewModel(context)});
		}

		private void Cancel()
		{
			ManagerPage.ChangePage("Work", new ManageWorksPage {DataContext = new ManageWorksViewModel(context)});
		}

		private void DeleteVariant()
		{
			if (SelectedVariant != null)
			{
				if (work != null) context.VariantWorks.Remove(SelectedVariant);
				VariantsList.Remove(SelectedVariant);
			}

			AllowAddWork = CheckAllowAddWork();
		}
	}
}