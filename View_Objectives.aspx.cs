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
using ClosedXML.Excel;

namespace HRKpi
{
    public partial class View_Objectives : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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


                cbocompany.SelectedValue = "0";
                DataTable dt1 = new DataTable();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetYearList"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd1);
                        adapter.Fill(dt1);
                        cboyear.DataTextField = "fin_year";
                        cboyear.DataValueField = "fin_year";
                        cboyear.DataSource = dt1;
                        cboyear.DataBind();
                        dt1.Dispose();
                    }
                }


                cbocompany.Focus();
            }
        }

        protected void button_list_Click(object sender, EventArgs e)
        {
            cboemp.Items.Clear();
            cboemp.Items.Add(new ListItem("-- Select Employee --", "0"));

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetEmpList"))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                    cmd1.Connection = con;
                    con.Open();
                    adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);
                    cboemp.DataTextField = "emp_name";
                    cboemp.DataValueField = "emp_code";
                    cboemp.DataSource = dt;
                    cboemp.DataBind();
                    dt.Dispose();
                }
            }
        }

        protected void button_list0_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            lblstatus.Text = "";

            if (cboyear.SelectedValue != "0")
            {
                SqlDataReader dr;
               
         
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd1 = new SqlCommand("GetEmpDetail"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                        cmd1.Parameters.AddWithValue("@empcode", Convert.ToInt32(cboemp.SelectedValue));
                        cmd1.Connection = con;
                        con.Open();
                        dr = cmd1.ExecuteReader();
                        dr.Read();

                        if (dr == null || !dr.HasRows)
                        {

                        }
                        else
                        {
                            lbl_empcode.Text = dr[1].ToString();
                            lbl_name.Text = dr[2].ToString();
                            lbl_dept.Text = dr[8].ToString();
                            lbl_desig.Text = dr[7].ToString();
                            lbl_grade.Text = dr[9].ToString();
                            lbl_doj.Text = dr[3].ToString();


                        }


                        dr.Close();
                    }
                }


            }


            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

        
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("GetObjectives"))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@cid", Convert.ToInt32(cbocompany.SelectedValue));
                    cmd1.Parameters.AddWithValue("@eid", Convert.ToInt32(cboemp.SelectedValue));
                    cmd1.Parameters.AddWithValue("@yr", Convert.ToInt32(cboyear.SelectedValue));

                    cmd1.Connection = con1;
                    con1.Open();
                    adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);

                    GV_KPI.DataSource = dt;
                    GV_KPI.DataBind();
                    dt.Dispose();
                }
            }

            
            SqlConnection con2 = new SqlConnection(constr);
            con2.Open();

            if ((cboyear.SelectedValue == "2021") || (cboyear.SelectedValue == "2022"))
            {

                SqlCommand cmd = new SqlCommand(@"SELECT  distinct (a.emp_code), Status_Id, remarks, reviewer_remarks, approver_remarks
                                FROM KPI_Objective_MSt a   
                                WHERE a.fin_year = '" + cboyear.SelectedValue + "' and emp_code = '" + lbl_empcode.Text + "' and a.company_id = '" + cbocompany.SelectedValue + "'", con2);



                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                   
                    if ((dr["Status_Id"]).ToString() == "1")
                    {
                        lblstatus.Text = "Draft";
                    }

                    if ((dr["Status_Id"]).ToString() == "2")
                    {
                        lblstatus.Text = "Submitted";
                    }

                    if ((dr["Status_Id"]).ToString() == "3")
                    {
                        lblstatus.Text = "Approved";
                    }

                    if ((dr["Status_Id"]).ToString() == "4")
                    {
                        lblstatus.Text = "Resubmit";
                    }

                    if ((dr["Status_Id"]).ToString() == "6")
                    {
                        lblstatus.Text = "Reviewed";
                    }

                    txt_remarks.Text = dr["remarks"].ToString().Replace("<br />", "\r\n");
                    txt_reviewer_remarks.Text = dr["reviewer_remarks"].ToString().Replace("<br />", "\r\n");
                    txt_approver_remarks.Text = dr["approver_remarks"].ToString().Replace("<br />", "\r\n");


                }

                dr.Close();
                con2.Close();

            }


        }
    }
}