
using System.Collections.Generic;
using DataAccess;
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



    }
}
