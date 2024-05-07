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
    public partial class TrainingDetailsEntry : System.Web.UI.Page
    {
        protected private static Label MyLabel;
        protected void Page_Load(object sender, EventArgs e)
        {
            MyLabel = lblerr;

            if (!IsPostBack)
            {
                MyLabel = lblerr;
                lblerr.Text = "";
                Clearcontrols();

                cbocompany.Items.Clear();
                cbocompany.Items.Add(new ListItem("-- Select Company --", "0"));
            


            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["NewPayConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetCompanyList"))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
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

            cbocompany.SelectedValue = "0";
             
            cbocompany.Focus();
            }
        }

        protected void Clearcontrols()
        {
            cbocompany.SelectedValue = "0";
            cbolocation.SelectedValue = "0";
            cbograde.SelectedValue = "0";
            cbodept.SelectedValue = "0";
            cbotype.SelectedValue = "0";
            txttopic.Text = "";
            txthr.Text = "";
            txtsize.Text = "";
            txtunitcost.Text = "";
            txttotcost.Text = "";
            txtfacultyname.Text = "";
            txtremarks.Text = "";
            cbounit.SelectedValue = "0";
            txtvenue.Text = ""; 
        }
        protected void cmdexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.Aspx");
        }

        protected void cbocompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Select Company!";
                return;
            }

            GetDept (Convert.ToInt32(cbocompany.SelectedValue));
            GetGrade(Convert.ToInt32(cbocompany.SelectedValue));
            GetLoc();
        }



        protected void GetDept(int compid)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["NewPayConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetListDetail"))
                {
                    cbodept.Items.Clear();
                    cbodept.Items.Add(new ListItem("-- Select Department --", "0"));

                    cmd1.CommandType = CommandType.StoredProcedure;
                  
                    cmd1.Parameters.AddWithValue("@compid", compid);
                    cmd1.Parameters.AddWithValue("@option", "DEPT");


                    cmd1.Connection = con;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);
                    cbodept.DataTextField = "department_name";
                    cbodept.DataValueField = "department_id";
                    cbodept.DataSource = dt;
                    cbodept.DataBind();
                    dt.Dispose();
                }
            }

            cbodept.SelectedValue = "0";

        }

        protected void GetGrade(int compid)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["NewPayConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetListDetail"))
                {
                    cbograde.Items.Clear();
                    cbograde.Items.Add(new ListItem("-- Select Grade --", "0"));

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@compid", compid);
                    cmd1.Parameters.AddWithValue("@option", "GRADE");

                    cmd1.Connection = con;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);
                    cbograde.DataTextField = "grade_name";
                    cbograde.DataValueField = "grade_id";
                    cbograde.DataSource = dt;
                    cbograde.DataBind();
                    dt.Dispose();
                }
            }

            cbograde.SelectedValue = "0";
        }

        protected void GetLoc()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["NewPayConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetListDetail"))
                {
                    cbolocation.Items.Clear();
                    cbolocation.Items.Add(new ListItem("-- Select Location --", "0"));

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@compid", 0);
                    cmd1.Parameters.AddWithValue("@option", "LOC");

                    cmd1.Connection = con;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);
                    cbolocation.DataTextField = "location_name";
                    cbolocation.DataValueField = "location_id";
                    cbolocation.DataSource = dt;
                    cbolocation.DataBind();
                    dt.Dispose();
                }
            }

            cbolocation.SelectedValue = "0";
        }

        protected void txtunitcost_TextChanged(object sender, EventArgs e)
        {

            if (txtsize.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Batch size !";
                return;
            }

            if (txtsize.Text == "0")
            {
                MyLabel.Text = "Invalid Batch size !";
                return;
            }

            if (txtunitcost.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Cost/Unit !";
                return;
            }

            if (txtunitcost.Text == "0")
            {
                MyLabel.Text = "Invalid Cost/Unit !";
                return;
            }

            if (cbounit.SelectedValue == "Batch")
            {
                txttotcost.Text = txtunitcost.Text;
            }

            if (cbounit.SelectedValue == "Person")
            {
                txttotcost.Text = Convert.ToString(Convert.ToInt32(txtsize.Text) * Convert.ToDouble(txtunitcost.Text));
            }
        }

        protected void cmdsave_Click(object sender, EventArgs e)
        {
            if (cbocompany.SelectedValue == "0")
            {
                MyLabel.Text = "Select Company !";
                return;
            }

            if (cbolocation.SelectedValue == "0")
            {
                MyLabel.Text = "Select Location !";
                return;
            }

            if (cbodept.SelectedValue == "0")
            {
                MyLabel.Text = "Select Department !";
                return;
            }

            if (cbograde.SelectedValue == "0")
            {
                MyLabel.Text = "Select Grade !";
                return;
            }

            if (txttopic.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Topic!";
                return;
            }

            if ((txtdt.Text.Length == 0) || (txtdt.Text.Length != 10))
            {
                MyLabel.Text = "Invalid Training Date!";
                return;
            }

            if ((txthr.Text.Length == 0) || (txthr.Text== "0"))
            {
                MyLabel.Text = "Invalid Duration!";
                return;
            }

            if (cbotype.SelectedValue == "0")
            {
                MyLabel.Text = "Invalid Training Type!";
                return;
            }

            if ((txtsize.Text.Length == 0) || (txtsize.Text == "0"))
            {
                MyLabel.Text = "Invalid Batch Size!";
                return;
            }

            if (txtvenue.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Venue!";
                return;
            }

            if (cbounit.SelectedValue == "0")
            {
                MyLabel.Text = "Invalid Unit !";
                return;
            }

            if ((txtunitcost.Text.Length == 0) || (txtunitcost.Text == "0"))
            {
                MyLabel.Text = "Invalid Unit Cost !";
                return;
            }

            if ((txttotcost.Text.Length == 0) || (txttotcost.Text == "0"))
            {
                MyLabel.Text = "Invalid Total Cost !";
                return;
            }

            if (txtfacultyname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Faculty Name!";
                return;
            }

            if (cbounit.SelectedValue == "Batch")
            {
                txttotcost.Text = txtunitcost.Text;
            }

            if (cbounit.SelectedValue == "Person")
            {
                txttotcost.Text = Convert.ToString(Convert.ToInt32(txtsize.Text) * Convert.ToDouble(txtunitcost.Text));
            }
            
            if (Session["UserName"] == null)
            {
                MyLabel.Text = "Session Expired ! Logout and try again !";
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["NewPayConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);

            con.Open();
            objTrans = con.BeginTransaction();

            SqlCommand cmd = new SqlCommand("InsertTrainingDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = objTrans;
       
            try
            {
            
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                cmd.Parameters.AddWithValue("@locid", Convert.ToInt32(cbolocation.SelectedValue));
                cmd.Parameters.AddWithValue("@deptid", Convert.ToInt32(cbodept.SelectedValue));
                cmd.Parameters.AddWithValue("@gradeid", Convert.ToInt32(cbograde.SelectedValue));
                cmd.Parameters.AddWithValue("@topic", txttopic.Text);
                cmd.Parameters.AddWithValue("@traindate", txtdt.Text);
                cmd.Parameters.AddWithValue("@duration", txthr.Text);
                cmd.Parameters.AddWithValue("@type", cbotype.SelectedValue);
                cmd.Parameters.AddWithValue("@batchsize", txtsize.Text);
                cmd.Parameters.AddWithValue("@venue", txtvenue.Text);
                cmd.Parameters.AddWithValue("@unit", cbounit.SelectedValue);
                cmd.Parameters.AddWithValue("@unitcost", txtunitcost.Text);
                cmd.Parameters.AddWithValue("@totcost", txttotcost.Text);
                cmd.Parameters.AddWithValue("@facultyname", txtfacultyname.Text);
                cmd.Parameters.AddWithValue("@remarks", txtremarks.Text);
                cmd.Parameters.AddWithValue("@username", Session["UserName"].ToString());
                cmd.ExecuteNonQuery();
                objTrans.Commit();
                objTrans.Dispose();
                objTrans = null;
                string message = " Record Inserted Sucessfully !";
                Clearcontrols();
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
                cmd.Dispose();

            }


       }

        protected void txtsize_TextChanged(object sender, EventArgs e)
        {
            if ((cbounit.SelectedValue != "0") && (txtunitcost.Text.Length != 0) && (txtsize.Text.Length != 0) )
            {
                if (cbounit.SelectedValue == "Batch")
                {
                    txttotcost.Text = txtunitcost.Text;
                }

                if (cbounit.SelectedValue == "Person")
                {
                    txttotcost.Text = Convert.ToString(Convert.ToInt32(txtsize.Text) * Convert.ToDouble(txtunitcost.Text));
                }

            }

        }

        protected void cbounit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbounit.SelectedValue != "0") && (txtunitcost.Text.Length != 0) && (txtsize.Text.Length != 0))
            {
                if (cbounit.SelectedValue == "Batch")
                {
                    txttotcost.Text = txtunitcost.Text;
                }

                if (cbounit.SelectedValue == "Person")
                {
                    txttotcost.Text = Convert.ToString(Convert.ToInt32(txtsize.Text) * Convert.ToDouble(txtunitcost.Text));
                }

            }
        }
    }
}