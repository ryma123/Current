
using System.Collections.Generic;
using DataAccess;
using System;
using System.Data;
using System.Text.RegularExpressions;
namespace BusinessLayer
{
    public class Business
    {
       
        DashboardDataAccess dashboardDataAccess;
        
        public Business()
        {
            dashboardDataAccess = new DashboardDataAccess();
        }

        public List<string> GetAllProduct()
        {
            return dashboardDataAccess.GetAllProduct();
        }

        public List<string> GetAllVersion(string SelectedProduct)
        {
            return dashboardDataAccess.GetAllVersions(SelectedProduct);
        }

        public List<string> GetPercent(string SelectedVersion)
        {
           return dashboardDataAccess.PercentageCalculator(SelectedVersion);          
            
        }
        public List<string> GetGrade(string SelectedVersion)
        {
           var grades = new List<string>();
            foreach (var percentage in GetPercent(SelectedVersion))
            {
               var resultString = Regex.Match(percentage, @"\d+").Value;


                if (!(percentage.StartsWith("No Data")))
                {
                    if (Convert.ToInt32(resultString) >= 0 && Convert.ToInt32(resultString) < 5)
                    {
                        grades.Add("A");
                    }
                    else if (Convert.ToInt32(resultString) >= 5 && Convert.ToInt32(resultString) < 15)
                    {
                        grades.Add("B");

                    }
                    else if (Convert.ToInt32(resultString) >= 15 && Convert.ToInt32(resultString) < 30)
                    {
                        grades.Add("C");

                    }
                    else if (Convert.ToInt32(resultString) >= 30 && Convert.ToInt32(resultString) < 40)
                    {
                        grades.Add("D");

                    }
                    else if (Convert.ToInt32(resultString) >= 40 && Convert.ToInt32(resultString) <= 100)
                    {
                        grades.Add("E");
                    }
                }
                else
                {
                    grades.Add("No Data");
                }

            }
                return grades;
            }



      

        public DataTable GetTrendlineInformation()
        {
            
              DataTable dataTable = new DataTable();
              dataTable.Columns.Add("Product", typeof(String));
              dataTable.Columns.Add("Version", typeof(String));
              dataTable.Columns.Add("KpiType", typeof(String));
              dataTable.Columns.Add("Percentage", typeof(String));
              foreach (var product in GetAllProduct())
                {
                var b = GetAllVersion(product);
                foreach (var version in b )
                    {
                    var a = GetPercent(version);
                      foreach (var percentage in a)
                        {
                        
                        Match result = new Regex(@"([a-zA-Z]+)(\d+)").Match(percentage);
                        //Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                        //  Match result = re.Match(percentage);
                        //   alphabetPart = result.Groups[1].Value;
                          dataTable.Rows.Add(product,version, result.Groups[1].Value, result.Groups[2].Value);
                         }                  
                    }           
                }
            return dataTable;
        }

    }
}
