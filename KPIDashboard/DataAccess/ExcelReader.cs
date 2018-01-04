using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Configuration;
using System.Data;


namespace DataAccess

{
    public class ExcelReader : IReader
    {
        static private DataTable dataTable = new DataTable();
      

        public void ReadExcel()
        {
            string filepath = ConfigurationManager.AppSettings["FilePath"].ToString();
                //open the excel using openxml sdk  
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filepath, false))
            {   Sheet sheet = document.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                //Get the Worksheet instance.
                Worksheet worksheet = (document.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                //Fill DataTable.
                FillDataTable(rows, document);
              
            
             }
        }
        public object Read()
        {
            ReadExcel();
            return dataTable;
        }

        public void FillDataTable(IEnumerable<Row> rows, SpreadsheetDocument document)
        {
            dataTable = new DataTable();
            foreach (Row row in rows)
            {
                //Use the first row to add columns to DataTable.
                if (row.RowIndex.Value == 10)
                {
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        dataTable.Columns.Add(GetValue(document, cell));
                    }
                }
                if (row.RowIndex.Value >10)
                {
                    //Add rows to DataTable.
                    dataTable.Rows.Add();
                    int i = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        dataTable.Rows[dataTable.Rows.Count - 1][i] = GetValue(document, cell);
                        i++;
                    }
                }
            }
        }

        private string GetValue(SpreadsheetDocument document, Cell cell)
        {

            if (cell.CellValue != null)
            {
                string value = cell.CellValue.Text;

                if (value == "Effort")
                {


                }

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    string s = document.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                    return s;
                }
                return value;
            }
            else
            { return ""; }
        }
      
     
    }
}
