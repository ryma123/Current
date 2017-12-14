using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Timers;


namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        Business business;
        private static System.Timers.Timer aTimer;
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                business = new Business();
                Session["Database"] = business;
                
                PopulateProductComboBox();
                if (DropDownList2.SelectedItem != null)
                {

                    PopulateVersionComboBox();
                }
                // Create a timer and set a two second interval.
                aTimer = new System.Timers.Timer();
                aTimer.Interval = 60000+60000+60000;
               

                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;
            }
           
            //PopulateProductComboBox();
            //if (DropDownList2.SelectedItem != null)
            //{

            //    PopulateVersionComboBox();
            //}
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            business = new Business();
            PopulateProductComboBox();
            if (DropDownList2.SelectedItem != null)
            {

                PopulateVersionComboBox();}
                List<string> blab = ((Business)Session["Database"]).GetAllProduct();            
        }
        public void PopulateProductComboBox()
        {
            foreach (string product in ((Business)Session["Database"]).GetAllProduct())
                if (DropDownList2.Items.Contains(DropDownList2.Items.FindByText(product))==false)
                {
                    //code..
               DropDownList2.Items.Add((product));
                }

            //{  }
   
        }

        public void PopulateVersionComboBox()
        {
            DropDownList1.Items.Clear();
            string temp = DropDownList2.SelectedItem.ToString();
            foreach (string version in ((Business)Session["Database"]).GetAllVersion(temp))
            { DropDownList1.Items.Add(version); }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            string version = DropDownList1.SelectedItem.ToString();
            progress1.Attributes["data-percent"] = Percentage("OnTimeShipment", version);
            progress2.Attributes["data-percent"] = Percentage("CodeFreeze", version);
            progress3.Attributes["data-percent"] = Percentage("TestCoverage", version);


        }

        protected void DropDownList2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            PopulateVersionComboBox();
        }

       
      
        public string Percentage(string kpiType, string version)
        {
            var stringPercentage = "";
            foreach (var percentage in ((Business)Session["Database"]).GetPercent(version))
            {
                if (percentage.StartsWith(kpiType))
                {
                    stringPercentage = Regex.Match(percentage, @"\d+").Value;

                }

            }
            return stringPercentage;
        }
    }
}