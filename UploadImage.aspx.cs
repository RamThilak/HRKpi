using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;


namespace HRKpi
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected private static Label MyLabel;
        protected void Page_Load(object sender, EventArgs e)
        {
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

                txtcode.Text = "";
                cbocompany.SelectedValue = "0";
                lblname.Text = "";

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
            
            if (cbocompany.SelectedValue == "0")
            {
                MyLabel.Text = "Select Employee!";
                return;
            }

            if (txtcode.Text.Length == 0)
            {
                MyLabel.Text = "Enter Employee Code!";
                return;
            }

            DataTable dt = new DataTable();

            dt = GetEmpDetail(Convert.ToInt32(cbocompany.SelectedValue), txtcode.Text);

            if (dt.Rows.Count != 0)
            {
                lblname.Text = dt.Rows[0].Field<string>(0);
                
            }

            if (lblname.Text == "NoRecord")
            {
                MyLabel.Text = "No Such Employee Code!";
                return;
            }

            MyLabel.Text = "";
            Image2.ImageUrl = "~/images/blank_photo.jpg";
            lblfilename.Text = "";
            lblpath.Text = "";

            GetExistingImage();


        }


        protected void GetExistingImage()
        {

            string localfilepath = Server.MapPath("~/temp");
            string actualfilepathPhoto = ConfigurationManager.AppSettings["AbsPhotoPath"].ToString();

            int compid = Convert.ToInt32(cbocompany.SelectedValue);
            string comp_shortname = "";

            if (compid == 2)
            {
                comp_shortname = "TRV";
            }

            if (compid == 3)
            {
                comp_shortname = "PPL";
            }
            if (compid == 4)
            {
                comp_shortname = "PCS";
            }
            if (compid == 5)
            {
                comp_shortname = "PPRL";
            }
            if (compid == 7)
            {
                comp_shortname = "PGL";
            }
            if (compid == 11)
            {
                comp_shortname = "TMS";
            }

            if (compid == 12)
            {
                comp_shortname = "PEIL";
            }

            if (compid == 17)
            {
                comp_shortname = "PLPL";
            }

          
            actualfilepathPhoto = actualfilepathPhoto + comp_shortname;


            if (File.Exists(actualfilepathPhoto + "\\" + txtcode.Text + ".JPG"))
            {
                File.Delete(localfilepath + "\\" + txtcode.Text + ".JPG");
                File.Copy(actualfilepathPhoto + "\\" + txtcode.Text + ".JPG", localfilepath + "\\" + txtcode.Text + ".JPG", true);
                Image2.ImageUrl = "~/temp/" + txtcode.Text + ".JPG";

            }

            else
            {
                Image2.ImageUrl = "~/images/blank_photo.jpg";
                lblfilename.Text = "";
                lblpath.Text = "";
            }
        }

        protected void button_add_Click(object sender, EventArgs e)
        {
            string filepath = "";
            string localfilepath = Server.MapPath("~/temp");
            string actualfilepathPhoto = ConfigurationManager.AppSettings["AbsPhotoPath"].ToString();
            filepath = ConfigurationManager.AppSettings["PhotoPath"].ToString();

            int compid = Convert.ToInt32(cbocompany.SelectedValue);
            string comp_shortname = "";

            if (compid == 2)
            {
                comp_shortname = "TRV";
            }

            if (compid == 3)
            {
                comp_shortname = "PPL";
            }
            if (compid == 4)
            {
                comp_shortname = "PCS";
            }
            if (compid == 5)
            {
                comp_shortname = "PPRL";
            }
            if (compid == 7)
            {
                comp_shortname = "PGL";
            }
            if (compid == 11)
            {
                comp_shortname = "TMS";
            }

            if (compid == 12)
            {
                comp_shortname = "PEIL";
            }

            if (compid == 17)
            {
                comp_shortname = "PLPL";
            }

            filepath = filepath + comp_shortname;
            actualfilepathPhoto = actualfilepathPhoto + comp_shortname;

            if (FileUpload1.HasFile)
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(filepath + "\\"+ txtcode.Text + ".JPG");

                    if (File.Exists(filepath +"\\"+ txtcode.Text + ".JPG"))
                    {
                        File.Delete(localfilepath + "\\" + txtcode.Text + ".JPG");
                        File.Copy(actualfilepathPhoto + "\\" + txtcode.Text + ".JPG", localfilepath + "\\" + txtcode.Text + ".JPG", true);
                        Image2.ImageUrl = "~/temp/" + txtcode.Text + ".JPG";
                        MyLabel.Text = "File Uploaded Sucessfully !";
                        ShowMessage("File Uploaded Successfully!");
                    }
                    else
                    {
                        MyLabel.Text = "Error In Operation !";
                        return;
                    }
                }

                catch (UnauthorizedAccessException err)
                {


                    MyLabel.Text = err.Message;
                    return;
                }

                catch (ArgumentException err)
                {

                    MyLabel.Text = err.Message;
                    return;
                }

                catch (PathTooLongException err)
                {

                    MyLabel.Text = err.Message;
                    return;
                }

                catch (DirectoryNotFoundException err)
                {

                    MyLabel.Text = err.Message;
                    return;
                }
                catch (FileNotFoundException err)
                {
                    MyLabel.Text = err.Message;
                    return;
                }

                catch (IOException)
                {

                    MyLabel.Text = "File already exists !";
                    return;
                }

                catch (NotSupportedException err)
                {

                    MyLabel.Text = err.Message;
                    return;
                }

                catch (Exception msg)
                {
                    MyLabel.Text = msg.ToString();
                    return;
                }

                finally
                {
                    lblpath.Text = "";
                    lblfilename.Text = "";
                    //Mylabel.Text = "";
                    txtcode.Text = "";
                    lblname.Text = "";

                }

            }




       }
               

    private void ShowMessage(string Message)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("alert('");
        //sb.Append(count.ToString());
        sb.Append(Message + "');");
        sb.Append("</script>");
        //ScriptManager.RegisterStartupScript(this.Page,this.GetType(), "script", sb.ToString(),true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');", Message), true);
    }

        protected void cbocompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image2.ImageUrl = "~/images/blank_photo.jpg";
            lblfilename.Text = "";
            lblpath.Text = "";
        }
    }
}