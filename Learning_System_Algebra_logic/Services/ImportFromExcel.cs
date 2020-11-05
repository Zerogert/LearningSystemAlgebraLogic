using System.Collections.Generic;
using ClosedXML.Excel;
using Learning_System_Algebra_logic.ViewModels.DataViewModel;

namespace Learning_System_Algebra_logic.Services
{
	internal class ImportFromExcel
	{
		public static IEnumerable<StudentViewModel> ImportStudentsFromExcel(string path)
		{
			using (var workbook = new XLWorkbook(path))
			{
				var worksheet = workbook.Worksheets.Worksheet(1);
				var row = 2;
				while (CheckRow(worksheet, row))
				{
					var student = new StudentViewModel
					{
						LastName = worksheet.Cell(row, 1).GetString(),
						FirstName = worksheet.Cell(row, 2).GetString(),
						MiddleName = worksheet.Cell(row, 3).GetString(),
						Note = worksheet.Cell(row, 4).GetString()
					};
					row++;
					yield return student;
				}
			}
		}


		private static bool CheckRow(IXLWorksheet worksheet, int row)
		{
			if (worksheet.Cell(row, 1).GetString() == string.Empty) //Чек фамилия
				return false;
			if (worksheet.Cell(row, 2).GetString() == string.Empty) //Чек имя
				return false;
			if (worksheet.Cell(row, 3).GetString() == string.Empty)
				return false;
			return true;
		}
	}
}