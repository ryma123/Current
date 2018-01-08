using System.Collections.Generic;
using System.Data;
using DataAccess.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DataAccess
{
    public class DashboardDataAccess
    {
 
        public DashboardDataAccess()
        {
            IReader   reader = new ExcelReader();
            DataTable dataTable = (DataTable)reader.Read();
            AddToDatabase(dataTable);
          
        }




        public void AddToDatabase(DataTable dataTable)
        {
            var numberOfRows = dataTable.Rows.Count;
            for (int row = 0; row < numberOfRows; row++)
            {
                var productName = dataTable.Rows[row]["Product"];
                var releaseId = dataTable.Rows[row]["Release"];
                var location = dataTable.Rows[row]["Site"];
                var freezePeriod =dataTable.Rows[row]["Freeze period (WORK DAYS)"];
                var totalFTE = dataTable.Rows[row]["Total FTEs"];
                var effort =dataTable.Rows[row]["Effort (WORK DAYS)"];
                var codeFreezePercentage = dataTable.Rows[row]["% Effort"];

                var plannedTest = dataTable.Rows[row]["planned"];
                var executedTest = dataTable.Rows[row]["executed"];
                var notExecutedTestPercentage = dataTable.Rows[row]["% not executed"];

                var passedTest = dataTable.Rows[row]["passed"];
                var notPassedTestPercentage = dataTable.Rows[row]["% not passed"];




                using (var database = new RelationCont())
                {    
                    var product = new Product() { Productname = productName.ToString(), Location = location.ToString() };
                    database.Product.Add (product);
                    var release = new Release() { Product = product, ReleaseName = releaseId.ToString() };
                    if (!(DuplicateDataCheck(release, database)))
                    { database.Release.Add(release); }
                    if (!(DuplicateDataCheck(release, database)))
                    {
                        var kpi = database.kpi.Add(new TestNotExecuted()
                        {
                            Product = product,
                            Planned = plannedTest.ToString(),
                            Executed = executedTest.ToString(),
                            NotExecutedPerdcentage = notExecutedTestPercentage.ToString()
                        });
                        var kpirelease = database.Releasekpi.Add(new JoinReleaseKpi() { kpi = kpi, Release = release });
                        database.kpi.Add(new CodeFreeze() { Product = product, FreezePeriod = freezePeriod.ToString(), Effort = effort.ToString(), TotalFTE = totalFTE.ToString(), EffortPercentage = codeFreezePercentage.ToString() });
                        database.kpi.Add(new FailedTest() { Product = product, Passed = passedTest.ToString(), NotPassedPerdcentage = notPassedTestPercentage.ToString() });
                        database.SaveChanges();
                    }
                }
            }
        }


        public bool DuplicateDataCheck(Release release, RelationCont database)
        {
            foreach (Release releas in database.Release)
            {
                if ( releas.ReleaseName == release.ReleaseName)

                {
                    return true;
                }

            }
            return false;
        }
      
        public List<string> GetAllProduct()
        {
            var productList = new List<string>();
            using (var database = new RelationCont())
            {
                foreach (Product product in database.Product)
                { productList.Add(product.Productname); }

            }
            return productList.Distinct().ToList();
          
        }


       
        public List<string> GetAllVersions(string selectedProduct)
        {
            var versionlist = new List<string>();
            using (var database = new RelationCont())
            {
                foreach (Release release in database.Release)
                {
                    if (release.Product.Productname == selectedProduct)
                        { versionlist.Add(release.ReleaseName); }
                }

                return versionlist;
            }
        }

        public string GetPErcentageValue(string percentage)
        {
            if (percentage != null && percentage!="")
            {
                  if (char.IsNumber(percentage[0]))
                  {
                     if (percentage.Contains('E'))
                      {
                        var convertExponent = double.Parse(percentage, CultureInfo.InvariantCulture);
                                        
                        var percentageDouble = Convert.ToDouble(percentage) * 100;
                       return Math.Round(percentageDouble, MidpointRounding.AwayFromZero).ToString();
                        
                       }
                      else
                      {
                        var percentageDouble = Convert.ToDouble(percentage) * 100;
                        return Math.Round(percentageDouble, MidpointRounding.AwayFromZero).ToString();
                        
                       }

                 }
                  else
                  {
                    //  return " No Data"+"0";
                    return "0";
                   }
            }
            else
            {// return " No Data" + "0"; 
                return "0";
            }
        }
        public List<string> PercentageCalculator(string selectedProduct)
        {
            var percentageList = new List<string>();
            using (var database = new RelationCont())
            {
                foreach (Release releas in database.Release)
                {
                    { Release release = releas; }
                }

                foreach (KPI kp in database.kpi)

                {
                    KPI kpi = kp;

                }
                foreach (Product produc in database.Product)

                {
                    Product product = produc;

                }

                foreach (JoinReleaseKpi releaseKpi in database.Releasekpi)
                {
                    if (releaseKpi.Release.ReleaseName == selectedProduct)
                    {

                        foreach (var kpi in database.kpi)
                        {
                            if (releaseKpi.kpi.Product.Id == kpi.Product.Id)
                            {

                                if (kpi is TestNotExecuted)
                                {    var percentage = ((TestNotExecuted)kpi).NotExecutedPerdcentage;
                                     percentageList.Add("TestNotExecuted" + GetPErcentageValue(percentage));
                                }
                                if (kpi is CodeFreeze)
                                {
                                  
                                    var percentage = ((CodeFreeze)kpi).EffortPercentage;
                                    percentageList.Add("CodeFreeze" + GetPErcentageValue(percentage));

                                 }
                                if (kpi is FailedTest)
                                {

                                    var percentage = ((FailedTest)kpi).NotPassedPerdcentage;
                                    percentageList.Add("FailedTest" + GetPErcentageValue(percentage));
                                   
                                }
                            }
                        }
                    }


                }
                return percentageList;
            }

        }
    }
}
