using System.IO;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class TheoryViewModel : BaseViewModel
	{
		private readonly XpsDocument xpsDocument;
		private IDocumentPaginatorSource theoryDocument;

		public TheoryViewModel()
		{
			xpsDocument = new XpsDocument("Resources/Theory.xps", FileAccess.Read);
			TheoryDocument = xpsDocument.GetFixedDocumentSequence();
		}

		public IDocumentPaginatorSource TheoryDocument
		{
			get => theoryDocument;
			set
			{
				theoryDocument = value;
				OnPropertyChanged("TheoryDocument");
			}
		}
	}
}