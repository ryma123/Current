using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        Business business;
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

            }
        }



        public void PopulateProductComboBox()
        {
            foreach (string product in ((Business)Session["Database"]).GetAllProduct())

            { DropDownList2.Items.Add(product); }

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

        public void Subscribe(Business business)  //get the object of pubisher class
        {
            business.notifyPresenter+= HeardIt;    //attach listener class method to publisher class delegate object
        }
        private void HeardIt(Business business, EventArgs e)   //subscriber class method
        {
            PopulateProductComboBox();
            if (DropDownList2.SelectedItem != null)
            {

                PopulateVersionComboBox();
            }
            // notifyPresenter?.Invoke(this, e);  //if it points i.e. not null then invoke that method!

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