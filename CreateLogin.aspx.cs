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
using System.Net.Mail;
using System.Net;
using System.Net.Security;

namespace HRKpi
{
    public partial class CreateLogin : System.Web.UI.Page
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

                DataTable dt1 = new DataTable();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetCompanyList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt1);
                        cboapprovercompany.DataTextField = "company_name";
                        cboapprovercompany.DataValueField = "company_id";
                        cboapprovercompany.DataSource = dt1;
                        cboapprovercompany.DataBind();
                        dt1.Dispose();
                    }
                }
                DataTable dt2 = new DataTable();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetCompanyList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt2);
                        cbolevel1company.DataTextField = "company_name";
                        cbolevel1company.DataValueField = "company_id";
                        cbolevel1company.DataSource = dt2;
                        cbolevel1company.DataBind();
                        dt2.Dispose();
                    }
                }

                DataTable dt3 = new DataTable();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetCompanyList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt3);
                        cbolevel2company.DataTextField = "company_name";
                        cbolevel2company.DataValueField = "company_id";
                        cbolevel2company.DataSource = dt3;
                        cbolevel2company.DataBind();
                        dt3.Dispose();
                    }
                }


                ClearControls();                
                cboapprovercompany.Focus();

                
            }
        }




        protected void button_exit_click(object sender, EventArgs e)
        {
            Response.Redirect("Home.Aspx");
        }

        protected void ClearControls()
        {
            cbocompany.SelectedValue = "0";
            txtcode.Text = "";
            lblname.Text = "";
            lblmailid.Text = "";
            chkkpi.Checked = false;
            cbolevel1company.SelectedValue = "0";
            cbolevel1emp.SelectedValue = "0";
            cbolevel2company.SelectedValue = "0";
            cbolevel2emp.SelectedValue = "0";
            cboapprover.SelectedValue = "0";
            cboapprovercompany.SelectedValue = "0";



        }

        protected DataTable GetEmpDetail(int compid,string ecode)
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
            
            dt = GetEmpDetail(Convert.ToInt32(cbocompany.SelectedValue),txtcode.Text);

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

        protected void cboapprovercompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtapprovercompany = new DataTable();
            dtapprovercompany = GetCompanyEmpDetail(Convert.ToInt32(cboapprovercompany.SelectedValue));            
            cboapprover.DataTextField = "emp_name";
            cboapprover.DataValueField = "emp_code";
            cboapprover.DataSource = dtapprovercompany;
            cboapprover.DataBind();         
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

            if (chkkpi.Checked == true)
            {
                if (cbolevel1company.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver Level 1 Company!";
                    return;
                }

                if (cbolevel1emp.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver Level 1 !";
                    return;
                }

                if (cbolevel2company.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver Level 2 Company!";
                    return;
                }

                if (cbolevel2emp.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Approver Level 2 !";
                    return;
                }

            }                        

         
                con.Open();
                objTrans = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("CreatePepLogin");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = objTrans;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                cmd.Parameters.AddWithValue("@empcode", txtcode.Text);
                cmd.Parameters.AddWithValue("@approvercompanyid", Convert.ToInt32(cboapprovercompany.SelectedValue));
                cmd.Parameters.AddWithValue("@approverid", Convert.ToInt32(cboapprover.SelectedValue));

                if (chkkpi.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@kpiflag", 1);
                    cmd.Parameters.AddWithValue("@kpilevel1companyid", Convert.ToInt32(cbolevel1company.SelectedValue));
                    cmd.Parameters.AddWithValue("@kpiapprover1", Convert.ToInt32(cbolevel1emp.SelectedValue));
                    cmd.Parameters.AddWithValue("@kpilevel2companyid", Convert.ToInt32(cbolevel2company.SelectedValue));
                    cmd.Parameters.AddWithValue("@kpiapprover2", Convert.ToInt32(cbolevel2emp.SelectedValue));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@kpiflag", 0);
                    cmd.Parameters.AddWithValue("@kpilevel1companyid", 0);
                    cmd.Parameters.AddWithValue("@kpiapprover1", 0);
                    cmd.Parameters.AddWithValue("@kpilevel2companyid", 0);
                    cmd.Parameters.AddWithValue("@kpiapprover2", 0);
                }
                               
                cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                cmd.ExecuteNonQuery();
                objTrans.Commit();
                objTrans.Dispose();
                objTrans = null;

                if (lblmailid.Text.Length > 5)
                {
                    SendMail(Convert.ToInt32(cbocompany.SelectedValue));
                }


                string message = " Record Inserted Sucessfully ! ";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
            }

            catch(Exception ex)
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


        public void SendMail(int vcompid )
        {
            string ecode = "";
            string mailid = "";
            string compprefix = "";

            if (vcompid == 2)
            {
                compprefix = "trv";
            }

            if (vcompid == 3)
            {
                compprefix = "ppl";
            }

            if (vcompid == 4)
            {
                compprefix = "pcs";
            }

            if (vcompid == 5)
            {
                compprefix = "pprl";
            }

            if (vcompid == 7)
            {
                compprefix = "PRL";
            }

            if (vcompid == 11)
            {
                compprefix = "TMS";
            }

            if (vcompid == 17)
            {
                compprefix = "plpl";
            }

            try
            {
                    ecode = compprefix+txtcode.Text;
                    mailid = lblmailid.Text;

                    MailMessage mail = new MailMessage();
                    string to_add = mailid;
                    string from_name = Session["UserName"].ToString();

                    mail.To.Add(to_add);
                    mail.CC.Add("vijayakumar.balasubramaniam@pricol.com");

                    mail.From = new MailAddress("pep@pricolcorporate.com", "PEP");
                    //mail.From = new MailAddress("ashok4988@gmail.com");
                    mail.Subject = "PEP Login Creation";

                    string Body = "<p>Dear Associate,</p> <p>Your Login Credentials for accessing PEP Portal are as below :</p><p>URL- www.pricolcorporate.com/pep</p><p>User Name : " + ecode + "</p><p>Password : " + ecode + "</p>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    //mailMessage.Body = "<p style='font-family:verdana; font-size:20'> This is to inform you that File named as <b> " + filename + "</b> has been uploaded by <b> " + uname + "</b> for checking and uploading.</p>  <p style='font-family:verdana; font-size:20'> <b> User Remarks :  " + strremark + "</b> </p> Click on the portal <a href='" + ConfigurationManager.AppSettings["URLTxt"] + "'> DMS-PEIL </a> to  access DMS";
                    //mailMessage.IsBodyHtml = true;


                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.pricolcorporate.com"; //Or Your SMTP Server Address

                    //smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = new System.Net.NetworkCredential("pep@pricolcorporate.com", "Welcome@19");
                    //Or your Smtp Email ID and Password

                    smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    smtp.Send(mail);

                }
            

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {

            }
        }

    }
}