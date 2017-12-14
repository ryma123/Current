
using System.Collections.Generic;
using System.Data;
using DataAccess.Models;
using System;
using System.Linq;

namespace DataAccess
{
    public class DashboardDataAccess
    {
       
        public IReader Reader;
        DataTable dataTable;
        public DashboardDataAccess()
        {
            Reader = new ExcelReader();
            dataTable = (DataTable)Reader.Read();
            AddToDatabase(dataTable);
          
        }

       

        

       

        public void AddToDatabase(DataTable dataTable)
        {
            var numberOfRows = dataTable.Rows.Count;
            for (int row = 0; row < numberOfRows; row++)
            {
                var Productname = dataTable.Rows[row]["Product"];
                var releaseid = dataTable.Rows[row]["Release"];
                var Location = dataTable.Rows[row]["Location"];
                int FreezePeriod = int.Parse(dataTable.Rows[row]["FreezePeriod"].ToString());
                int TotalFTE = int.Parse(dataTable.Rows[row]["TotalFTE"].ToString());
                int Effort = int.Parse(dataTable.Rows[row]["Effort"].ToString());
                var PlannedTest = dataTable.Rows[row]["PlannedTest"];
                var PassedTest = dataTable.Rows[row]["PassedTest"];
                var ExecutedTest = dataTable.Rows[row]["ExecutedTest"];
                int PlannedUseCase = int.Parse(dataTable.Rows[row]["PlannedUseCase"].ToString());
                int ActualUsCase = int.Parse(dataTable.Rows[row]["ActualUsCase"].ToString());
                int fg = int.Parse(PlannedUseCase.ToString());

                using (var db = new RelationCont())
                {
                    var product = db.Product.Add(new Product() { Productname = Productname.ToString(), Location = Location.ToString() });
                    var release = db.Release.Add(new Release() { Product = product, ReleaseNamme = releaseid.ToString() });
                    if (!(DuplicateDataCheck(release, db)))
                    {
                        var kpi = db.kpi.Add(new OnTimeShipment() { Product = product, PlannedUseCases = PlannedUseCase, ActualUseCases = ActualUsCase });
                        var kpirelease = db.Releasekpi.Add(new JoinReleaseKpi() { kpi = kpi, Release = release });
                        db.kpi.Add(new CodeFreeze() { Product = product, FreezePeriod = FreezePeriod, Effort = Effort, TotalFTE = TotalFTE });
                        db.kpi.Add(new TestCoverage() { Product = product, Executed = ExecutedTest.ToString(), Planned = PlannedTest.ToString(), Passed = PassedTest.ToString() });
                        db.SaveChanges();
                    }
                }
            }
        }

        public bool DuplicateDataCheck(Release release, RelationCont db)
        {
            foreach (Release r in db.Release)
            {
                if (r.Product.Productname == release.Product.Productname || r.ReleaseNamme == release.ReleaseNamme)

                { return true; }

            }
            return false;
        }

        public List<string> UpdateProductComboBox()
        {
            var productList = new List<string>();
            using (var db = new RelationCont())
            {
                foreach (Product p in db.Product)
                { productList.Add(p.Productname); }

            }
           
            return productList;

        }


        public string OnTimeShipmentPercentageCalculator(KPI kpi)
        {
            if (((OnTimeShipment)kpi).PlannedUseCases == 0)
            { return "OnTimeShipment" + 0; }
            else
              return "OnTimeShipment" + (int)Math.Round((double)
                (100 * ((OnTimeShipment)kpi).ActualUseCases) / ((OnTimeShipment)kpi).PlannedUseCases);
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


        public List<string> UpdateVersionComboBox(string selectedProduct)
        {
            var versionlist = new List<string>();
            using (var db = new RelationCont())
            {
                foreach (Release r in db.Release)
                {
                    if (r.Product.Productname == selectedProduct)
                        { versionlist.Add(r.ReleaseNamme); }
                }

                return versionlist;
            }
        }
       

        public List<string> PercentageCalculator(string selectedProduct)
        {
            var percentageList = new List<string>();
            using (var db = new RelationCont())
            {   //initializing entities of database 
                foreach (Release rel in db.Release)
                {
                    { Release release = rel; }
                }

                foreach (KPI kp in db.kpi)

                {
                    KPI kpi = kp;

                }
                foreach (Product prod in db.Product)

                {
                    Product product = prod;

                }

                foreach (JoinReleaseKpi releaseKpi in db.Releasekpi)
                {
                    if (releaseKpi.Release.ReleaseNamme == selectedProduct)
                    {
                       
                            foreach (var kpi in db.kpi)
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
