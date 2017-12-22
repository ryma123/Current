using System.Collections.Generic;
using System.Data;
using DataAccess.Models;
using System;
using System.Linq;

namespace DataAccess
{
    public class DashboardDataAccess
    {
       
     //   public IReader reader;
    //    DataTable dataTable;
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
                var location = dataTable.Rows[row]["Location"];
                int freezePeriod = int.Parse(dataTable.Rows[row]["FreezePeriod"].ToString());
                int totalFTE = int.Parse(dataTable.Rows[row]["TotalFTE"].ToString());
                int effort = int.Parse(dataTable.Rows[row]["Effort"].ToString());
                var plannedTest = dataTable.Rows[row]["PlannedTest"];
                var passedTest = dataTable.Rows[row]["PassedTest"];
                var executedTest = dataTable.Rows[row]["ExecutedTest"];
                int plannedUseCase = int.Parse(dataTable.Rows[row]["PlannedUseCase"].ToString());
                int actualUsCase = int.Parse(dataTable.Rows[row]["ActualUsCase"].ToString());
               

                using (var database = new RelationCont())
                {
                    var product = database.Product.Add(new Product() { Productname = productName.ToString(), Location = location.ToString() });
                    var release = database.Release.Add(new Release() { Product = product, ReleaseName = releaseId.ToString() });
                    if (!(DuplicateDataCheck(release, database)))
                    {
                        var kpi = database.kpi.Add(new OnTimeShipment() { Product = product, PlannedUseCases = plannedUseCase,
                                                                          ActualUseCases = actualUsCase });
                        var kpirelease = database.Releasekpi.Add(new JoinReleaseKpi() { kpi = kpi, Release = release });
                        database.kpi.Add(new CodeFreeze() { Product = product, FreezePeriod = freezePeriod, Effort = effort, TotalFTE = totalFTE });
                        database.kpi.Add(new TestCoverage() { Product = product, Executed = executedTest.ToString(),
                                                              Planned = plannedTest.ToString(), Passed = passedTest.ToString() });
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


        public string OnTimeShipmentPercentageCalculator(KPI kpi)
        {
            if (((OnTimeShipment)kpi).PlannedUseCases == 0)
            {
                return "OnTimeShipment" + 0;
            }
            else
            {
                return "OnTimeShipment" + (int)Math.Round((double)
                (100 * ((OnTimeShipment)kpi).ActualUseCases) / ((OnTimeShipment)kpi).PlannedUseCases);
            }
        }


        public string CodeFreezePercentageCalculator(KPI kpi)
        {
            if (((CodeFreeze)kpi).FreezePeriod * ((CodeFreeze)kpi).TotalFTE == 0)
            {
                return "CodeFreeze" + 0;
            }
            else
            {
                return "CodeFreeze" + (int)Math.Round((double)
                (100 * ((CodeFreeze)kpi).Effort) / (((CodeFreeze)kpi).FreezePeriod * ((CodeFreeze)kpi).TotalFTE));
            }
        }

        
        public string TestCoveragePercentageCalculator(KPI kpi)
        {
            if (((TestCoverage)kpi).Executed == "no data" || ((TestCoverage)kpi).Passed == "no data" || ((TestCoverage)kpi).Executed == "0")
            {
                return "TestCoverage" + 0;
            }

            else
            {
                return "TestCoverage" + (int)Math.Round((double)(100 * int.Parse(
               ((TestCoverage)kpi).Passed)) / int.Parse(((TestCoverage)kpi).Executed));

            }

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

                                    if (kpi is OnTimeShipment)
                                    {
                                        percentageList.Add(OnTimeShipmentPercentageCalculator(kpi));
                                    }
                                    if (kpi is CodeFreeze)
                                    {
                                        percentageList.Add(CodeFreezePercentageCalculator(kpi));
                                    }
                                    if (kpi is TestCoverage)
                                    {
                                        percentageList.Add(TestCoveragePercentageCalculator(kpi));
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
