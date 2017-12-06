using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using System.Data;


namespace DataAccess

{
    public class ExcelReader : IReader
    {
        static private DataTable dataTable = new DataTable();

        //static private Excel.Application excelApp;
        //static private Excel.Workbook workBook;
        //static private Excel.Worksheet worksheet;
        //static private Excel.Range worksheetUsedRange;
        static private List<string> rowItemsList = new List<string>();
        static private List<string> columnsNameList = new List<string>();
      
        public ExcelReader()
        {
            //excelApp = new Excel.Application();
            //excelApp.Visible = true;
            //excelApp.Workbooks.Open(@"C:\Users\rkhan\Documents\GitHub\Current\KPIDashboard\WebApplication1\App_Data\test1.xlsx");
            //workBook = excelApp.Workbooks["test1.xlsx"];
            //worksheet = workBook.Sheets[1];
            //worksheetUsedRange = worksheet.UsedRange;
            //workBook.AfterSave += Workbook_AfterSave;

        }

        public void ReadExcel()
        {

            string filepath = @"C:\Users\rkhan\Documents\GitHub\Current\KPIDashboard\WebApplication1\App_Data\test1.xlsx";
            //open the excel using openxml sdk  
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, false))
            {
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                //Get the Worksheet instance.
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                //Fill DataTable.
                FillDataTable(rows, doc);

            }
        }

        public void FillDataTable(IEnumerable<Row> rows, SpreadsheetDocument doc)
        {
            dataTable = new DataTable();
            foreach (Row row in rows)
            {
                //Use the first row to add columns to DataTable.
                if (row.RowIndex.Value == 1)
                {
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        dataTable.Columns.Add(GetValue(doc, cell));
                    }
                }
                else
                {
                    //Add rows to DataTable.
                    dataTable.Rows.Add();
                    int i = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        dataTable.Rows[dataTable.Rows.Count - 1][i] = GetValue(doc, cell);
                        i++;
                    }
                }
            }
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
      
        public object Read()
        {
            ReadExcel();
          
            return dataTable;
        }
        















public delegate void NotifyDataAccess(ExcelReader m, EventArgs e); 
        public NotifyDataAccess notifyDataAccess;    
        public EventArgs e = null;

        private void Workbook_AfterSave(bool success)
        {
            //rowItemsList = new List<string>();
            //columnsNameList = new List<string>();
            //dataTable = new DataTable();
            //worksheetUsedRange = worksheet.UsedRange;
            //notifyDataAccess?.Invoke(this, e); 
        }




    }
}
