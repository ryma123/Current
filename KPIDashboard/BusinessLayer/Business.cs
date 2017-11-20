
using System.Collections.Generic;
using DataAccess;
using System;

namespace BusinessLayer
{
    public class Business
    {
       
        DashboardDataAccess dashboardDataAccess;
        public Business()
        {
            dashboardDataAccess = new DashboardDataAccess();
            Subscribe(dashboardDataAccess);
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
        public delegate void NotifyPresenter(Business business, EventArgs e);
        public NotifyPresenter notifyPresenter;
        public EventArgs e = null;

        public void Subscribe(DashboardDataAccess dashboard)  
        {
            dashboard.notify += HeardIt;              //attach listener class method to publisher class delegate object
        }

        private void HeardIt(DashboardDataAccess dashboard, EventArgs e)   
        {
            notifyPresenter?.Invoke(this, e);  
        }

    }
}
