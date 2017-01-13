using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SNTQueueDataHelper;

namespace QueueManagement
{
    public partial class Customer : System.Web.UI.Page
    {
       
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerDetail cd = new CustomerDetail();
            cd.CustomerName = tbxName.Text;
            cd.MobileNo = tbxMobile.Text;
            cd.SMSMarketing = cbxSmsMarket.Checked;
            cd.OnlyVoiceCall = cbxOnlyCall.Checked;
            cd.Store();
           
        }
    }
}
