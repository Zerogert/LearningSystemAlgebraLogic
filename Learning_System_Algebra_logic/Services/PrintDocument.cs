using System.Windows.Controls;
using System.Windows.Documents;

namespace Learning_System_Algebra_logic.Services
{
	internal class PrintDocument
	{
		public static void Print(FlowDocument document)
		{
			var printDialog = new PrintDialog();
			var print = printDialog.ShowDialog();
			if (print == true)
			{
				var paginator = ((IDocumentPaginatorSource) document).DocumentPaginator;
				printDialog.PrintDocument(paginator, "Печать документа");
			}
		}
	}
}