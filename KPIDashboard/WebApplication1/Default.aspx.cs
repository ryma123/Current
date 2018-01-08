using BusinessLayer;
using System;
using System.Linq;
using System.Text.RegularExpressions;

using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

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
                // Create colour timer and set colour two second interval.
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
            progress1.Attributes["data-percent"] = Percentage("TestNotExecuted", version);
            progress2.Attributes["data-percent"] = Percentage("CodeFreeze", version);
            progress3.Attributes["data-percent"] = Percentage("FailedTest", version);
            if (Convert.ToInt32(progress1.Attributes["data-percent"]) >= 90)
            { progress1.Attributes["data-color"] = "#f75567,##db0a23"; }
            else
            { progress1.Attributes["data-color"] = "#f75567,#12b321"; }
           
            // progress1.Attributes["data-color"] = "#f75567,#12b321";
            if (Convert.ToInt32(progress2.Attributes["data-percent"]) >= 90)
            { progress2.Attributes["data-color"] = "#f75567,##db0a23"; }
            else
            { progress2.Attributes["data-color"] = "#f75567,#12b321"; }
            if (Convert.ToInt32(progress3.Attributes["data-percent"]) >= 90)
            { progress3.Attributes["data-color"] = "#f75567,##db0a23"; }
            else
            { progress3.Attributes["data-color"] = "#f75567,#12b321"; }
            var grades= ((Business)Session["Database"]).GetGrade(version);
            Label4.Text = grades[0];
            GetLabelColour(Label4, Label4.Text);
            Label1.Text = grades[1];
            GetLabelColour(Label1, Label1.Text);
            Label5.Text = grades[2];
            GetLabelColour(Label5, Label5.Text);

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

            decimal[] r = source.Where(i => i != 5000).ToArray();
            return r;
        }
       
        public string[] RemoveZeross(string[] source)
        {

            string[] r = source.Where(i => i != null).ToArray();
            return r;
        }
        public void GetLabelColour(Label label,string value)
        {
           
            switch (value)
            {
              
                    case "A":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#009900");
                    break;

                    case "B":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#80ff80");
                    break;

                    case "C":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff00");
                    break;

                    case "D":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9900");
                    break;

                    case "E":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff3300");
                     break;
                   
                    case "No Data":
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#669999");
                    break;
                    
            }
           
        }
       // public void GetBarColour()
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
                else
                {
                    y[i] = Convert.ToInt32("5000");
                   
                }

            }

            Chart2.Series[0].Points.DataBindXY(RemoveZeross(x), RemoveZeros(y));
            Chart2.Series[0].ChartType = SeriesChartType.Column;

            Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            // Chart2.Legends[0].Enabled = true;
             Chart2.Visible = true;


            Chart2.Series[0]["PointWidth"]= "0.3";
            foreach (DataPoint point in Chart2.Series[0].Points)
            {

                if (point.YValues[0] >= 0 && point.YValues[0] < 5)
                { point.Color = System.Drawing.ColorTranslator.FromHtml("#009900"); }
                if (point.YValues[0] >= 5 && point.YValues[0] < 15)
                { point.Color = System.Drawing.ColorTranslator.FromHtml("#80ff80"); }
                if (point.YValues[0] >= 15 && point.YValues[0] < 30)
                { point.Color = System.Drawing.ColorTranslator.FromHtml("#ffff00"); }
                if (point.YValues[0] >= 30 && point.YValues[0] < 40)
                { point.Color = System.Drawing.ColorTranslator.FromHtml("#009900"); }
                if (point.YValues[0] >= 40 && point.YValues[0] <= 100)
                { point.Color = System.Drawing.ColorTranslator.FromHtml("#ff3300"); }

            }
                //       else
                //       { LineChart1.Series[0].BarColor = "#000099"; }
                //   }
                ////   LineChart1.Series[0].BarColor = "#f75567";
                //   LineChart1.Series[0].Name = kpiSelectorDropDown.SelectedItem+"-Percentage";

                // Chart2.Visible = true;
            }


        }
}

