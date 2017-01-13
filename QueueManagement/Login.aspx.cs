using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SNTQueueDataHelper;

namespace QueueManagement
{
    public partial class Login : System.Web.UI.Page
    {
        QueueDataHelper dh = new QueueDataHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginQM_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool isValidLogin;
            string UserID, Password;
            UserID = QMLogin.UserName.Trim();
            Password = QMLogin.Password.Trim();
            dh.AuthenticateLogin(UserID, Password,out isValidLogin);
            
            if (isValidLogin)
            {
                e.Authenticated = true;
                Response.Redirect("AddCustomerQueue.aspx?UserID="+ UserID+"&Password="+Password+"");

            }
            else
            {
                e.Authenticated = false;
            }
            
        }
    }
}
