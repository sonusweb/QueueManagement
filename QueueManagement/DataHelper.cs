using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace QueueManagement
{
    public class DataHelper
    {
    }
    [Serializable]
    public class CustomerDetail
    {
        public CustomerDetail()
        { }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int MobileNo { get; set; }
        public bool SMSMarketing { get; set; }
        public bool OnlyVoiceCall { get; set; }
        //public Customer(int CustomerID)
        //{
        //    Load(CustomerID);
        //}
        public CustomerDetail(int MobileNo)
        {
            Load(MobileNo);
        }


        public void Load()
        {
            SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["QueueManagement"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("spQM_GetCustomer", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                connect.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["CustomerID"] == null)
                    {
                        throw new Exception("Custmer not found");
                    }
                    CustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                    CustomerName = dr["CustomerName"].ToString();
                    MobileNo = Convert.ToInt32(dr["MobileNo"].ToString());
                    SMSMarketing = Convert.ToBoolean(dr["SMSMarketing"].ToString());
                    OnlyVoiceCall = Convert.ToBoolean(dr["OnlyVoiceCall"].ToString());
                }

            }
            finally
            {
                connect.Close();
            }
        }

        //public void Load(int CustomerID)
        //{
        //    SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["QueueManagement"].ToString());
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("spQM_GetCustomerByCustomerID", connect);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        connect.Open();
        //        SqlParameter pCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
        //        pCustomerID.Value = CustomerID;
        //        cmd.Parameters.Add(pCustomerID);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            if (dr["CustomerID"] == null)
        //            {
        //                throw new Exception("Custmer not found");
        //            }
        //            CustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
        //            CustomerName = dr["CustomerName"].ToString();
        //            MobileNo = Convert.ToInt32(dr["MobileNo"].ToString());
        //            SMSMarketing = Convert.ToBoolean(dr["SMSMarketing"].ToString());
        //            OnlyVoiceCall = Convert.ToBoolean(dr["OnlyVoiceCall"].ToString());
        //        }

        //    }
        //    finally
        //    {
        //        connect.Close();
        //    }
        //}


        public void Load(int MobileNo)
        {
            SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["QueueManagement"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("spQM_GetCustomerByMobileNo", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                connect.Open();
                SqlParameter pMobileNo = new SqlParameter("@MobileNo", SqlDbType.Int);
                pMobileNo.Value = MobileNo;
                cmd.Parameters.Add(pMobileNo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["MobileNo"] == null)
                    {
                        throw new Exception("Custmer not found");
                    }
                    CustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
                    CustomerName = dr["CustomerName"].ToString();
                    MobileNo = Convert.ToInt32(dr["MobileNo"].ToString());
                    SMSMarketing = Convert.ToBoolean(dr["SMSMarketing"].ToString());
                    OnlyVoiceCall = Convert.ToBoolean(dr["OnlyVoiceCall"].ToString());
                }

            }
            finally
            {
                connect.Close();
            }
        }

        public void Store()
        {
            SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["QueueManagement"].ToString());

            SqlCommand cmd = new SqlCommand("spQM_SaveCustomer", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] prms = new SqlParameter[5];
            prms[0] = new SqlParameter("@CustomerName", SqlDbType.VarChar, 50);
            prms[0].Value = CustomerName;
            prms[1] = new SqlParameter("@MobileNo", SqlDbType.Int);
            prms[1].Value = MobileNo;
            prms[2] = new SqlParameter("@SMSMarketing", SqlDbType.Bit);
            prms[2].Value = SMSMarketing;
            prms[3] = new SqlParameter("OnlyVoiceCall", SqlDbType.Bit);
            prms[3].Value = OnlyVoiceCall;
            prms[4] = new SqlParameter("@CustomerID", SqlDbType.Int);
            prms[4].Direction = ParameterDirection.Output;

            cmd.Parameters.AddRange(prms);
            try
            {
                connect.Open();
                cmd.ExecuteNonQuery();
                CustomerID = (int)prms[4].Value;

            }
            finally
            {
                connect.Close();
            }

        }
    }
}
