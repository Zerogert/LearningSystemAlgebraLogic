using System.Collections.Generic;
using ClosedXML.Excel;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.Services
{
	public class ExportToExcel
	{
		public static void ExportCodesToExcelFile(ICollection<ControlWorkViewModel> codes, string path)
		{
			var workbook = new XLWorkbook();
			var worksheet = workbook.Worksheets.Add("Лист1");
			//создадим заголовки у столбцов
			worksheet.Cell("A" + 1).Value = "Студент";
			worksheet.Cell("B" + 1).Value = "Код доступа";
			worksheet.Cell("C" + 1).Value = "Группа";
			worksheet.Cell("D" + 1).Value = "Работа";
			worksheet.Cell("E" + 1).Value = "Вариант";
			worksheet.Cell("F" + 1).Value = "Состяние";
			worksheet.Cell("G" + 1).Value = "Оценка";
			worksheet.Cell("H" + 1).Value = "Дата создания";
			worksheet.Cell("I" + 1).Value = "Дата выполнения";
			var row = 2;
			foreach (var code in codes)
			{
				worksheet.Cell("A" + row).Value = $"{code.LastName} {code.FirstName}";
				worksheet.Cell("B" + row).Value = code.Code;
				worksheet.Cell("C" + row).Value = code.Group;
				worksheet.Cell("D" + row).Value = code.WorkName;
				worksheet.Cell("E" + row).Value = code.VariantName;
				worksheet.Cell("F" + row).Value = code.State;
				worksheet.Cell("G" + row).Value = code.Result;
				worksheet.Cell("H" + row).Value = code.DateCreate;
				worksheet.Cell("I" + row).Value = code.DateEnd;
				row++;
			}

			worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому
			workbook.SaveAs(path);
		}
	}
}