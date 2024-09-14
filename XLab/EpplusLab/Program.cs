// See https://aka.ms/new-console-template for more information

using OfficeOpenXml;

Student student = new Student()
{
    BirthDate = new DateTime(1999, 9, 9,9,9,9).AddMilliseconds(999),
    Height = 177.7,
    IsDeveloper = true,
    Name = "Jack"
};

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
string fileName = "D:/students.xlsx";
using ExcelPackage excel = new ExcelPackage(fileName);
ExcelWorksheet sheet = excel.Workbook.Worksheets.Add("sheet1");

/* title row */
sheet.Cells[1, 1].Value = "Name";
sheet.Cells[1, 2].Value = "BirthDate";
sheet.Cells[1, 3].Value = "Height";
sheet.Cells[1, 4].Value = "IsDeveloper";

/*specify columns format*/
sheet.Column(2).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss.000"; // datetime
sheet.Column(3).Style.Numberformat.Format = "0.00"; // 保留2位小数

int startRow = 2;

sheet.Cells[startRow, 1].Value = student.Name; // 字符串
sheet.Cells[startRow, 2].Value = student.BirthDate; // 时间
sheet.Cells[startRow, 3].Value = student.Height;  // 数字
sheet.Cells[startRow, 4].Value = student.IsDeveloper ? 'Y' : 'N';

// 列宽自适应
sheet.Cells.AutoFitColumns();

excel.Save();

public class Student
{
    public DateTime BirthDate { get; set; }
    public bool IsDeveloper { get; set; }
    public string? Name { get; set; }
    public double Height { get; set; }
}