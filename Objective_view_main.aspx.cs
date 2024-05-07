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
    public partial class Objective_view_main : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

                // ClearControls();
                cbocompany.Focus();
            }



        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            string tesst = (e.Row.FindControl("lblstatus") as Label).Text;

               if ((e.Row.FindControl("lblstatus") as Label).Text != "Not yet prepared")
            {
                HyperLink hpy_lnk1 = (HyperLink)e.Row.FindControl("lnk_id");
                hpy_lnk1.Enabled = true;

                HyperLink hpy_lnk2 = (HyperLink)e.Row.FindControl("lnk_name");
                hpy_lnk2.Enabled = true;
               
            }


           
        }

        protected void button_getlist_click(object sender, EventArgs e)
        {
           

            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Company!";
                return;
            }

            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Year!";
                return;
            }

            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Quarter!";
                return;
            }


            int compid = Convert.ToInt32(cbocompany.SelectedValue);
            int yr = Convert.ToInt32(ddl_year.SelectedValue);
          
            Session["Fin_year"] = yr;
            Session["cid"] = compid;          

            DataTable dt2 = new DataTable();

            string kpicriteriatxt = ddl_status.SelectedValue;
            string ratindcriteriatext = ddl_rating.SelectedValue;

            dt2 = GetList(compid,yr,kpicriteriatxt,ratindcriteriatext);

            GridView1.DataSource = dt2;
            GridView1.DataBind();

        }




        protected DataTable GetList(int vcompid, int vyr,string sts,string ratests)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constr))
            {
                //using (SqlCommand cmd = new SqlCommand("Proc_KPI_Objective_Emp_List"))
                using (SqlCommand cmd = new SqlCommand("Proc_KPI_Objective_Emp_List_rating"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@cid", vcompid);
                    cmd.Parameters.AddWithValue("@finyr", vyr);
                    cmd.Parameters.AddWithValue("@kpists", sts);
                    cmd.Parameters.AddWithValue("@ratingsts", ratests);
                    cmd.Connection = concomp;
                    concomp.Open();
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    cmd.Dispose();
                    concomp.Close();
                    concomp.Dispose();
                    return dt;
                }
            }

        }

        protected void button_export_click(object sender, EventArgs e)
        {

         
           
            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Company!";
                return;
            }

            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Year!";
                return;
            }

            if (cbocompany.SelectedValue == "0")
            {
                lblerr.Text = "Choose Quarter!";
                return;
            }


            try
            {

                int compid = Convert.ToInt32(cbocompany.SelectedValue);
                int yr = Convert.ToInt32(ddl_year.SelectedValue);

                Session["Fin_year"] = yr;
                Session["cid"] = compid;

                DataTable dt2 = new DataTable();

                string kpicriteriatxt = ddl_status.SelectedValue;
                string ratindcriteriatext = ddl_rating.SelectedValue;



                dt2 = GetList(compid, yr, kpicriteriatxt, ratindcriteriatext);

                if (dt2.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }

                XLWorkbook wb = new XLWorkbook();

                wb = new XLWorkbook();

                var ws = wb.AddWorksheet("Sheet1");

                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(2, 3).Value = "Overall Annual Objectives Status For " + yr;
                ws.Cell(2, 10).Value = "As on - " + DateTime.Today;

                //foreach (DataRow row in dt2.Rows)
                //{
                // ws.Cell(5, 12).Value = row["empcount"].ToString();



                //}

                ws.Cell(4, 1).InsertTable(dt2);

                dt2.Dispose();


                using (wb)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=status.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.SuppressContent = true;
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        //Response.End();


                    }


                }
            }

            catch (Exception ex)
            {
                lblerr.Text = ex.ToString();
                return;
            }

           
            

        }
    }
}