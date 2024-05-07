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
    public partial class ChangeKPIApprover : System.Web.UI.Page
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
                        cbolevel1company.DataTextField = "company_name";
                        cbolevel1company.DataValueField = "company_id";
                        cbolevel1company.DataSource = dt;
                        cbolevel1company.DataBind();
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
                        cbolevel2company.DataTextField = "company_name";
                        cbolevel2company.DataValueField = "company_id";
                        cbolevel2company.DataSource = dt;
                        cbolevel2company.DataBind();
                        dt.Dispose();
                    }
                }


                cbocompany.SelectedValue = "0";
                txtcode.Text = "";
                lblname.Text = "";
                lblmailid.Text = "";
                lblapp1.Text = "";
                lblapp2.Text = "";
                cbolevel1company.SelectedValue = "0";
                cbolevel1emp.SelectedValue = "0";
                cbolevel2company.SelectedValue = "0";
                cbolevel2emp.SelectedValue = "0";              


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
                MyLabel.Text = "No Such Employee Code in Master!";
                return;
            }



            int chklogin = CheckLoginExists(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (chklogin == 0)
            {
                MyLabel.Text = "No Login Exists For This Employee!";
                return;
            }
   
            dt = GetKPIApprover1(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (dt.Rows.Count != 0)
            {
                lblapp1.Text = dt.Rows[0].Field<string>(1);
            }
            //else
            //{
            //    MyLabel.Text = "No Record Found!";
            //    return;
            //}

            dt = GetKPIApprover2(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (dt.Rows.Count != 0)
            {
                lblapp2.Text = dt.Rows[0].Field<string>(1);
            }
            //else
            //{
            //    MyLabel.Text = "No Record Found!";
            //    return;
            //}
        }

        protected DataTable GetKPIApprover1(int compid, string ecode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("GetKPIApprover1", conn);
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

        protected DataTable GetKPIApprover2(int compid, string ecode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("GetKPIApprover2", conn);
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


        protected int CheckLoginExists(int compid, string ecode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dtuser = new DataTable();
            SqlCommand cmd = new SqlCommand("CheckLoginExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@compid", compid);
            cmd.Parameters.AddWithValue("@empid", ecode);
            cmd.Connection = conn;
            conn.Open();
            int empexist = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return empexist;

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

        protected void cbolevel1company_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtcompanyLevel1 = new DataTable();
            dtcompanyLevel1 = GetCompanyEmpDetail(Convert.ToInt32(cbolevel1company.SelectedValue));
            cbolevel1emp.DataTextField = "emp_name";
            cbolevel1emp.DataValueField = "emp_code";
            cbolevel1emp.DataSource = dtcompanyLevel1;
            cbolevel1emp.DataBind();
        }

        protected void cbolevel2company_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtcompanyLevel2 = new DataTable();
            dtcompanyLevel2 = GetCompanyEmpDetail(Convert.ToInt32(cbolevel2company.SelectedValue));
            cbolevel2emp.DataTextField = "emp_name";
            cbolevel2emp.DataValueField = "emp_code";
            cbolevel2emp.DataSource = dtcompanyLevel2;
            cbolevel2emp.DataBind();
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


                if (cbolevel1company.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver 1 Company!";
                    return;
                }

                if (cbolevel1emp.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver1!";
                    return;
                }


                if (cbolevel2company.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver 2 Company!";
                    return;
                }

                if (cbolevel2emp.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver2!";
                    return;
                }



                con.Open();
                objTrans = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("ChangeKPIApprover");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = objTrans;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                cmd.Parameters.AddWithValue("@empcode", txtcode.Text);
                cmd.Parameters.AddWithValue("@newcomp1", Convert.ToInt32(cbolevel1company.SelectedValue));
                cmd.Parameters.AddWithValue("@newemp1", cbolevel1emp.SelectedValue);
                cmd.Parameters.AddWithValue("@newcomp2", Convert.ToInt32(cbolevel2company.SelectedValue));
                cmd.Parameters.AddWithValue("@newemp2", cbolevel2emp.SelectedValue);
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