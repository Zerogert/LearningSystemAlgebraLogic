using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels.DataViewModel
{
	internal class WorkViewModel
	{
		public WorkViewModel(Work work)
		{
			Id = work.WorkId;
			TypeWork = work.TypeWork;
			NameWork = work.Name;
			Work = work;
		}

		public Work Work { get; set; }
		public string TypeWork { get; set; }
		public string NameWork { get; set; }
		public string Id { get; set; }
	}
}