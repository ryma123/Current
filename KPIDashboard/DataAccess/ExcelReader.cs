using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
namespace DataAccess

{
    public class ExcelReader : IReader
    {
        static private DataTable dataTable = new DataTable();

        static private Excel.Application excelApp;
        static private Excel.Workbook workBook;
        static private Excel.Worksheet worksheet;
        static private Excel.Range worksheetUsedRange;
        static private List<string> rowItemsList = new List<string>();
        static private List<string> columnsNameList = new List<string>();

        public ExcelReader()
        {
            excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Open(@"C:\SB\TEM_Apps  KPI_Dashboard\KPIDashboard\WebApplication1\App_Data\test1.xlsx");
            workBook = excelApp.Workbooks["test1.xlsx"];
            worksheet = workBook.Sheets[1];
            worksheetUsedRange = worksheet.UsedRange;
            workBook.AfterSave += Workbook_AfterSave;

        }

        public void ReadExcel()
        {
            rowItemsList = new List<string>();
            columnsNameList = new List<string>();

            int FirstRow = 1;
            int FirstColumn = 1;
            for (int rowLoop = FirstRow; rowLoop <= worksheetUsedRange.Rows.Count; rowLoop++)
            {
                for (int columnLoop = FirstColumn; columnLoop <= worksheetUsedRange.Columns.Count; columnLoop++)
                {
                    if (rowLoop == 1)
                    {
                        if (worksheet.Cells[rowLoop, columnLoop] != null && worksheet.Cells[rowLoop, columnLoop].Value2 != null)
                        {
                            columnsNameList.Add(worksheet.Cells[rowLoop, columnLoop].Value2.ToString());
                        }
                    }
                    else
                    {
                        if (worksheet.Cells[rowLoop, columnLoop] != null && worksheet.Cells[rowLoop, columnLoop].Value2 != null)
                        {
                            rowItemsList.Add(worksheet.Cells[rowLoop, columnLoop].Value2.ToString());
                        }
                    }
                }
            }
        }
        public void FillColumnsInDataTable()
        {
            dataTable = new DataTable();
            foreach (var columnName in columnsNameList)
            {
                dataTable.Columns.Add(columnName, typeof(string));
            }
        }

        public void FillRowsInDataTable()
        {
            int totalColumnInDataTable = dataTable.Columns.Count;
            int counter = rowItemsList.Count / totalColumnInDataTable;
            int InnerIiteration = 0;

            for (int i = 1; i <= counter; i++)
            {

                int columns = 0;
                DataRow newrow = dataTable.NewRow();
                while (InnerIiteration < (i * totalColumnInDataTable))
                {
                    string value = rowItemsList[InnerIiteration];
                    newrow[columns] = value;
                    columns++;
                    InnerIiteration++;
                }
                dataTable.Rows.Add(newrow);
            }
        }
        public object Read()
        {
            ReadExcel();
            FillColumnsInDataTable();
            FillRowsInDataTable();
            return dataTable;
        }
        private void Workbook_AfterSave(bool success)
        {
            rowItemsList = new List<string>();
            columnsNameList = new List<string>();
            dataTable = new DataTable();
            worksheetUsedRange = worksheet.UsedRange;
            Read();
        }
    }
}
