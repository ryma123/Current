using KPI_Dashboard.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //DashboardPresenter db = new DashboardPresenter();

               // Session["Database"] = db;
              // PopulateProductComboBox();
                if (DropDownList2.SelectedItem != null)
                {

              ///  PopulateVersionComboBox();
                }

            }
        }

      

        public void PopulateProductComboBox()
        {
            //foreach (string product in ((DashboardPresenter)Session["Database"]).GetAllProduct())

            //{ DropDownList2.Items.Add(product); }

        }

        public void PopulateVersionComboBox()
        {
            DropDownList1.Items.Clear();
            string temp = DropDownList2.SelectedItem.ToString();
            foreach (string version in ((DashboardPresenter)Session["Database"]).GetAllVersion(temp))
            { DropDownList1.Items.Add(version); }
        }






        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedItem != null)
            {

                PopulateVersionComboBox();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string v = DropDownList1.SelectedItem.ToString();
            int s = business.GetPercent(v);
            string t = s.ToString();
            Label4.Text = t;

            // ProgressText.InnerText =TextBox1.Text + "%";
            // Div2.InnerText = TextBox1.Text + "%";
            ////  CalculateActiveUsersAngle(16, out val1, out val2, out colorCode);
            //  CalculateActiveUsersAngle(Convert.ToInt32(TextBox1.Text), out val3, out val4, out colorCode2);

            ft.Attributes["data-percent"] = t;
        }

       
    }
}