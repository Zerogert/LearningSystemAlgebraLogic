using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.Services
{
	internal class GenerateDocument
	{
		public static FlowDocument GenerateDocumentCodes(ICollection<ControlWorkViewModel> codes)
		{
			var flowDocument = new FlowDocument();
			var codeTable = new Table();
			flowDocument.Blocks.Add(codeTable);
			codeTable.Columns.Add(new TableColumn());
			codeTable.Columns.Add(new TableColumn());
			codeTable.RowGroups.Add(new TableRowGroup());
			codeTable.RowGroups[0].Rows.Add(new TableRow());
			var currentRow = codeTable.RowGroups[0].Rows[0];
			currentRow.Background = Brushes.BurlyWood;
			currentRow.FontSize = 40;
			currentRow.FontWeight = FontWeights.Bold;
			currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Студент"))));
			currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Код доступа"))));
			currentRow.Cells[0].TextAlignment = TextAlignment.Justify;
			currentRow.Cells[1].TextAlignment = TextAlignment.Justify;
			foreach (var code in codes)
			{
				currentRow = new TableRow();
				codeTable.RowGroups[0].Rows.Add(currentRow);
				currentRow.FontSize = 18;
				currentRow.Cells.Add(new TableCell(new Paragraph(new Run($"{code.LastName} {code.FirstName}"))));
				currentRow.Cells.Add(new TableCell(new Paragraph(new Run($"{code.Code}"))));
			}

			return flowDocument;
		}
	}
}