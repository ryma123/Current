
using System.Collections.Generic;
using DataAccess;
using System;

namespace BusinessLayer
{
    public class Business
    {
        public delegate void NotifyPresenter(Business business, EventArgs e); //declaring a delegate
        public NotifyPresenter notifyPresenter;     //creating an object of delegate
        public EventArgs e = null;
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

        public void Subscribe(DashboardDataAccess dashboard)  //get the object of pubisher class
        {
            dashboard.notify += HeardIt;              //attach listener class method to publisher class delegate object
        }
        private void HeardIt(DashboardDataAccess dashboard, EventArgs e)   //subscriber class method
        {
           
            notifyPresenter?.Invoke(this, e);  //if it points i.e. not null then invoke that method!

        }

    }
}
