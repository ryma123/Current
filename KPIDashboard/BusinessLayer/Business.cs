
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
            return dashboardDataAccess.UpdateProductComboBox();
        }

        public List<string> GetAllVersion(string SelectedProduct)
        {
            return dashboardDataAccess.UpdateVersionComboBox(SelectedProduct);
        }

        public List<string> GetPercent(string SelectedVersion)
        {
            return dashboardDataAccess.PercentageCalculator(SelectedVersion);
        }
        public DataTable GraphInfo()
        {
              DataTable dataTable = new DataTable();
              dataTable.Columns.Add("Product", typeof(String));
              dataTable.Columns.Add("Version", typeof(String));
              dataTable.Columns.Add("KpiType", typeof(String));
              dataTable.Columns.Add("Percentage", typeof(String));
              foreach (var product in GetAllProduct())
                {
                 var temporaryVersionList= GetAllVersion(product);
                   foreach(var version in temporaryVersionList)
                   {
                     foreach(var percentage in GetPercent(version))
                        {
                          Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
                          Match result = re.Match(percentage);
                          string alphaPart = result.Groups[1].Value;
                          dataTable.Rows.Add(product,version, result.Groups[1].Value, result.Groups[2].Value);
                         }                  
                    }           
                }
            return dataTable;
        }

    }
}
