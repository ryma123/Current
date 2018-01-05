using BusinessLayer;
using System;
using System.Linq;
using System.Text.RegularExpressions;

using System.Data;

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
                //**************Have to replace these events***********//
                // Create a timer and set a two second interval.
                aTimer = new System.Timers.Timer();
                aTimer.Interval = 60000 + 60000 + 60000;


                // Hook up the Elapsed event for the timer. 
                //aTimer.Elapsed += OnTimedEvent;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;

            }

         
        }
        //UserInterface Events
     protected void Button1_Click(object sender, EventArgs e)
        {
            string version = DropDownList1.SelectedItem.ToString();
            progress1.Attributes["data-percent"] = Percentage("NotExecuted", version);
            progress2.Attributes["data-percent"] = Percentage("CodeFreeze", version);
            progress3.Attributes["data-percent"] = Percentage("FailedTest", version);
            if (Convert.ToInt32(progress1.Attributes["data-percent"]) >= 90)
            { progress1.Attributes["data-color"] = "#f75567,#ff0066"; }
            else
            { progress1.Attributes["data-color"] = "#f75567,#12b321"; }
           
            // progress1.Attributes["data-color"] = "#f75567,#12b321";
            if (Convert.ToInt32(progress2.Attributes["data-percent"]) >= 90)
            { progress2.Attributes["data-color"] = "#f75567,#ff0066"; }
            else
            { progress2.Attributes["data-color"] = "#f75567,#12b321"; }
            if (Convert.ToInt32(progress3.Attributes["data-percent"]) >= 90)
            { progress3.Attributes["data-color"] = "#f75567,#ff0066"; }
            else
            { progress3.Attributes["data-color"] = "#f75567,#12b321"; }

        }
     protected void DropDownList2_SelectedIndexChanged(Object sender, EventArgs e)
        {
            PopulateVersionComboBox();
        }
     
      protected void kpiSelectorDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTrendLines();
            
        }
        //private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        //   {
        //       business = new Business();
        //       PopulateProductComboBox();
        //       if (DropDownList2.SelectedItem != null)
        //       {

        //           PopulateVersionComboBox();
        //       }
        //       List<string> blab = ((Business)Session["Database"]).GetAllProduct();
        //   }




       //All the required sub-functions
        public void PopulateProductComboBox()
        {
            foreach (string product in ((Business)Session["Database"]).GetAllProduct())
                if (DropDownList2.Items.Contains(DropDownList2.Items.FindByText(product)) == false)
                {
                   DropDownList2.Items.Add((product));
                }


        }

        public void PopulateVersionComboBox()
        {
            DropDownList1.Items.Clear();
            string selectedProduct = DropDownList2.SelectedItem.ToString();
            foreach (string version in ((Business)Session["Database"]).GetAllVersion(selectedProduct))
            {
                DropDownList1.Items.Add(version);
            }
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


        public decimal[] RemoveZeros(decimal[] source)
        {

            decimal[] r=  source.Where(i => i != 0 ).ToArray();
            return r;
        }
        public string[] RemoveZeross(string[] source)
        {

            string[] r = source.Where(i => i != null).ToArray();
            return r;
        }
        public void ShowTrendLines()
        {
            DataTable dt = new DataTable();
            dt = ((Business)Session["Database"]).GetTrendlineInformation();

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["KpiType"].ToString() == kpiSelectorDropDown.SelectedItem.ToString()
                    && dt.Rows[i]["Product"].ToString() == DropDownList2.SelectedItem.ToString())
                {
                    y[i] = Convert.ToInt32(dt.Rows[i][3]);
                    x[i] = dt.Rows[i][1].ToString();
                }

            }
            LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Data = RemoveZeros(y) });
            LineChart1.CategoriesAxis = string.Join(",", RemoveZeross(x));
            LineChart1.Series[0].LineColor = "#f75567";
            LineChart1.Series[0].Name = kpiSelectorDropDown.SelectedItem+"-Percentage";
            LineChart1.Visible = true;
        }
       

       
    }
}

