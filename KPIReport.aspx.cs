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
    public partial class KPIReport : System.Web.UI.Page
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



                txtsheetname.Text = "";
                cbocompany.Focus();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            int compid1;
            int finyr1;

            if (cbocompany.SelectedValue == "0")
            {
                MyLabel.Text = "Select Company!";
                return;
            }
            else
            {
                compid1 = Convert.ToInt32(cbocompany.SelectedValue);
            }

            if (cboyear.SelectedValue == "0")
            {
                MyLabel.Text = "Select Fin Year!";
                return;
            }
            else
            {
                finyr1 = Convert.ToInt32(cboyear.SelectedValue);
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


            if (cboreportname.SelectedValue == "Overall Locationwise Quarter Wise  Annual Objectives Status")
            {
                StatusReport(compid1, finyr1);
            }

            if (cboreportname.SelectedValue == "Employee Master Report")
            {
                MasterReport();
            }

            if (cboreportname.SelectedValue == "Annual Objective Evaluation Status")
            {
                AnnualObjectivesStatusReport(compid1, finyr1);
            }
        }


        protected void MasterReport()
        {


            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("RptProcESDEmployeeMaster"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@flg", 'A');
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

                IXLWorksheet ws = wb.Worksheets.Add("Active");

                ws = wb.Worksheet("Active");

                ws.Cell(2, 1).InsertTable(dt);

                dt.Dispose();

                ws = wb.Worksheets.Add("Inactive");

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("RptProcESDEmployeeMaster"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@flg", 'I');
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

                ws.Cell(2, 1).InsertTable(dt);

                dt.Dispose();

                using (wb)
                {

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtsheetname.Text+".xlsx");
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


        protected void AnnualObjectivesStatusReport(int compid,int finyr)
        {


            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ESD_Overall_Objective_status"))
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

                wb = new XLWorkbook(Server.MapPath("~/templates/AnnualObjectiveStatus.xlsx"));
               
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


        protected void StatusReport(int compid,int finyr)
           {
            

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ESD_Overall_KPI_status"))
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

                wb = new XLWorkbook(Server.MapPath("~/templates/StatusReport.xlsx"));


                var ws = wb.Worksheet("Abstract");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(3, 2).Value = "Overall KPI Annual Objectives Status For " + cboyear.SelectedItem.Text;


                int rno = 7;
                int sno = 1;


                foreach (DataRow row in dt.Rows)
                {

                    ws.Cell(rno, 1).Value = sno.ToString();
                    ws.Cell(rno, 2).Value = row["locationname"].ToString();
                    ws.Cell(rno, 3).Value = Convert.ToInt32(row["q1_empcount"].ToString());
                    ws.Cell(rno, 4).Value = Convert.ToInt32(row["q1_noaction"].ToString());
                    ws.Cell(rno, 5).Value = Convert.ToInt32(row["q1_draft"].ToString());
                    ws.Cell(rno, 6).Value = Convert.ToInt32(row["q1_submit"].ToString());
                    ws.Cell(rno, 7).Value = Convert.ToInt32(row["q1_resubmit"].ToString());
                    ws.Cell(rno, 8).Value = Convert.ToInt32(row["q1_approved"].ToString());
                    ws.Cell(rno, 9).Value = Convert.ToInt32(row["q1_reviewed"].ToString());
                    ws.Cell(rno, 10).Value = Convert.ToInt32(row["q1_revaluation"].ToString());
                    ws.Cell(rno, 11).Value = Convert.ToInt32(row["q1_cancel"].ToString());
                    ws.Cell(rno, 12).Value = Convert.ToInt32(row["q1_dispute"].ToString());
                    ws.Cell(rno, 13).Value = Convert.ToInt32(row["q1_rejected"].ToString());

                    ws.Cell(rno, 14).Value = Convert.ToInt32(row["q2_empcount"].ToString());
                    ws.Cell(rno, 15).Value = Convert.ToInt32(row["q2_noaction"].ToString());
                    ws.Cell(rno, 16).Value = Convert.ToInt32(row["q2_draft"].ToString());
                    ws.Cell(rno, 17).Value = Convert.ToInt32(row["q2_submit"].ToString());
                    ws.Cell(rno, 18).Value = Convert.ToInt32(row["q2_resubmit"].ToString());
                    ws.Cell(rno, 19).Value = Convert.ToInt32(row["q2_approved"].ToString());
                    ws.Cell(rno, 20).Value = Convert.ToInt32(row["q2_reviewed"].ToString());
                    ws.Cell(rno, 21).Value = Convert.ToInt32(row["q2_revaluation"].ToString());
                    ws.Cell(rno, 22).Value = Convert.ToInt32(row["q2_cancel"].ToString());
                    ws.Cell(rno, 23).Value = Convert.ToInt32(row["q2_dispute"].ToString());
                    ws.Cell(rno, 24).Value = Convert.ToInt32(row["q2_rejected"].ToString());

                    ws.Cell(rno, 25).Value = Convert.ToInt32(row["q3_empcount"].ToString());
                    ws.Cell(rno, 26).Value = Convert.ToInt32(row["q3_noaction"].ToString());
                    ws.Cell(rno, 27).Value = Convert.ToInt32(row["q3_draft"].ToString());
                    ws.Cell(rno, 28).Value = Convert.ToInt32(row["q3_submit"].ToString());
                    ws.Cell(rno, 29).Value = Convert.ToInt32(row["q3_resubmit"].ToString());
                    ws.Cell(rno, 30).Value = Convert.ToInt32(row["q3_approved"].ToString());
                    ws.Cell(rno, 31).Value = Convert.ToInt32(row["q3_reviewed"].ToString());
                    ws.Cell(rno, 32).Value = Convert.ToInt32(row["q3_revaluation"].ToString());
                    ws.Cell(rno, 33).Value = Convert.ToInt32(row["q3_cancel"].ToString());
                    ws.Cell(rno, 34).Value = Convert.ToInt32(row["q3_dispute"].ToString());
                    ws.Cell(rno, 35).Value = Convert.ToInt32(row["q3_rejected"].ToString());

                    ws.Cell(rno, 36).Value = Convert.ToInt32(row["q4_empcount"].ToString());
                    ws.Cell(rno, 37).Value = Convert.ToInt32(row["q4_noaction"].ToString());
                    ws.Cell(rno, 38).Value = Convert.ToInt32(row["q4_draft"].ToString());
                    ws.Cell(rno, 39).Value = Convert.ToInt32(row["q4_submit"].ToString());
                    ws.Cell(rno, 40).Value = Convert.ToInt32(row["q4_resubmit"].ToString());
                    ws.Cell(rno, 41).Value = Convert.ToInt32(row["q4_approved"].ToString());
                    ws.Cell(rno, 42).Value = Convert.ToInt32(row["q4_reviewed"].ToString());
                    ws.Cell(rno, 43).Value = Convert.ToInt32(row["q4_revaluation"].ToString());
                    ws.Cell(rno, 44).Value = Convert.ToInt32(row["q4_cancel"].ToString());
                    ws.Cell(rno, 45).Value = Convert.ToInt32(row["q4_dispute"].ToString());
                    ws.Cell(rno, 46).Value = Convert.ToInt32(row["q4_rejected"].ToString());
                    rno = rno + 1;
                }

                
                dt.Dispose();

                ws = wb.AddWorksheet("Sheet1");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(3, 2).Value = "Quarterwise Eligible Employee List For " + cboyear.SelectedItem.Text;

                DataTable dt1 = new DataTable();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ESD_Overall_KPI_status_EligEmpList"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", compid);
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt1);
                        }

                    }
                }


                if (dt1.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }


                 rno = 7;
                 sno = 1;


                foreach (DataRow row in dt1.Rows)
                {

                    ws.Cell(rno, 1).Value = sno.ToString();
                    ws.Cell(rno, 2).Value = row["locationname"].ToString();
                    ws.Cell(rno, 3).Value = row["qno"].ToString();
                    ws.Cell(rno, 4).Value = row["emp_code"].ToString();
                    ws.Cell(rno, 5).Value = row["emp_name"].ToString();
                    rno = rno + 1;
                    sno = sno + 1;

                }

                dt1.Dispose();


                ws = wb.AddWorksheet("Sheet2");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(3, 2).Value = "Quarterwise Employee List Who Have Made No Entry For " + cboyear.SelectedItem.Text;

                DataTable dt2 = new DataTable();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ESD_Overall_KPI_status_NoactionList"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", compid);
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt2);
                        }

                    }
                }


                if (dt2.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }


                rno = 7;
                sno = 1;


                foreach (DataRow row in dt2.Rows)
                {

                    ws.Cell(rno, 1).Value = sno.ToString();
                    ws.Cell(rno, 2).Value = row["locationname"].ToString();
                    ws.Cell(rno, 3).Value = row["kpiperiod"].ToString();
                    ws.Cell(rno, 4).Value = row["emp_code"].ToString();
                    ws.Cell(rno, 5).Value = row["emp_name"].ToString();
                    rno = rno + 1;
                    sno = sno + 1;

                }

                dt2.Dispose();


                ws = wb.AddWorksheet("sheet3");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(3, 2).Value = "Quarterwise Employee Entry List For " + cboyear.SelectedItem.Text;

                DataTable dt3 = new DataTable();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ESD_Overall_KPI_status_EntryEmpList"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", compid);
                            cmd.Parameters.AddWithValue("@finyr", finyr);
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt3);
                        }

                    }
                }


                if (dt3.Rows.Count == 0)
                {
                    lblerr.Text = "No Records Found !";
                    return;
                }


                rno = 7;
                sno = 1;


                foreach (DataRow row in dt3.Rows)
                {

                    ws.Cell(rno, 1).Value = sno.ToString();
                    ws.Cell(rno, 2).Value = row["locationname"].ToString();
                    ws.Cell(rno, 3).Value = row["kpi_period"].ToString();
                    ws.Cell(rno, 4).Value = row["emp_code"].ToString();
                    ws.Cell(rno, 5).Value = row["emp_name"].ToString();
                    ws.Cell(rno, 6).Value = row["status"].ToString();
                    rno = rno + 1;
                    sno = sno + 1;

                }

                dt3.Dispose();

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

        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("Home.Aspx");
        }

        }
}