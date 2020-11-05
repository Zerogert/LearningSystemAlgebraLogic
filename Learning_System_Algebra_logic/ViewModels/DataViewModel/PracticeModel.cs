using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels.DataViewModel
{
	internal class PracticeModel : BaseViewModel
	{
		public VariantWork variantWork;

		public PracticeModel(VariantWork variant)
		{
			variantWork = variant;
		}

		public override string ToString()
		{
			return variantWork.Work.Name + ' ' + variantWork.Variant;
		}
	}
}