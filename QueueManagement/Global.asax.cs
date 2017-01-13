using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Mail;
using System.Web.Caching;
using System.Web.SessionState;
using System.IO;
using System.Messaging;
using SNTSmsCommunication;
using System.IO.Ports;


namespace QueueManagement
{
    public class Global : System.Web.HttpApplication
    {
        private const string DummyCacheItemKey = "GagaGuguGigi";
        public string smsMessage,errorMessage;
        #region Private Variables
        SerialPort port = new SerialPort();
        ClsSMS objclsSMS = new ClsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        #endregion
       
        public Global()
        {
            InitializeComponent();
        }
        public System.Threading.Thread schedulerThread = null;
        protected void Application_Start(object sender, EventArgs e)
        {
            
            //ThreadStart tsTask = new ThreadStart(TaskLoop);
            //schedulerThread = new System.Threading.Thread(ReadSMS);
            //schedulerThread.Start();
            //RegisterCacheEntry();

        }
      

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                RegisterCacheEntry();
            }

        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            if (null != schedulerThread)
            {
                schedulerThread.Abort();
            } 

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Debug.WriteLine(Server.GetLastError());
        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                    DateTime.MaxValue, TimeSpan.FromMinutes(1),
                    CacheItemPriority.Normal,
                    new CacheItemRemovedCallback(CacheItemRemovedCallback));
            return true;
        }
        public void CacheItemRemovedCallback(string key,
                object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback" + DateTime.Now.ToString());

            HitPage();
            ReadSMS();
        }
        private const string DummyPageUrl = "http://localhost:49719/WebForm1.aspx";
        private void HitPage()
        {
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }
       
    

        public void ReadSMS()
        {

            try
            {
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
                        foreach (ShortMessage msg in objShortMessageCollection)
                        {

                            //ListViewItem item = new ListViewItem(new string[] { msg.Index, msg.Sent, msg.Sender, msg.Message });
                            //item.Tag = msg;
                            //lvwMessages.Items.Add(item);
                           smsMessage = msg.Index + "-" + msg.Sent + "-" + msg.Sender + "-" + msg.Message;


                        }
                        #endregion
                    }

                }
                else
                {

                    //MessageBox.Show("There is no message in SIM");
                    errorMessage = "There is no message in SIM";

                }
                this.port.Close();
            }
            catch (Exception ex)
            {
               errorMessage = ex.Message.ToString();
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
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
            }
        }




        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            
        }
        #endregion

       
    }
}