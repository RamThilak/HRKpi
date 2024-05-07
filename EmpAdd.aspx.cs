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
using System.Net.Mail;
using System.Net;
using System.Net.Security;

namespace HRKpi
{
    public partial class EmpAdd : System.Web.UI.Page
    {
        protected private static Label MyLabel;
        int hr1company = 0;
        int hr1code = 0;
        int hr2company = 0;
        int hr2code = 0;
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
                txtsheetname.Text = "";
                cbocompany.Focus();
            }
        }

        protected void cmdExit_click(object sender, EventArgs e)
        {
            Response.Redirect("Home.Aspx");
        }


        protected void StopTransction(SqlConnection vcon, SqlTransaction vtransaction)
        {
            if (vtransaction != null)
            {
                vtransaction.Rollback();
                vtransaction.Dispose();
                vtransaction = null;
            }

            if (vcon.State == ConnectionState.Open)
            {
                vcon.Close();
            }

        }

        protected void cmdSave_click(object sender, EventArgs e)
        {
            int compid = 0;
            int empcode = 0;
            string empname;
            string doj;
            string doc;
            int locid = 0;
            int deptid = 0;
            int desigid = 0;
            int bandid = 0;
            string mailid;
            int cnt = 0;
            string approver1company;
            int approver1companyid = 0;
            int approver1 = 0;
            string approver2company;
            int approver2companyid = 0;
            int approver2 = 0;
            string grpname;
            string verticalname;
            string functionname;
            string edudetail;
            int grpid = 0;
            int verticalid = 0;
            int functionid = 0;


            string locname;
            string deptname;
            string designame;
            string bandname;
            string dob = "";


            compid = Convert.ToInt32(cbocompany.SelectedValue);


            GetHrCodes(compid);

            if (compid == 0)
            {
                MyLabel.Text = "Invalid Company !";
                return;
            }
           

            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Sheet Name !";
                return;
            }



            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);

          

            try

            {

                if (FileUpload1.HasFile)

                {
                    string FileName = FileUpload1.FileName;

                    string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

                    FileUpload1.PostedFile.SaveAs(path);

                    var wb = new XLWorkbook(path);
                    var ws = wb.Worksheet(txtsheetname.Text);

                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("InsertUploadEmployee");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;
                    
                    
                    foreach (var row in table.DataRange.Rows())
                    {

                        empcode = 0;
                        empname = "";
                        doj = "";
                        doc ="";
                        locid = 0;
                        deptid = 0;
                        desigid = 0;
                        bandid = 0;
                        mailid = "";
                        approver1company="";
                        approver1companyid = 0;
                        approver1 = 0;
                        approver2company = "";
                        approver2companyid = 0;
                        approver2 = 0;

                        locname="";
                        deptname="";
                        designame="";
                        bandname="";
                        dob = "";
                        grpname = "";
                        functionname = "";
                        verticalname = "";
                        edudetail = "";
                        grpid = 0;
                        functionid = 0;
                        verticalid = 0;

                        if (row.Field("emp_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_code");
                        }
                        else
                        {
                            empcode = Convert.ToInt32(row.Field("emp_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid,empcode) == 1)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Already Exist In Employee Master !";
                                return;
                            }

                        }

                        if (row.Field("emp_name").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_name");
                        }
                        else
                        {
                            empname = row.Field("emp_name").GetString();
                        }


                        if (row.Field("dob").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid DOB for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            string date = DateTime.Parse(row.Field("dob").GetString()).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid DOB for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }
                            else
                            {
                                //dob = row.Field("dob").GetString();
                                dob = DateTime.Parse(row.Field("dob").GetString()).ToString("dd/MM/yyyy");
                            }
                        }


                        if (row.Field("doj").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid DOJ for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            string date = DateTime.Parse(row.Field("doj").GetString()).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid DOJ for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }
                            else
                            {
                                //dob = row.Field("dob").GetString();
                                doj = DateTime.Parse(row.Field("doj").GetString()).ToString("dd/MM/yyyy");
                            }
                        }

                        if (row.Field("group_doj").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Group DOJ for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            string date = DateTime.Parse(row.Field("group_doj").GetString()).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid Group DOJ for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }
                            else
                            {
                                //dob = row.Field("dob").GetString();
                                doc = DateTime.Parse(row.Field("group_doj").GetString()).ToString("dd/MM/yyyy");
                            }
                        }


                        if (row.Field("location").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Location for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            locname = row.Field("location").GetString();
                            locid = GetLocationId(compid, locname);

                            if (locid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Location Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }


                        if (row.Field("department").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Department for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                           deptname = row.Field("department").GetString();
                           deptid = GetDeptId(compid, deptname);

                            if (deptid== 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Department Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }

                        if (row.Field("designation").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Designation for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            designame = row.Field("designation").GetString();
                            desigid = GetDesigId(compid, designame);

                            if (desigid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Designation Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }


                        if (row.Field("band").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Band for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            bandname = row.Field("band").GetString();
                            bandid = GetBandId(compid, bandname);

                            if (bandid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Band Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }


                        if (row.Field("group").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Group for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            grpname = row.Field("group").GetString();
                            grpid = GetGroupId(compid, grpname);

                            if (grpid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Group Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }

                        if (row.Field("vertical").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Vertical for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            verticalname = row.Field("vertical").GetString();
                            verticalid = GetVerticalId(compid, verticalname);

                            if (verticalid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Vertical Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }

                        if (row.Field("function").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Function for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            functionname = row.Field("function").GetString();
                            functionid = GetFunctionId(compid, functionname);

                            if (verticalid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Vertical Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }

                        if (row.Field("EduDetail").GetString() == "")
                        {
                            edudetail = "";
                        }
                     
                        if (row.Field("mailid").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid mail id  for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            mailid = row.Field("mailid").GetString();
                        }

                        if (row.Field("approver1_company").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Approver 1 Company for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            approver1company = row.Field("approver1_company").GetString();
                            approver1companyid = GetCompanyId(approver1company);

                            if (approver1companyid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver 1 Company Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }


                        if (row.Field("approver1_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Approver 1 Code for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            approver1  = Convert.ToInt32(row.Field("approver1_code").GetString());

                            if (CheckEmpExist(approver1companyid, approver1) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver 1 Code for Employee " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }                          

                        }


                        if (row.Field("approver2_company").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Approver 2 Company for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            approver2company = row.Field("approver2_company").GetString();
                            approver2companyid = GetCompanyId(approver2company);

                            if (approver2companyid == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver 2 Company Not found in Master for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }

                        }

                        if (row.Field("approver2_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid Approver 2 Code for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            approver2 = Convert.ToInt32(row.Field("approver2_code").GetString());

                            if (CheckEmpExist(approver2companyid, approver2) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver 2 Code for Employee " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }

                        }
                                               


                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@compid", compid);
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                        cmd.Parameters.AddWithValue("@emp_name", empname);
                        cmd.Parameters.AddWithValue("@doj", doj);
                        cmd.Parameters.AddWithValue("@groupdoj",doc);
                        cmd.Parameters.AddWithValue("@locid", locid);
                        cmd.Parameters.AddWithValue("@deptid", deptid);
                        cmd.Parameters.AddWithValue("@desigid", desigid);
                        cmd.Parameters.AddWithValue("@bandid", bandid);
                        cmd.Parameters.AddWithValue("@mailid", mailid);
                        cmd.Parameters.AddWithValue("@approver_comp1", approver1companyid);
                        cmd.Parameters.AddWithValue("@approver1", approver1);
                        cmd.Parameters.AddWithValue("@approver_comp2", approver2companyid);
                        cmd.Parameters.AddWithValue("@approver2", approver2);
                        cmd.Parameters.AddWithValue("@hr1companyid",hr1company);
                        cmd.Parameters.AddWithValue("@hr1code", hr1code);
                        cmd.Parameters.AddWithValue("@hr2companyid", hr2company);
                        cmd.Parameters.AddWithValue("@hr2code", hr2code);
                        cmd.Parameters.AddWithValue("@dob", dob);

                        cmd.Parameters.AddWithValue("@grpid", grpid);
                        cmd.Parameters.AddWithValue("@verticalid", verticalid);
                        cmd.Parameters.AddWithValue("@functionid", functionid);
                        cmd.Parameters.AddWithValue("@edudetail", edudetail);
                        cmd.ExecuteNonQuery();
                        
                        cnt = cnt + 1;

                    }

                    try
                    {
                        objTrans.Commit();
                        objTrans.Dispose();
                        objTrans = null;

                        SendMail();


                        string message = " Record(s) Inserted Sucessfully ! Total Records Uploaded : " + cnt;
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
                        txtsheetname.Text = "";
                    }


                }



            }

            catch (Exception msg)

            {

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

                MyLabel.Text = msg.ToString();



                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("')};");
                sb.Append("</script>");

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


            }

            finally
            {


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }




        }

        protected void validatenull(int rno, string colnm)
        {
            MyLabel.Text = "Null value in  column " + colnm + " in Row no " + Convert.ToString(rno);
            return;
        }

        protected int CheckEmpExist(int compid,int ecode)
        {
            int empexist = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conemp = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("CheckEmployeeExist"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@ecode", ecode);
                    cmd.Connection = conemp;
                    conemp.Open();
                    empexist = Convert.ToInt32(cmd.ExecuteScalar());
                   // conemp.Close();
                    return empexist;
                }
            }


        }


        protected int CheckCompanyExist(int compid)
        {
            int compexist = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conemp = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("CheckCompanyExist"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                   
                    cmd.Connection = conemp;
                    conemp.Open();
                    compexist = Convert.ToInt32(cmd.ExecuteScalar());
                    // conemp.Close();
                    return compexist;
                }
            }


        }

        protected int CheckStatus(int compid,int fyr, int ecode)
        {
            int sts = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conemp = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetObjectiveStatus"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@finyr", fyr);
                    cmd.Parameters.AddWithValue("@empcode", ecode);
                    cmd.Connection = conemp;
                    conemp.Open();
                    sts = Convert.ToInt32(cmd.ExecuteScalar());
                    // conemp.Close();
                    return sts;
                }
            }

        }
        protected int GetLocationId(int compid, string nm)
        {
            int location = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conloc = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Getlocationid"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = conloc;
                    conloc.Open();
                   location = Convert.ToInt32(cmd.ExecuteScalar());
                  //  conloc.Close();
                    return location;
                }
            }

        }

        protected int GetDeptId(int compid, string nm)
        {
            int dept = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection condept = new SqlConnection(constr);

            SqlCommand cmd = new SqlCommand("Getdeptid");
            condept.Open();
           
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@comp", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = condept;
                    dept = Convert.ToInt32(cmd.ExecuteScalar());
                    condept.Close();
                    return dept;
               
         

        }


        protected int GetDesigId(int compid, string nm)
        {
            int desig = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection condesig = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetDesigid"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@comp", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = condesig;
                    condesig.Open();
                    desig = Convert.ToInt32(cmd.ExecuteScalar());
                   // condesig.Close();
                    return desig;
                }
            }

        }

        protected int GetBandId(int compid, string nm)
        {
            int band = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conband = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetBandid"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@comp", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = conband;
                    conband.Open();
                    band= Convert.ToInt32(cmd.ExecuteScalar());
                   // conband.Close();
                    return band;
                }
            }

        }


        protected int GetCompanyId(string nm)
        {
            int compid = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetCompanyId"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = concomp;
                    concomp.Open();
                    compid = Convert.ToInt32(cmd.ExecuteScalar());
                   // concomp.Close();
                    return compid;
                }
            }

        }

        protected int GetGroupId(int compid, string nm)
        {
            int location = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conloc = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetGroupid"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = conloc;
                    conloc.Open();
                    location = Convert.ToInt32(cmd.ExecuteScalar());
                    //  conloc.Close();
                    return location;
                }
            }

        }

        protected int GetFunctionId(int compid, string nm)
        {
            int location = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conloc = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetFunctionid"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = conloc;
                    conloc.Open();
                    location = Convert.ToInt32(cmd.ExecuteScalar());
                    //  conloc.Close();
                    return location;
                }
            }

        }

        protected int GetVerticalId(int compid, string nm)
        {
            int location = 0;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conloc = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetVerticalId"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@nm", nm);
                    cmd.Connection = conloc;
                    conloc.Open();
                    location = Convert.ToInt32(cmd.ExecuteScalar());
                    //  conloc.Close();
                    return location;
                }
            }

        }
        protected string GetActiveStatus(int compid,int empid)
        {
            string flg;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("GetActiveStatus"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@empcode", empid);

                    cmd.Connection = concomp;
                    concomp.Open();
                    flg = Convert.ToString(cmd.ExecuteScalar());
                    // concomp.Close();
                    return flg;
                }
            }

        }
        private bool ValidateDate(string date)

        {

            try
            {
                string[] dateParts = date.Split('/');
                DateTime testDate = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]));
                return true;
            }

            catch
            {
                return false;
            }

        }


        protected void GetHrCodes(int compid)
        {
            DataTable dtfactor = new DataTable();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conhr = new SqlConnection(constr))
            {
                using (SqlCommand cmdhr = new SqlCommand("GetHrCodes"))
                {
                    cmdhr.CommandType = CommandType.StoredProcedure;
                    cmdhr.Parameters.AddWithValue("@compid",compid);
                    cmdhr.Connection = conhr;
                    conhr.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdhr))
                    {
                        da.Fill(dtfactor);
                    }

                    if (dtfactor.Rows.Count != 0)
                    {
                        foreach (DataRow factorrow in dtfactor.Rows)
                        {
                           hr1company = Convert.ToInt32(factorrow["hr1company"]);
                           hr1code = Convert.ToInt32(factorrow["hr1code"]);
                           hr2company = Convert.ToInt32(factorrow["hr2company"]);
                           hr2code = Convert.ToInt32(factorrow["hr2code"]);

                        }
                    }

                    else
                    {
                        hr1code = 0;
                        hr1company = 0;
                        hr2code = 0;
                        hr2company = 0;
                    }


                  //  conhr.Close();

                }
            }

        }


        public void SendMail()
        {
            string ecode = "";
            string mailid = "";

            string FileName = FileUpload1.FileName;

            string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

            FileUpload1.PostedFile.SaveAs(path);

            var wb = new XLWorkbook(path);
            var ws = wb.Worksheet(txtsheetname.Text);

            var range = ws.RangeUsed();
            var table = range.AsTable();

            try 
            {
                foreach (var row in table.DataRange.Rows())
                {

                    ecode = 'E' + row.Field("emp_code").GetString();
                    mailid = row.Field("mailid").GetString();

                    MailMessage mail = new MailMessage();
                    string to_add = mailid;
                    string from_name = Session["UserName"].ToString();

                    mail.To.Add(to_add);

                    mail.CC.Add("vijayakumar.balasubramaniam @pricol.com");

                    mail.From = new MailAddress("pep@pricolcorporate.com", "KPI");
                    //mail.From = new MailAddress("ashok4988@gmail.com");
                    mail.Subject = "PMS Login Creation";

                    string Body = "<p>Dear Associate,</p> <p>Your Login Credentials for accessing PMS Portal are as below :</p><p>URL- 113.193.22.82/KPI</p><p>User Name : " + ecode + "</p><p>Password : " + ecode +"</p>";
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
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                
            }
        }

        protected void cmdResign_click(object sender, EventArgs e)
        {

            int compid = 0;
            int empcode = 0;
            string dol;
            string dor;
            string reason;
            int cnt = 0;

            compid = Convert.ToInt32(cbocompany.SelectedValue);


            if (compid == 0)
            {
                MyLabel.Text = "Invalid Company !";
                return;
            }
           

            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Sheet Name !";
                return;
            }



            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);



            try

            {

                if (FileUpload1.HasFile)

                {
                    string FileName = FileUpload1.FileName;

                    string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

                    FileUpload1.PostedFile.SaveAs(path);

                    var wb = new XLWorkbook(path);
                    var ws = wb.Worksheet(txtsheetname.Text);

                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("InsertResignation");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;


                    foreach (var row in table.DataRange.Rows())
                    {

                        empcode = 0;
                        reason = "";
                        dor = "";
                        dol = "";
                        
                        if (row.Field("emp_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_code");
                        }
                        else
                        {
                            empcode = Convert.ToInt32(row.Field("emp_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, empcode) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }

                        }



                        if (row.Field("dor").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid DOR for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            string date = DateTime.Parse(row.Field("dor").GetString()).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid DOR for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }
                            else
                            {
                                //dob = row.Field("dob").GetString();
                                dor = DateTime.Parse(row.Field("dor").GetString()).ToString("dd/MM/yyyy");
                            }
                        }

                        if (row.Field("dol").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid DOL for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            string date = DateTime.Parse(row.Field("dol").GetString()).ToString("dd/MM/yyyy");

                            if (!ValidateDate(date))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid DOL for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                                return;
                            }
                            else
                            {
                                //dob = row.Field("dob").GetString();
                                dol = DateTime.Parse(row.Field("dol").GetString()).ToString("dd/MM/yyyy");
                            }
                        }

                        if (row.Field("reason").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid reason for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            reason = row.Field("reason").GetString();
                        }



                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@compid", compid);
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                        cmd.Parameters.AddWithValue("@dor", dor);
                        cmd.Parameters.AddWithValue("@dol", dol);
                        cmd.Parameters.AddWithValue("@reason", reason);
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                        cmd.ExecuteNonQuery();

                        cnt = cnt + 1;

                    }

                    try
                    {
                        objTrans.Commit();
                        objTrans.Dispose();
                        objTrans = null;

                        SendMail();


                        string message = " Record(s) Inserted Sucessfully ! Total Records Uploaded : " + cnt;
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
                        txtsheetname.Text = "";
                    }


                }



            }

            catch (Exception msg)

            {

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

                MyLabel.Text = msg.ToString();



                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("')};");
                sb.Append("</script>");

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


            }

            finally
            {


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }




        }



        protected void cmdOpen_click(object sender, EventArgs e)
        {

            int compid = 0;
            int finyr = 0;
            int empcode = 0;
            string reason;
            int cnt = 0;
            int sts = 0;

            compid = Convert.ToInt32(cbocompany.SelectedValue);


            if (compid == 0)
            {
                MyLabel.Text = "Invalid Company !";
                return;
            }
            

            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Sheet Name !";
                return;
            }



            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);



            try

            {

                if (FileUpload1.HasFile)

                {
                    string FileName = FileUpload1.FileName;

                    string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

                    FileUpload1.PostedFile.SaveAs(path);

                    var wb = new XLWorkbook(path);
                    var ws = wb.Worksheet(txtsheetname.Text);

                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("InsertReleaseAnnualObjectiveLog");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;


                    foreach (var row in table.DataRange.Rows())
                    {
                        finyr = 0;
                        empcode = 0;
                        reason = "";
                        sts = 0;

                        if (row.Field("fin_year").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "Fin Year");
                        }
                        else
                        {
                            finyr = Convert.ToInt32(row.Field("fin_year").GetString());
                        }


                        if (row.Field("emp_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_code");
                        }
                        else
                        {
                            empcode = Convert.ToInt32(row.Field("emp_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, empcode) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }

                        }

                        sts = CheckStatus(compid, finyr, empcode);

                        if (sts == 99 )
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "Has no objectives entry !";
                            return;
                        }

                        if (sts == 1)
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "Has objectivesalready in Open Status !";
                            return;
                        }

                        if (row.Field("reason").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid reason for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            reason = row.Field("reason").GetString();
                        }



                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@finyr", finyr);
                        cmd.Parameters.AddWithValue("@compid", compid);
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                       cmd.Parameters.AddWithValue("@reason", reason);
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                        cmd.ExecuteNonQuery();

                        cnt = cnt + 1;

                    }

                    try
                    {
                        objTrans.Commit();
                        objTrans.Dispose();
                        objTrans = null;

                        SendMail();


                        string message = " Record(s) Inserted Sucessfully ! Total Records Uploaded : " + cnt;
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
                        txtsheetname.Text = "";
                    }


                }



            }

            catch (Exception msg)

            {

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

                MyLabel.Text = msg.ToString();



                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("')};");
                sb.Append("</script>");

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


            }

            finally
            {


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }




        }



        protected void cmdActivate_click(object sender, EventArgs e)
        {
            int compid = 0;
            int empcode = 0;
            string actdeact;
            string oldflg = "";
            string reason;
            int cnt = 0;

            compid = Convert.ToInt32(cbocompany.SelectedValue);



            if (compid == 0)
            {
                MyLabel.Text = "Invalid Company !";
                return;
            }
        

            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Sheet Name !";
                return;
            }



            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);



            try

            {

                if (FileUpload1.HasFile)

                {
                    string FileName = FileUpload1.FileName;

                    string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

                    FileUpload1.PostedFile.SaveAs(path);

                    var wb = new XLWorkbook(path);
                    var ws = wb.Worksheet(txtsheetname.Text);

                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("ActivateDeactivateEmployee");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;


                    foreach (var row in table.DataRange.Rows())
                    {

                        empcode = 0;
                        reason = "";
                        actdeact = "";
                        oldflg = "";

                        if (row.Field("emp_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_code");
                        }
                        else
                        {
                            empcode = Convert.ToInt32(row.Field("emp_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, empcode) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }

                        }

                        if (row.Field("reason").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid reason for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            reason = row.Field("reason").GetString();
                        }


                        if (row.Field("active_flag").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            MyLabel.Text = "Invalid active_flag for emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + "!";
                            return;
                        }
                        else
                        {
                            oldflg = GetActiveStatus(compid, empcode);

                            if ((row.Field("active_flag").GetString() != "A") && (row.Field("active_flag").GetString() != "I"))
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Invalid active_flag for Emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) ;
                                return;
                            }

                            if (oldflg == row.Field("active_flag").GetString())
                            {
                                StopTransction(con, objTrans);

                                if (oldflg == "A")
                                {
                                    MyLabel.Text = "Emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " already active!";
                                    return;
                                }
                                else
                                {
                                    MyLabel.Text = "Emp code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " already deactivated !";
                                    return;
                                }
                                
                            }

                            else
                            {
                                actdeact = row.Field("active_flag").GetString();

                            }
                            
                        }


                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@compid", compid);
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                        cmd.Parameters.AddWithValue("@reason",reason);
                        cmd.Parameters.AddWithValue("@oldflg", oldflg);
                        cmd.Parameters.AddWithValue("@newflg", actdeact);
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                        cmd.ExecuteNonQuery();

                        cnt = cnt + 1;

                    }

                    try
                    {
                        objTrans.Commit();
                        objTrans.Dispose();
                        objTrans = null;

                        SendMail();


                        string message = " Record(s) Inserted Sucessfully ! Total Records Uploaded : " + cnt;
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
                        txtsheetname.Text = "";
                    }


                }



            }

            catch (Exception msg)

            {

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

                MyLabel.Text = msg.ToString();



                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("')};");
                sb.Append("</script>");

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


            }

            finally
            {


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }




        }

        protected void cmdapprove_click(object sender, EventArgs e)
        {

            int compid = 0;
            int empcode = 0;
            int approverid1 = 0;
            int approverid2 = 0;
            int appcompany1 = 0;
            int appcompany2 = 0;

             int cnt = 0;

            compid = Convert.ToInt32(cbocompany.SelectedValue);

            if (compid == 0)
            {
                MyLabel.Text = "Invalid Company !";
                return;
            }
           
               

            if (txtsheetname.Text.Length == 0)
            {
                MyLabel.Text = "Invalid Sheet Name !";
                return;
            }



            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection(constr);



            try

            {

                if (FileUpload1.HasFile)

                {
                    string FileName = FileUpload1.FileName;

                    string path = string.Concat(Server.MapPath("~/temp/" + FileUpload1.FileName));

                    FileUpload1.PostedFile.SaveAs(path);

                    var wb = new XLWorkbook(path);
                    var ws = wb.Worksheet(txtsheetname.Text);

                    var range = ws.RangeUsed();
                    var table = range.AsTable();

                    con.Open();
                    objTrans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("UpdateApprover");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = objTrans;


                    foreach (var row in table.DataRange.Rows())
                    {

                        empcode = 0;
                        approverid1 = 0;
                        approverid2 = 0;
                        appcompany1 = 0;
                        appcompany2 = 0;

                        if (row.Field("emp_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "emp_code");
                        }
                        else
                        {
                            empcode = Convert.ToInt32(row.Field("emp_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, empcode) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Emp Code " + Convert.ToInt32(row.Field("emp_code").GetString()) + " Not Exist In Employee Master !";
                                return;
                            }

                        }


                        if (row.Field("approver1_company").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "Approver 1 Company");
                        }
                        else
                        {
                            appcompany1 = Convert.ToInt32(row.Field("approver1_company").GetString()); // Notice how we're calling the cell by field name

                            if (CheckCompanyExist(appcompany1) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Company ID " + Convert.ToInt32(row.Field("approver1_company").GetString()) + " Not Exist In Company Master !";
                                return;
                            }

                        }

                        if (row.Field("approver1_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "Approver1");
                        }
                        else
                        {
                            approverid1 = Convert.ToInt32(row.Field("approver1_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, approverid1) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver1  " + approverid1 + " Not Exist In Employee Master !";
                                return;
                            }

                        }

                        if (row.Field("approver2_company").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "Approver 2 Company");
                        }
                        else
                        {
                            appcompany2 = Convert.ToInt32(row.Field("approver2_company").GetString()); // Notice how we're calling the cell by field name

                            if (CheckCompanyExist(appcompany2) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Company ID " + Convert.ToInt32(row.Field("approver2_company").GetString()) + " Not Exist In Company Master !";
                                return;
                            }

                        }


                        if (row.Field("approver2_code").GetString() == "")
                        {
                            StopTransction(con, objTrans);
                            validatenull(cnt + 1, "Approver2");
                        }
                        else
                        {
                            approverid2 = Convert.ToInt32(row.Field("approver2_code").GetString()); // Notice how we're calling the cell by field name

                            if (CheckEmpExist(compid, approverid2) == 0)
                            {
                                StopTransction(con, objTrans);
                                MyLabel.Text = "Approver2  " + approverid2 + " Not Exist In Employee Master !";
                                return;
                            }

                        }



                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@compid", compid);
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                        cmd.Parameters.AddWithValue("@appcompany1", appcompany1);
                        cmd.Parameters.AddWithValue("@approver1", approverid1);
                        cmd.Parameters.AddWithValue("@appcompany2", appcompany2);
                        cmd.Parameters.AddWithValue("@approver2", approverid2);
                        cmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["UserID"]));
                        cmd.ExecuteNonQuery();

                        cnt = cnt + 1;

                    }

                    try
                    {
                        objTrans.Commit();
                        objTrans.Dispose();
                        objTrans = null;

                        SendMail();


                        string message = " Record(s) Inserted Sucessfully ! Total Records Uploaded : " + cnt;
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
                        txtsheetname.Text = "";
                    }


                }



            }

            catch (Exception msg)

            {

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

                MyLabel.Text = msg.ToString();



                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("')};");
                sb.Append("</script>");

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());


            }

            finally
            {


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }




        }
    }
}