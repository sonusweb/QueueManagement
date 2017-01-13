using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Reporting.WebForms;
using SNTQueueDataHelper;

namespace QueueManagement
{
    public partial class ReportCustomerLeft : System.Web.UI.Page
    {
        QueueDataHelper qdh;
        int ClientID;

        String UserID, Password;
        protected void Page_Load(object sender, EventArgs e)
        {
            qdh = new QueueDataHelper();


            UserID = Request.QueryString["UserID"];
            Password = Request.QueryString["Password"];

            ClientDetail cld = new ClientDetail();
            cld.Load(UserID, Password);
            lblClientName.Text = cld.ClientName;
            ClientID = cld.ClientID;

            LoadReport(ClientID);
        }
        protected void LoadReport(int clientID)
        {

            rpvCustomerLeft.LocalReport.DataSources.Clear();

            SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["QueueManagement"].ToString());

            rpvCustomerLeft.Visible = true;

            DataSet ds = new DataSet();
            SqlParameter prms = new SqlParameter("@ClientID", clientID.ToString());
            ds = SqlHelper.ExecuteDataset(connect,
                                "spQM_GetCustomerLeftByClientID", prms);

            ReportDataSource dataSource = new ReportDataSource("dsQueue_spQM_GetCustomerLeftByClientID",
                                    ds.Tables[0]);

            rpvCustomerLeft.LocalReport.DataSources.Add(dataSource);
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    lblMessage.Text = "Sorry, no customer!";
            //}
            rpvCustomerLeft.LocalReport.Refresh();
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?UserID=" + UserID + "&Password=" + Password + "");
        }
        protected void btnReport3_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportAvgWaitTime.aspx?UserID=" + UserID + "&Password=" + Password + "");
        }

        protected void btnAddQueue_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCustomerQueue.aspx?UserID=" + UserID + "&Password=" + Password + "");
        }
    }
}
