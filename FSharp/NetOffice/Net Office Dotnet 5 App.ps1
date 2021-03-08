rm app -r
mkdir app
cd app
dotnet new winforms -lang "C#"
dotnet add package NetOfficeFw.Core
dotnet add package NetOfficeFw.Excel
rm *.cs 

"using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

using var application = new Application();
application.Visible = true;
var workbook = application.Workbooks.Add();
var worksheet = (Worksheet)workbook.Sheets[1];
worksheet.Cells[1,1].Value2 = 1;
worksheet.Cells[1,1].Interior.Color = 65535;
worksheet.ListObjects.Add(XlSourceType.xlSourceRange, worksheet.Cells[1, 2]);
" > Program.cs

dotnet run