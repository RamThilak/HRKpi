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
    public partial class TrainingReport : System.Web.UI.Page
    {
        protected private static Label MyLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            MyLabel = lblerr;

            if (!IsPostBack)
            {
                MyLabel = lblerr;
                lblerr.Text = "";
                txtsheetname.Text = "";

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.Aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("RptProcTrainingReport"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@compid", Convert.ToInt32(cbocompany.SelectedValue));
                            cmd.Parameters.AddWithValue("@fdt", txtfdt.Text);
                            cmd.Parameters.AddWithValue("@tdt", txttdt.Text);
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

                wb = new XLWorkbook(Server.MapPath("~/templates/TrainingReport.xlsx"));

                var ws = wb.Worksheet("Sheet1");
                ws.Cell(1, 1).Value = "*" + cbocompany.SelectedItem.Text + "*";
                ws.Cell(2, 3).Value = "Training Report For " + cbocompany.SelectedItem.Text + " for the period from " + txtfdt.Text + " To " + txttdt.Text;
                ws.Cell(2, 10).Value = " Print Date : " + DateTime.Today;

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