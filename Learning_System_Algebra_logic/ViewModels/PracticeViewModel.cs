using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using Learning_System_Algebra_logic.Data;
using Learning_System_Algebra_logic.Enums;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class PracticeViewModel : Logical_cxem.ViewModels.MainViewModel
	{
		private readonly ModelDataContext context;
		private ObservableCollection<PracticeModel> practiceWorkList;
		private PracticeModel selectedWorkItem;

		public PracticeViewModel(ref Canvas canvas, ModelDataContext context) : base(ref canvas)
		{
			this.context = context;
			var practiceWorkList = context.VariantWorks.Include(v => v.Work)
				.Where(variant => variant.Work.TypeWork == TypeWork.Practice).ToList();
			PracticeWorkList = new ObservableCollection<PracticeModel>();
			foreach (var variant in practiceWorkList) PracticeWorkList.Add(new PracticeModel(variant));
		}

		public ObservableCollection<PracticeModel> PracticeWorkList
		{
			get => practiceWorkList;
			set
			{
				if (practiceWorkList == value)
					return;

				practiceWorkList = value;
				OnPropertyChanged();
			}
		}


		public PracticeModel SelectedWorkItem
		{
			get => selectedWorkItem;
			set
			{
				if (selectedWorkItem == value)
					return;
				selectedWorkItem = value;
				SelectedWork();
				OnPropertyChanged();
			}
		}

		private void SelectedWork()
		{
			MakeControl(SelectedWorkItem.variantWork.Attachment);
		}
	}
}