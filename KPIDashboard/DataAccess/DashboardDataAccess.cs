
using System.Collections.Generic;
using System.Data;
using DataAccess.Models;
using System;

namespace DataAccess
{
    public class DashboardDataAccess
    {

        public IReader Reader;

        public DashboardDataAccess()
        {
            Reader = new ExcelReader();
            DataTable dataTable = (DataTable)Reader.Read();
            AddToDatabase(dataTable);

        }
        public void AddToDatabase(DataTable dataTable)
        {

            var numberofrows = dataTable.Rows.Count;

            for (int i = 0; i < numberofrows; i++)
            {
                var Productname = dataTable.Rows[i]["Product"];
                var releaseid = dataTable.Rows[i]["Release"];
                var Location = dataTable.Rows[i]["Location"];
                int FreezePeriod = int.Parse(dataTable.Rows[i]["FreezePeriod"].ToString());
                int TotalFTE = int.Parse(dataTable.Rows[i]["TotalFTE"].ToString());
                int Effort = int.Parse(dataTable.Rows[i]["Effort"].ToString());
                var PlannedTest = dataTable.Rows[i]["PlannedTest"];
                var PassedTest = dataTable.Rows[i]["PassedTest"];
                var ExecutedTest = dataTable.Rows[i]["ExecutedTest"];
                int PlannedUseCase = int.Parse(dataTable.Rows[i]["PlannedUseCase"].ToString());
                int ActualUsCase = int.Parse(dataTable.Rows[i]["ActualUsCase"].ToString());
                int fg = int.Parse(PlannedUseCase.ToString());

                using (var db = new RelationCont())
                {
                    var product = db.Product.Add(new Product() { ProductId = Productname.ToString(), Location = Location.ToString() });
                    var release = db.Release.Add(new Release() { Product = product, Releaseid = releaseid.ToString() });
                    db.kpi.Add(new OnTimeShipment() { Product = product, Release = release, PlannedUseCases = PlannedUseCase, ActualUseCases = ActualUsCase });
                    if (!(DuplicateData(release, db)))
                    {
                        db.kpi.Add(new CodeFreeze() { Product = product, Release = release, FreezePeriod = FreezePeriod, Effort = Effort, TotalFTE = TotalFTE });
                        db.kpi.Add(new TestCoverage() { Product = product, Release = release, Executed = ExecutedTest.ToString(), Planned = PlannedTest.ToString(), Passed = PassedTest.ToString() });
                        db.SaveChanges();
                    }
                }


            }


        }

        public bool DuplicateData(Release release, RelationCont db)
        {
            foreach (Release r in db.Release)
            {
                if (r.Product.ProductId == release.Product.ProductId || r.Releaseid == release.Releaseid)

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
                { productList.Add(p.ProductId); }

            }
            return productList;

        }
    

        public string OnTimeShipmentPercentage(KPI kpi)
        {

            if (((OnTimeShipment)kpi).PlannedUseCases == 0)
            { return "OnTimeShipment" + 0; }
            else
                return "OnTimeShipment" + (int)Math.Round((double)
                (100 * ((OnTimeShipment)kpi).ActualUseCases) / ((OnTimeShipment)kpi).PlannedUseCases);



        }
        public string CodeFreezePercentage(KPI kpi)
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

        public string TestCoveragePercentage(KPI kpi)
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
                    if (r.Product.ProductId == selectedProduct)

                        versionlist.Add(r.Releaseid);
                }
                foreach (KPI kpi in db.kpi)
                {
                    string s = kpi.Release.Releaseid;

                }
                return versionlist;

            }

        }

        public List<string> PercentageCalculator(string selectedProduct)
        {
            var percentage = new List<string>();
            using (var db = new RelationCont())
            {
                foreach (Release r in db.Release)
                {
                    if (r.Releaseid == selectedProduct)

                    { Release ra = r; }
                }
                foreach (KPI kpi in db.kpi)
                {
                    if (kpi.Release.Releaseid == selectedProduct)
                    {
                        if (kpi is OnTimeShipment)
                        {
                            percentage.Add(OnTimeShipmentPercentage(kpi));
                        }
                        else if (kpi is CodeFreeze)
                        {
                            percentage.Add(CodeFreezePercentage(kpi));
                        }
                        else if (kpi is TestCoverage)
                        {
                            percentage.Add(TestCoveragePercentage(kpi));
                        }
                    }

                }
                return percentage;

            }

        }

    }
}
