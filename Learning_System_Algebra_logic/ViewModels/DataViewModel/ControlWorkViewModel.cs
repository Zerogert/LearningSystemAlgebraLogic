using System;
using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels.DataViewModel
{
	public class ControlWorkViewModel : BaseViewModel
	{
		public ControlWorkViewModel(CodeWork codeWork)
		{
			CodeWork = codeWork;
			LastName = codeWork?.Student.LastName;
			FirstName = codeWork?.Student.FirstName;
			Group = codeWork?.Student.Group.ToString();
			WorkName = codeWork?.VariantWork.Work.Name;
			State = codeWork?.State;
			Result = codeWork?.Result;
			DateCreate = codeWork?.DateCreate.ToShortDateString();
			if (codeWork?.DateComplete != DateTime.MinValue) DateEnd = codeWork?.DateComplete.ToShortDateString();
			VariantName = codeWork?.VariantWork.Variant;
			Code = codeWork?.Code;
		}

		public CodeWork CodeWork { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Group { get; set; }
		public string WorkName { get; set; }
		public string State { get; set; }
		public string Result { get; set; }
		public string DateCreate { get; set; }
		public string DateEnd { get; set; }
		public string VariantName { get; set; }
		public string Code { get; set; }
	}
}