
using BDA.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ExcelService
{
	

	public List<Customers> ImportExcelData(string filePath)
	{
		var dataList = new List<Customers>();

		using (var package = new ExcelPackage(new FileInfo(filePath)))
		{
			var worksheet = package.Workbook.Worksheets.FirstOrDefault();
			if (worksheet != null)
			{
				int rowCount = worksheet.Dimension.Rows;
				for (int row = 1; row <= rowCount; row++) // Skip header row
				{
					var nameCellValue = worksheet.Cells[row, 1].Text;
					if (string.IsNullOrWhiteSpace(nameCellValue))
					{
						continue; // Skip rows with no Name
					}
					var customer = new Customers
					{
						//Id = int.TryParse(worksheet.Cells[row, 1].Text, out int id) ? id : 0,
						Name = nameCellValue,
						//Name = worksheet.Cells[row, 2].Text,
						Surname = worksheet.Cells[row, 2].Text,
						PhoneNumber = worksheet.Cells[row, 3].Text,
						Email = worksheet.Cells[row, 4].Text,
						Company = worksheet.Cells[row, 5].Text,
						Department = worksheet.Cells[row, 6].Text,
						Position = worksheet.Cells[row, 7].Text,
						//CreatedByUserId = user({ IdentityServiceCollectionExtensions[5] }), // Co

					    CreatedAt = DateTime.Now, // Set this appropriately if needed
						
					};

					dataList.Add(customer);
				}
			}
		}
		return dataList;
	}
}