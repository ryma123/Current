
using System.Collections.Generic;
using System.Data;
using DataAccess.Models;
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

                //using (var db = new RelationCont())
                //{
                //    var product = db.product.Add(new Product() { ProductId = Productname.ToString(), location = Location.ToString() });
                //    var release = db.release.Add(new Release() { Product = product, Releaseid = releaseid.ToString() });

                //    db.kpi.Add(new OnTimeShipment() { product = product, release = release, PlannedUseCases = PlannedUseCase, ActualUseCases = ActualUsCase });

                //    db.kpi.Add(new CodeFreeze() { product = product, release = release, FreezePeriod = FreezePeriod, Effort = Effort, TotalFTE = TotalFTE });
                //    db.kpi.Add(new TestCoverage() { product = product, release = release, executed = ExecutedTest.ToString(), planned = PlannedTest.ToString(), passed = PassedTest.ToString() });
                //    db.SaveChanges();
                //}


            }


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
        public int GetPercent(string version)
        {

            using (var db = new RelationCont())
            {
                foreach (OnTimeShipment kp in db.kpi)
                {
                    if (kp.Release.Releaseid == version)

                    {
                        //return kp.ActualUseCases;
                        return 50;
                    }
                }

            }
            return -1;




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

            }
            return versionlist;

        }

    }

}
