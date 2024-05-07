using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace HRKpi
{

    public partial class ChangePEPApprover : System.Web.UI.Page
    {
        protected private static Label MyLabel;
        protected void Page_Load(object sender, EventArgs e)
        {
            MyLabel = lblerr;

            if (!IsPostBack)
            {
                MyLabel = lblerr;
                lblerr.Text = "";
                cbocompany.Items.Clear();
                cbocompany.Items.Add(new ListItem("-- Select Company --", "0"));

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetEmpCompanyList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                        cmd1.Parameters.AddWithValue("@comp", Convert.ToInt32(Session["LoginUserCompanyId"]));
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt);
                        cbocompany.DataTextField = "company_name";
                        cbocompany.DataValueField = "company_id";
                        cbocompany.DataSource = dt;
                        cbocompany.DataBind();
                        dt.Dispose();
                    }
                }


                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetCompanyList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt);
                        cboapprovercompany.DataTextField = "company_name";
                        cboapprovercompany.DataValueField = "company_id";
                        cboapprovercompany.DataSource = dt;
                        cboapprovercompany.DataBind();
                        dt.Dispose();
                    }
                }


                cbocompany.SelectedValue = "0";
                txtcode.Text = "";
                lblname.Text = "";
                lblmailid.Text = "";               
                cboapprover.SelectedValue = "0";
                cboapprovercompany.SelectedValue = "0";
                lblapprover.Text = "";

            }
        }


        protected void button_exit_click(object sender, EventArgs e)
        {
            Response.Redirect("Home.Aspx");
        }


        protected DataTable GetEmpDetail(int compid, string ecode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("GetEmployeeName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Parameters.AddWithValue("@empid", ecode);
            cmd.Connection = conn;
            conn.Open();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtuser);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dtuser;

        }

        protected void button_getdetail_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = GetEmpDetail(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (dt.Rows.Count != 0)
            {
                lblname.Text = dt.Rows[0].Field<string>(0);
                lblmailid.Text = dt.Rows[0].Field<string>(1);
            }

            if (lblname.Text == "NoRecord")
            {
                MyLabel.Text = "No Such Employee Code!";
                return;
            }

                       
            dt = GetPEPApprover(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (dt.Rows.Count != 0)
            {
                lblapprover.Text = dt.Rows[0].Field<string>(1);
            }
            else
            {
                MyLabel.Text = "No Record Found!";
                return;
            }

        }

        protected DataTable GetCompanyEmpDetail(int compid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("GetCompanyEmployeeList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Connection = conn;
            conn.Open();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtuser);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dtuser;

        }

        protected DataTable GetPEPApprover(int compid,string ecode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("GetPEPApprover", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Parameters.AddWithValue("@empcode", ecode);
            cmd.Connection = conn;
            conn.Open();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtuser);
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dtuser;

        }


        protected void cboapprovercompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtapprovercompany = new DataTable();
            dtapprovercompany = GetCompanyEmpDetail(Convert.ToInt32(cboapprovercompany.SelectedValue));
            cboapprover.DataTextField = "emp_name";
            cboapprover.DataValueField = "emp_code";
            cboapprover.DataSource = dtapprovercompany;
            cboapprover.DataBind();
        }

        protected void button_add_Click(object sender, EventArgs e)
        {
         
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlTransaction objTrans = null;
                SqlConnection con = new SqlConnection(constr);


                try

                {

                    if (cbocompany.SelectedValue == "0")
                    {
                        MyLabel.Text = "Select Company!";
                        return;
                    }

                    if (txtcode.Text.Length == 0)
                    {
                        MyLabel.Text = "Invalid Employee Code!";
                        return;
                    }

                    if (lblname.Text.Length == 0)
                    {
                        MyLabel.Text = "Invalid Employee Name!";
                        return;
                    }

                    if (cboapprovercompany.SelectedValue == "0")
                    {
                        MyLabel.Text = "Select Approver Company!";
                        return;
                    }

                    if (cboapprover.SelectedValue == "0")
                    {
                        MyLabel.Text = "Select Approver!";
                        return;
                    }                                     


                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("ChangePEPApprover");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@empcode", txtcode.Text);
                    cmd.Parameters.AddWithValue("@newcomp", Convert.ToInt32(cboapprovercompany.SelectedValue));
                    cmd.Parameters.AddWithValue("@newemp", Convert.ToInt32(cboapprover.SelectedValue));
                    cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                    cmd.ExecuteNonQuery();
                    objTrans.Commit();
                    objTrans.Dispose();
                    objTrans = null;

                  
                    string message = " Record Updated Sucessfully ! ";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }

                catch (Exception ex)
                {
                    MyLabel.Text = ex.ToString();

                    if (objTrans != null)
                    {
                        objTrans.Rollback();
                        objTrans.Dispose();
                        objTrans = null;
                    }


                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        con.Dispose();
                    }


                    return;

                }


                finally
                {


                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }



                }



           

        }
    }
}