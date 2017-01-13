using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.IO.Ports;
using Microsoft.Reporting.WebForms;
using System.Text;
using SNTQueueDataHelper;
using SNTSmsCommunication;



namespace QueueManagement
{
    public partial class AddCustomerQueue : System.Web.UI.Page
    {
        CustomerQueueCol cqCol;
        QueueDataHelper qdh;
        String status;
        int clientID;
        String UserID, Password;
        bool IsUserAdmin, IsUserWebAdmin;
        bool smsSent = false;
        //Code for sms//
        #region Private Variables
        SerialPort port = new SerialPort();
        ClsSMS objclsSMS = new ClsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        #endregion

        #region Private Methods
        private void WriteStatusBar(string smsStatus)
        {
            try
            {
                lblMsg.Text = "Message:" + smsStatus;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.AppendHeader("Refresh", "15");
         //   SendSMS();
           // CheckBalance();
           // ReadSMS();
              
           
            UserID = Request.QueryString["UserID"];
            Password = Request.QueryString["Password"];
            Session["UserID"] = UserID;
            Session["Password"] = Password;

            ClientDetail cld = new ClientDetail();
            cld.Load(UserID, Password);
            lblClientName.Text = cld.ClientName;
            clientID = cld.ClientID;

            ContactDetail cd = new ContactDetail();
            cd.Load(UserID, Password);
            lblUserName.Text = cd.FName +" " + cd.LName;
            IsUserAdmin = cd.IsUserAdmin;
            IsUserWebAdmin = cd.IsUserWebAdmin;
            if (IsUserAdmin)
            {
                btnReport.Visible = true;
            }
            if (IsUserWebAdmin)
            {
            }
            
            if (!Page.IsPostBack)
            {
                BindGridView();
            }
            
            
        }
        private void BindGridView()
        {
            //String Status = "D";
            cqCol = new CustomerQueueCol(clientID);
            grdCustomerQueue.DataSource = cqCol;
            grdCustomerQueue.DataBind();
            
        }
        private void ClearForm()
        {
            tbxName.Text = "";
            tbxMobile.Text = "";
            tbxNotes.Text = "";
            tbxWaitTime.Text = "";
            tbxPartySize.Text = "";
            cbxSendSms.Checked = false;
        }
        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerDetail cd = new CustomerDetail();
            cd.CustomerName = tbxName.Text;
            cd.MobileNo = tbxMobile.Text;
            cd.SendSMS = cbxSendSms.Checked;
            cd.OnlyVoiceCall = false;//Deactivated for now
            cd.SMSMarketing = false;//Deacticvated for now
            cd.Store();

            CustomerQueue cq = new CustomerQueue();
            cq.CustomerID = cd.CustomerID;
            cq.ClientID = clientID;
            cq.QCustomerName = tbxName.Text;
            cq.PartySize = Convert.ToInt32(tbxPartySize.Text);
            cq.Notes = tbxNotes.Text;
            cq.waitingTime = Convert.ToInt32(tbxWaitTime.Text);
            cq.Status = "D";
            cq.Store();

            if (cd.SendSMS)
            {
                string mobileNo = cd.MobileNo;
                string status = "D";
                string smsStatus = "Thanks! Your est. wait is " + cq.waitingTime + " min. You can send: 'S'-Status update,'L'-Leave,'R'–Request more time – Q-Management";
               
                //string smsStatus = "Thanks! Your est. wait is "+cq.waitingTime+" min. We'll let you know when you reach the front. Commands you can send: 'S' - Status update, 'L' - Leave, 'R' – Request more time – Q-Management ";
                SendSMS(mobileNo, smsStatus);
            }
            Response.Redirect(Request.Url.ToString(), false);
            BindGridView();
            ClearForm();
        }

        protected void grdCustomerQueue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void grdCustomerQueue_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (!Page.IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList ddl = e.Row.FindControl("DDL1") as DropDownList;
                        if (ddl != null)
                        {
                            ddl.SelectedIndexChanged += new EventHandler(ddlStatus_SelectedIndexChanged);
                        }
                    }
                } 
            

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Control ctrl = e.Row.FindControl("ddlStatus");
                    if (ctrl != null)
                    {

                        DropDownList dropDownLst = ctrl as DropDownList;

                        dropDownLst.DataSource = qdh.getQueueStatus();
                        dropDownLst.DataTextField = "StatusDesc";
                        dropDownLst.DataValueField = "Status";
                        
                       
                        dropDownLst.DataBind();
                        // dropDownLst.SelectedValue = grdCustomerQueue.DataKeys[e.Row.RowIndex].Values[0].ToString();

                    }
                };
            

        }



        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
       
           status =  ((DropDownList)sender).SelectedValue;
           
        }
        public void grdCustomerQueue_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

            try
            { // If multiple ButtonField column fields are used, use the
                // CommandName property to determine which button was clicked.
                if (e.CommandName == "Serve")
                {
                  // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    GridViewRow selectedRow = grdCustomerQueue.Rows[index];
                    int customerQueueID = Convert.ToInt32(selectedRow.Cells[0].Text);
                    string mobileNo = selectedRow.Cells[5].Text;
                    string status = "T";
                    string smsStatus = "Your table is ready – Q-Management";
                    //string smsStatus = "You have reached the front of the line! Please see the host & tell him your phone number. Thanks for waiting. - QManagement";
                    SendSMS(mobileNo, smsStatus);

                        CustomerQueue cq = new CustomerQueue();
                        cq.Update(customerQueueID, status);


                        TransactionLog tl = new TransactionLog();
                        tl.CustomerQueueID = customerQueueID;
                        tl.Status = status;
                        tl.SMSSent = true;//Set once sms sent successfully once add code
                        tl.SMSReceive = false;
                        tl.MobileNo = mobileNo;
                        tl.Store();
                   


                }
                else if (e.CommandName == "RequestMoreTime")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    GridViewRow selectedRow = grdCustomerQueue.Rows[index];
                    int customerQueueID = Convert.ToInt32(selectedRow.Cells[0].Text);
                    string mobileNo = selectedRow.Cells[5].Text;
                    string status = "R";

                    CustomerQueue cq = new CustomerQueue();
                    cq.Update(customerQueueID, status);

                    TransactionLog tl = new TransactionLog();
                    tl.CustomerQueueID = customerQueueID;
                    tl.Status = status;
                    tl.SMSSent = false;//Set once sms sent successfully once add code
                    tl.SMSReceive = true;
                    tl.MobileNo = mobileNo;
                    tl.Store();
                }
                else if (e.CommandName == "CustomerLeft")
                {
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Get the last name of the selected author from the appropriate
                    // cell in the GridView control.
                    GridViewRow selectedRow = grdCustomerQueue.Rows[index];
                    int customerQueueID = Convert.ToInt32(selectedRow.Cells[0].Text);
                    string mobileNo = selectedRow.Cells[5].Text;
                    string status = "L";

                    CustomerQueue cq = new CustomerQueue();
                    cq.Update(customerQueueID, status);

                    TransactionLog tl = new TransactionLog();
                    tl.CustomerQueueID = customerQueueID;
                    tl.Status = status;
                    tl.SMSSent = true;
                    tl.SMSReceive = false;
                    tl.MobileNo = mobileNo;
                    tl.Store();
              
                }
                BindGridView();
                    
            }
            catch (Exception ex)
            {
                
            }

        }

        protected void grdCustomerQueue_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
           //grdCustomerQueue.EditIndex = -1;

            BindGridView();
        }

        protected void grdCustomerQueue_RowEditing(Object sender, GridViewEditEventArgs e)
        {
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?UserID=" + UserID + "&Password=" + Password + "");
        }

       
        public void SendSMS(string mobileNo,string smsMsg)
        {
            try
            {

                #region codeto send sms from api
                
                //string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=sonupatel@shaientech.com:sonu1970&senderID=TEST SMS&receipientno="+mobileNo+"&dcs=0&msgtxt="+smsMsg+"&state=4";

                //WebRequest request = HttpWebRequest.Create(strUrl);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream s = (Stream)response.GetResponseStream();
                //StreamReader readStream = new StreamReader(s);
                //string dataString = readStream.ReadToEnd();
                //response.Close();
                //s.Close();
                //readStream.Close();
                //code to send sms using mvaayoo
                #endregion //codeto send sms from api

                #region Code to send sms using wire2air api

                string ret=string.Empty;
                WebRequest w=WebRequest.Create("http://smsapi.Wire2Air.com/smsadmin/submitsm.aspx");
                w.Method="POST";
                w.ContentType="application/x-www-form-urlencoded";
                using(Stream writeStream = w.GetRequestStream())
                 {
                  UTF8Encoding encoding = new UTF8Encoding();
                  byte[] bytes = encoding.GetBytes("VERSION=2.0&userid=shaientech&password=SNT2029!&VASId=1852&PROFILEID=2&FROM=27126&TO="+mobileNo+"&Text="+smsMsg+"");
                  writeStream.Write(bytes, 0, bytes.Length);
                 }
                 using (HttpWebResponse r = (HttpWebResponse) w.GetResponse())
                  {
                 using (Stream responseStream = r.GetResponseStream())
                 {
                 using (StreamReader readStream = new StreamReader (responseStream, Encoding.UTF8))
                  {
                        ret = readStream.ReadToEnd();
                  }
                  }
                  }
                 //this.lblMsg.Visible = true;
                this.lblMsg.Text = "MSG" + ret;
                 //this.lblMsg.Text = "Test";
               // MessageBox.Show(ret); /* result of API call*/

                #endregion


                ////Code to send sms using modem
                //OpenPort();
                //if (this.port.IsOpen)
                //{
                //    // Thread.Sleep(1000);
                //    if (objclsSMS.sendMsg(this.port, mobileNo, smsMsg))
                //    {
                //        this.lblMsg.Text = "Message has sent successfully to " + mobileNo + ".";
                //        this.port.Close();
                //        objclsSMS.ClosePort(this.port);
                //        // smsSent = true;
                //    }
                //    else
                //    {
                //        this.lblMsg.Text = "Failed to send message to " + mobileNo + ".";
                //        // smsSent = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message.ToString();
                //objclsSMS.ClosePort(this.port);
            }
        }
        public void CheckBalance()
        {
            try
            {
                string strUrl = "http://api.mvaayoo.com/mvaayooapi/APIUtil?user=sonupatel@shaientech.com:sonu1970&type=0";
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
            }
        }
        public void ReadSMS()
        {                        
            try
            {
                #region readsmsusingmvaayoo
                //string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageReply?user=sonupatel@shaientech.com:sonu1970&senderID=TEST SMS&receipientno=9227105219&sdtime=2012-02-26 00:00:00&edtime=2012-02-27 24:00:00";
                //WebRequest request = HttpWebRequest.Create(strUrl);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream s = (Stream)response.GetResponseStream();
                //StreamReader readStream = new StreamReader(s);
                //string dataString = readStream.ReadToEnd();
                //response.Close();
                //s.Close();
                //readStream.Close();
                //this.lblMsgRec.Text = dataString;
                #endregion

                #region readsmsusingmodem

                //Reas sms using modem
                OpenPort();
                if (this.port.IsOpen)
                {
                    //Thread.Sleep(1000);
                    //count SMS 
                    int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                    if (uCountSMS > 0)
                    {

                        #region Command
                        string strCommand = "AT+CMGL=\"ALL\"";


                        #endregion

                        // If SMS exist then read SMS
                        #region Read SMS
                        //.............................................. Read all SMS ....................................................
                        objShortMessageCollection = objclsSMS.ReadSMS(this.port, strCommand);
                        ArrayList smsArray = new ArrayList();
                        foreach (ShortMessage msg in objShortMessageCollection)
                        {
                            smsArray.Add(msg.Index + "-" + msg.Sent + "-" + msg.Sender + "-" + msg.Message);

                            rptMsgRec.DataSource = smsArray;
                            rptMsgRec.DataBind();
                            int CustomerQueueID = 0;
                            string MobileNo;
                            if (msg.Sender.Length >= 12)
                            {
                                MobileNo = msg.Sender.Substring(2, 10);

                                qdh = new QueueDataHelper();

                                qdh.getMaxCustomerQueueIDByMobileNo(MobileNo, out CustomerQueueID);
                                if (CustomerQueueID != 0)
                                {
                                    TransactionLog tl = new TransactionLog();

                                    tl.CustomerQueueID = CustomerQueueID;
                                    tl.Status = msg.Message;
                                    tl.SMSSent = false;//Set once sms sent successfully once add code
                                    tl.SMSReceive = true;
                                    tl.MobileNo = MobileNo;
                                    tl.Store();

                                    if (msg.Message == "R")
                                    {
                                        CustomerQueue cq = new CustomerQueue();
                                        cq.Update(CustomerQueueID, msg.Message);
                                    }
                                    strCommand = "AT+CMGD=1,3";
                                    objclsSMS.DeleteMsg(this.port, strCommand);
                                }
                            }

                           /** lblMsgRec.Text = msg.Index + "-" + msg.Sent + "-" + msg.Sender + "-" + msg.Message;**/

                            
                            


                        }
                        #endregion
                    }

                }
                else
                {

                    //MessageBox.Show("There is no message in SIM");
                   /** this.lblMsgRec.Text = "There is no message in SIM";**/

                }
                this.port.Close();
                #endregion

            }
            catch (Exception ex)
            {
               this.lblMsgRec.Text = ex.Message.ToString();
            }
        }

        public void OpenPort()
        {
            try
            {
                this.port = objclsSMS.OpenPort("COM5", 9600, 8, 3000, 3000);
                //if (this.port.IsOpen)
                //{
                //    this.lblMsg.Text = "Modem is connected at PORT COM5";
                    

                //}
                //else
                //{
                //    this.lblMsg.Text = "Invalid port settings";
                //}
            }
            catch(Exception ex)
            {
                this.lblMsg.Text=ex.Message.ToString();
            }
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
           //add 5 min
            tbxWaitTime.Text = (Convert.ToInt32(tbxWaitTime.Text) + 1).ToString();
        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            //subtract 5 min
            tbxWaitTime.Text = (Convert.ToInt32(tbxWaitTime.Text) - 1).ToString();
        }
    }
}
