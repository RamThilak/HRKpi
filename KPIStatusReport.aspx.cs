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
    public partial class KPIStatusReport : System.Web.UI.Page
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


                cbomonth.Enabled = false;

                txtsheetname.Text = "";
                cbocompany.Focus();
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("Home.Aspx");
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            int compid;
            int finyr;

            if (cbocompany.SelectedValue == "0")
            {
                MyLabel.Text = "Select Company!";
                return;
            }
            else
            {
                compid = Convert.ToInt32(cbocompany.SelectedValue);
            }

            if (cboyear.SelectedValue == "0")
            {
                MyLabel.Text = "Select Fin Year!";
                return;
            }
            else
            {
                finyr = Convert.ToInt32(cboyear.SelectedValue);
            }


            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid File Name !";
                return;
            }

            if (cboreportname.SelectedValue == "0")
            {
                MyLabel.Text = "Select Report !";
                return;
            }

            if (cboreportname.SelectedValue == "Overall KPI Status")
            {
                GenerateKPIStatusReport(compid, finyr);
            }

            if (cboreportname.SelectedValue == "Group Emlpoyee Experience Report")
            {
                if (cbomonth.SelectedValue == "0")
                {
                    MyLabel.Text = "Select Month!";
                    return;
                }

                GenerateExperienceReport(finyr,Convert.ToInt32(cbomonth.SelectedValue),compid);
            }
            if (cboreportname.SelectedValue == "Annual Objective Evaluation Status")
            {
                GenerateObjectivesStatusReport(compid, finyr);
            }


           
        }

     

        protected void cboreportname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboreportname.SelectedItem.Text == "Overall KPI Status")
            {
                cbomonth.Enabled = false;
            }

            if (cboreportname.SelectedItem.Text == "Group Emlpoyee Experience Report")
            {
                cbomonth.Enabled = true;
                cbomonth.Focus();
            }
        }

        protected void GenerateKPIStatusReport (int compid,int finyr)
        {
            string spname = "";

            if (compid == 2)
            {
                spname = "Trv_Overall_KPI_Status";
            }


            if (compid == 3)
            {
                spname = "PPL_Overall_KPI_Status";
            }

            if (compid == 4)
            {
                spname = "PCS_Overall_KPI_Status";
            }

            if (compid == 7)
            {
                spname = "Retreats_Overall_KPI_Status";
            }

            if (compid == 11)
            {
                spname = "TMS_Overall_KPI_Status";
            }

            if (compid == 12)
            {
                spname = "PEIL_Overall_KPI_Status";
            }

            if (compid == 17)
            {
                spname = "Cargo_Overall_KPI_Status";
            }

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(spname))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", compid);
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }

                    }
                }


                if (dt.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }

                XLWorkbook wb = new XLWorkbook();

                wb = new XLWorkbook(Server.MapPath("~/templates/KPIStatusReport.xlsx"));


                var ws = wb.Worksheet("Sheet1");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(3, 3).Value = "Overall KPI Status For " + cboyear.SelectedItem.Text;
                ws.Cell(3, 11).Value = "As on - " + DateTime.Today;

                int rno = 7;
                int sno = 1;


                foreach (DataRow row in dt.Rows)
                {

                    ws.Cell(rno, 1).Value = sno;
                    ws.Cell(rno, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 2).Value = row["emp_code"].ToString();
                    ws.Cell(rno, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 3).Value = row["emp_name"].ToString();
                    ws.Cell(rno, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 4).Value = row["Q1_Goal_Status"].ToString();
                    ws.Cell(rno, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 5).Value = row["Q1_Self_Evaluation_Status"].ToString();
                    ws.Cell(rno, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 6).Value = row["Q2_Goal_Status"].ToString();
                    ws.Cell(rno, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 7).Value = row["Q2_Self_Evaluation_Status"].ToString();
                    ws.Cell(rno, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 8).Value = row["Q3_Goal_Status"].ToString();
                    ws.Cell(rno, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 9).Value = row["Q3_Self_Evaluation_Status"].ToString();
                    ws.Cell(rno, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 10).Value = row["Q4_Goal_Status"].ToString();
                    ws.Cell(rno, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 11).Value = row["Q4_Self_Evaluation_Status"].ToString();
                    ws.Cell(rno, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    rno = rno + 1;
                    sno = sno + 1;
                }


                dt.Dispose();

                using (wb)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtsheetname.Text + ".xlsx");
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
                MyLabel.Text = ex.ToString();
                return;
            }
        }



        protected void GenerateExperienceReport(int finyr,int monthcode,int cid)
        {
            string constr = ConfigurationManager.ConnectionStrings["PayConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            string spname = "";

            if (cid != 12)
            {
                spname = "Emp_Experience_List";
            }
            else
            {
                spname = "PEIL_Experience_List";
            }

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(spname))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Parameters.AddWithValue("@pay_mnth", monthcode);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }

                    }
                }


                if (dt.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }

                XLWorkbook wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Sheet1");


                ws.Cell(3, 3).Style.Font.Bold = true;
                ws.Cell(3, 3).Value = "Employee Experience List";
                ws.Cell(3, 11).Value = "As on - " + DateTime.Today;

                int rno = 5;
                int sno = 1;


                ws.Cell(rno, 1).Style.Font.Bold = true;
                ws.Cell(rno, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 1).Value = "Company Name";
                ws.Cell(rno, 2).Style.Font.Bold = true;
                ws.Cell(rno, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 2).Value = "Branch Name";
                ws.Cell(rno, 3).Style.Font.Bold = true;
                ws.Cell(rno, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 3).Value = "Emp Code";
                ws.Cell(rno, 4).Style.Font.Bold = true;
                ws.Cell(rno, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 4).Value = "Employee Name";
                ws.Cell(rno, 5).Style.Font.Bold = true;
                ws.Cell(rno, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 5).Value = "DOB";
                ws.Cell(rno, 6).Style.Font.Bold = true;
                ws.Cell(rno, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 6).Value = "DOJ";
                ws.Cell(rno, 7).Style.Font.Bold = true;
                ws.Cell(rno, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 7).Value = "Group Doj";
                ws.Cell(rno, 8).Style.Font.Bold = true;
                ws.Cell(rno, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 8).Value = "Department";
                ws.Cell(rno, 9).Style.Font.Bold = true;
                ws.Cell(rno, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 9).Value = "Designation";
                ws.Cell(rno, 10).Style.Font.Bold = true;
                ws.Cell(rno, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 10).Value = "Grade";
                ws.Cell(rno, 11).Style.Font.Bold = true;
                ws.Cell(rno, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 11).Value = "Experience ( In Months )";                
                ws.Cell(rno, 12).Style.Font.Bold = true;
                ws.Cell(rno, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(rno, 12).Value = "Experience";

                rno = rno + 1;


                foreach (DataRow row in dt.Rows)
                {

                    ws.Cell(rno, 1).Value = row["company"].ToString();
                    ws.Cell(rno, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 2).Value = row["branch"].ToString();
                    ws.Cell(rno, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 3).Value = row["Empcode"].ToString();
                    ws.Cell(rno, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 4).Value = row["Name"].ToString();
                    ws.Cell(rno, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 5).Value = row["DOB"].ToString();
                    ws.Cell(rno, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 6).Value = row["DOJ"].ToString();
                    ws.Cell(rno, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 7).Value = row["Group Doj"].ToString();
                    ws.Cell(rno, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 8).Value = row["Department"].ToString();
                    ws.Cell(rno, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 9).Value = row["Designation"].ToString();
                    ws.Cell(rno, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 10).Value = row["Grade"].ToString();
                    ws.Cell(rno, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 11).Value = row["Total_Experience_in_months"].ToString();
                    ws.Cell(rno, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(rno, 12).Value = row["EXPERIENCE"].ToString();
                    ws.Cell(rno, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rno = rno + 1;
                    sno = sno + 1;
                }


                dt.Dispose();

                using (wb)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtsheetname.Text + ".xlsx");
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
                MyLabel.Text = ex.ToString();
                return;
            }
        }


        protected void GenerateObjectivesStatusReport(int compid, int finyr)
        {
            string spname = "";

            if (compid == 2)
            {
                spname = "Trv_Overall_Objective_Status";
            }


            if (compid == 3)
            {
                spname = "PPL_Overall_Objective_Status";
            }

            if (compid == 4)
            {
                spname = "PCS_Overall_Objective_Status";
            }

            if (compid == 7)
            {
                spname = "MM_Overall_Objective_Status";
            }

            if (compid == 11)
            {
                spname = "TMS_Overall_Objective_Status";
            }

            if (compid == 12)
            {
                spname = "PEIL_Overall_Objective_Status";
            }

            if (compid == 17)
            {
                spname = "Cargo_Overall_Objective_Status";
            }

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(spname))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", compid);
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                        }

                    }
                }


                if (dt.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }

                XLWorkbook wb = new XLWorkbook();

                wb = new XLWorkbook(Server.MapPath("~/templates/GroupAnnualStatus.xlsx"));

                var ws = wb.Worksheet("Sheet1");

                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(2, 3).Value = "Overall Annual Objectives Status For " + cboyear.SelectedItem.Text;
                ws.Cell(2, 6).Value = "As on - " + DateTime.Today;
                ws.Cell(4, 2).InsertData(dt);

                dt.Dispose();


                using (wb)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtsheetname.Text + ".xlsx");
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
                MyLabel.Text = ex.ToString();
                return;
            }
        }


    }

}