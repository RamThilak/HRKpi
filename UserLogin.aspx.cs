using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HRKpi
{
    public partial class UserLogin : System.Web.UI.Page
    {
        private static Label MyLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            MyLabel = lblerr;

            if (!IsPostBack)
            {
                lblerr.Text = "";

                HttpCookie cookie = Request.Cookies["UserInfo"];
                
                TxtUserName.Focus();

            }

            if (IsPostBack)
            {
                lblerr.Text = "";

            }
        }



        protected void buttonsubmit_click(object sender, EventArgs e)
        {
          
            MyLabel.Text = "";

            int vuserid;

            string vusername = "";

            if (TxtUserName.Text.Length == 0)
            {
                MyLabel.Text = "Invalid User Name !";
                return;
            }

            if (TxtPassword.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Password !";
                return;
            }

            if (Kpi_Button.Checked == true)
            {
                vuserid = ValidateUser(TxtUserName.Text, TxtPassword.Text);
            }
            else
            {
                vuserid = ValidatePepUser(TxtUserName.Text, TxtPassword.Text);
            }

            if (vuserid == 0)
            {
                MyLabel.Text = "Login Unsucessful!";
                return;
            }

            else
            {


                // MyLabel.Text = "Login sucessful!";

                DataTable dt = new DataTable();

                if (Kpi_Button.Checked == true)
                {
                    dt = GetLoginUserDetail(vuserid);
                }
                else
                {
                    dt = GetLoginPepUserDetail(vuserid);
                }

                if (dt.Rows.Count != 0)
                {
                    vusername = dt.Rows[0].Field<string>(0);
                    Session["UserLevel"] = dt.Rows[0].Field<Int32>(1);
                }
                Session["UserID"] = Convert.ToString(vuserid);
                Session["UserName"] = vusername;
                Session["LoginUserCompanyId"] = dt.Rows[0].Field<Int32>(2);
                

                int logincomp = Convert.ToInt32(dt.Rows[0].Field<Int32>(2));
                Response.Redirect("Home.aspx");
            }

        }

        protected int ValidateUser(string uname, string pwd)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("Validate_User", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", uname);
            cmd.Parameters.AddWithValue("@Password", pwd);
            cmd.Connection = conn;
            conn.Open();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return result;

        }

        protected int ValidatePepUser(string uname, string pwd)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand("Validate_pep_User", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", uname);
            cmd.Parameters.AddWithValue("@Password", pwd);
            cmd.Connection = conn;
            conn.Open();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return result;

        }

        protected DataTable GetLoginUserDetail(int uid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("ProcReturnUserDetail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", uid);
            cmd.Connection = conn;
            conn.Open();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtuser);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dtuser;

        }


        protected DataTable GetLoginPepUserDetail(int uid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("ProcReturnPepUserDetail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", uid);
            cmd.Connection = conn;
            conn.Open();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtuser);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dtuser;

        }

    }



}
