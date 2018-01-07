using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Configuration;
using System.Data;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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
                WorkbookPart wbPart = document.WorkbookPart;
                Worksheet worksheet = (document.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                WorksheetPart wsPart =(WorksheetPart)(wbPart.GetPartById(sheet.Id.Value));

                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                //Fill DataTable.
                FillDataTable(rows, document,wbPart);
                
            
             }
        }
        public object Read()
        {
            ReadExcel();
            var numberOfRows = dataTable.Rows.Count;
            dataTable.AcceptChanges();
            for (int row = 0; row < numberOfRows; row++)
            {
               var visibility = int.Parse(dataTable.Rows[row]["Visible"].ToString());
               if (visibility == 0)
               { dataTable.Rows[row].Delete(); }
               
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public void FillDataTable(IEnumerable<Row> rows, SpreadsheetDocument document, WorkbookPart workbookpart)
        {
            dataTable = new DataTable();
            foreach (Row row in rows)
            {
                //Use the first row to add columns to DataTable.
                if (row.RowIndex.Value == 10)
                {
                    IEnumerable<Cell> cells = GetEnumerator(row);

                    foreach (Cell cell in cells)
                    {
                        dataTable.Columns.Add(GetValue(document, cell, workbookpart));
                    }
                }
                if (row.RowIndex.Value >10)
                {
                    //Add rows to DataTable.
                    dataTable.Rows.Add();
                    int i = 0;
                    IEnumerable<Cell> cells = GetEnumerator(row);
                    foreach (Cell cell in cells)
                    { 
                        dataTable.Rows[dataTable.Rows.Count - 1][i] = GetValue(document, cell, workbookpart);

                        i++;
                    }
                }
            }
        }

        private string GetValue(SpreadsheetDocument document, Cell cell, WorkbookPart workbookPart)
        {
            if (cell.CellValue != null)
            {
                string value = cell.CellValue.Text;



                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString && value != "")
                {
                    string s = document.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(Int32.Parse(value)).InnerText;
                    return s;
                }
               
                return value;

            }
            else
            { return "0"; }
        }
        public IEnumerable<Cell> GetEnumerator(Row row)
        {
            int currentCount = 0;
           
                // row is a class level variable representing the current
                // DocumentFormat.OpenXml.Spreadsheet.Row
                foreach (DocumentFormat.OpenXml.Spreadsheet.Cell cell in
                row.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                {
                    string columnName = GetColumnName(cell.CellReference);

                    int currentColumnIndex = ConvertColumnNameToNumber(columnName);

                    for (; currentCount < currentColumnIndex; currentCount++)
                    {
                        yield return new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    }

                    yield return cell;
                    currentCount++;
                }
            } 


        public static string GetColumnName(string cellReference)
        {
            // Match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }


        public static int ConvertColumnNameToNumber(string columnName)
        {
            Regex alpha = new Regex("^[A-Z]+$");
            if (!alpha.IsMatch(columnName)) throw new ArgumentException();

            char[] colLetters = columnName.ToCharArray();
            Array.Reverse(colLetters);

            int convertedValue = 0;
            for (int i = 0; i < colLetters.Length; i++)
            {
                char letter = colLetters[i];
                int current = i == 0 ? letter - 65 : letter - 64; // ASCII 'A' = 65
                convertedValue += current * (int)Math.Pow(26, i);
            }

            return convertedValue;
        }

        

    }
}
