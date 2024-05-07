using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


namespace HRKpi
{
    public partial class PMS_approve : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        string constr1 = ConfigurationManager.ConnectionStrings["PayConnectionString"].ToString();
        SqlCommand cmd = new SqlCommand();
        SqlCommand cmd1 = new SqlCommand();
        SqlCommand cmd2 = new SqlCommand();

        string status, user_mailid, Approver_Type, HR_MailId;
        int approval_status_id;

        string result_message;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {
                //lbl_cid.Text = Session["cid"].ToString();
                //lbl_eid.Text = Session["eid"].ToString();

                //string eid = Session["eid"].ToString();
                //lbl_emp_pass.Text = eid.ToString();

                //string cid = Session["cid"].ToString();
                //lbl_cid_pass.Text = cid.ToString();

                string eid = Request.QueryString["emp_code"];
                lbl_emp_pass.Text = eid.ToString();

                string cid = Request.QueryString["company_id"];
                lbl_cid_pass.Text = cid.ToString();


                int cidi;
                cidi = Convert.ToInt32(Session["cid"]);

                if (cid == "12")
                {
                    //eid = eid.Substring(eid.Length - 4);
                    //lbl_emp_pass.Text = eid.ToString();
                    lbl_emp_pass.Text = GetPEILEcode(lbl_emp_pass.Text);
                   
                }

                con.Open();
                //con1.Open();

                lbl_fin_year.Text = Session["Fin_year"].ToString();

                if (Session["Fin_year"].ToString() == "2019")
                {
                    lbl_fy_dtl.Text = "For the Financial Year 2019-20";
                    Pnl_KPI.Visible = true;
                    Pnl_Individual_Factors.Visible = true;
                    pnl_overall.Visible = false;
                    Pnl_Achievements.Visible = true;
                    Btn_Verification.Visible = false;
                }
                if (Session["Fin_year"].ToString() == "2020")
                {
                    lbl_fy_dtl.Text = "For the Financial Year 2020-21";
                    Pnl_KPI.Visible = true;
                    Pnl_Individual_Factors.Visible = true;
                    pnl_overall.Visible = false;
                    Pnl_Achievements.Visible = true;

                    Btn_Verification.Visible = false;
                }

                if (Session["Fin_year"].ToString() == "2021")
                {
                    lbl_fy_dtl.Text = "For the Financial Year 2021-22";
                    Pnl_KPI.Visible = true;
                    Pnl_Individual_Factors.Visible = true;
                    pnl_overall.Visible = false;
                    Pnl_Achievements.Visible = true;

                    Btn_Verification.Visible = false;
                }

                if (Session["Fin_year"] == null)
                {
                    Response.Redirect("Home.aspx");
                }

                if (!IsPostBack)
                {
                    refreshdata();
                    bind_kpi();
                    bind_kpi_rating();
                    bind_kpi_review();
                    bind_factors();
                    bind_spl_achievement();
                    review_check();
                    statuscheck();
                    Btn_Save.Visible = false;
                }
            }
            catch (Exception Con)
            {

            }
            finally
            {
                con.Close();
            }

            if (Session["cid"].ToString() == "9999")
            {
                pnl_overall.Visible = false;
            }
        }


        public void refreshdata()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {
                SqlDataReader dr;
                con.Open();
                SqlCommand command = new SqlCommand("group_employee_details_new", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@cid", SqlDbType.Int));
                command.Parameters["@cid"].Value = lbl_cid_pass.Text;
                command.Parameters.Add(new SqlParameter("@eid", SqlDbType.Int));
                command.Parameters["@eid"].Value = lbl_emp_pass.Text;


                dr = command.ExecuteReader();
                dr.Read();

                if (dr == null || !dr.HasRows)
                {

                }
                else
                {
                    if (lbl_cid_pass.Text == "12")
                    {
                        lbl_empcode.Text = dr[18].ToString();
                    }
                    else
                    {
                        lbl_empcode.Text = dr[0].ToString();
                    }


                    lbl_name.Text = dr[1].ToString();
                    lbl_dept.Text = dr[6].ToString();
                    lbl_desig.Text = dr[7].ToString();
                    lbl_grade.Text = dr[8].ToString();
                    lbl_doj.Text = dr[10].ToString();

                    if (Session["cid"].ToString() == "9999")
                    {
                        lbl_grade_dis.Text = "Band:";
                    }
                }
                dr.Close();

            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }
        public void statuscheck()
        {

            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                if (Session["Fin_Year"].ToString() == "2021")
                {

                    cmd = new SqlCommand(@"SELECT  distinct (a.emp_code), review_status as Status_Id, remarks, reviewer_remarks, approver_remarks
                                FROM KPI_Objective_MSt a   
                                WHERE a.fin_year = '" + lbl_fin_year.Text + "' and emp_code = '" + lbl_emp_pass.Text + "' and a.company_id = '" + lbl_cid_pass.Text + "'", con);

                }

                if (lbl_fin_year.Text == "2020")
                {

                    cmd = new SqlCommand(@"SELECT  distinct (a.emp_code), Status_Id 
                                FROM KPI_Review_Dtl a   
                                WHERE a.fin_year = '" + lbl_fin_year.Text + "' and emp_code = '" + lbl_emp_pass.Text + "' and a.company_id = '" + lbl_cid_pass.Text + "'", con);
                }

                if (lbl_fin_year.Text == "2019")
                {
                    cmd = new SqlCommand(@"SELECT  distinct (a.emp_code), Status_Id 
                                FROM KPI_Review_Dtl a   
                                WHERE a.fin_year = '" + lbl_fin_year.Text + "' and emp_code = '" + lbl_emp_pass.Text + "' and a.company_id = '" + lbl_cid_pass.Text + "'", con);
                }


                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if ((dr["Status_Id"]).ToString() == "2")
                    {
                       //  Btn_Save.Enabled = true;
                        // pnl_overall.Visible = true;


                    }

                    if ((dr["Status_Id"]).ToString() == "3")
                    {
                        //ddl_uom_7.DataBind();
                        //ddl_uom_7.SelectedItem.Text = (dr["Unit_Desc"]).ToString();

                        ddl_status.DataBind();
                        ddl_status.SelectedValue = (dr["Status_Id"]).ToString();

                        ddl_status.Enabled = false;
                        Btn_Save.Enabled = false;

                        //pnl_overall.Visible = true;



                    }

                    //if ((dr["Status_Id"]).ToString() == "3")
                    //{
                    //    Btn_Save.Enabled = false;
                    //    btn_submit.Enabled = false;
                    //}

                    if ((dr["Status_Id"]).ToString() == "6")
                    {
                       // Btn_Save.Enabled = true;
                        //   pnl_overall.Visible = true;


                    }


                }

                dr.Close();
            }
            catch (Exception eec) { }
            finally
            {
                con.Close();
            }
        }

        public void bind_kpi()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                //int cidi;
                //cidi = Convert.ToInt32(Session["cid"]);

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 3);
                //    lbl_emp_pass.Text = eid.ToString();
                //}
                //CONVERT(VARCHAR, DATEADD(DAY, -1, a.Target_date), 103) as Target_date

                if (cidi == 12)
                {
                    cmd = new SqlCommand(@"SELECT a.kpi_obj_id, a.S_No,  a.Obj_Desc, b.Unit_Desc,  CONVERT (varchar(11), a.Target_date, 106) as Target_date  , a.target, a.Weightage, a.review_status as  status_id, a.remarks 
                                FROM KPI_Objective_Mst a, KPI_Tbl_UOM_Mst b  
                                WHERE a.review_status in (2,3,4,6)  AND a.fin_year = '" + lbl_fin_year.Text + "' and a.UoM = b.Unit_Id and emp_code = '" + lbl_emp_pass.Text + "' and a.company_id = '" + lbl_cid_pass.Text + "'", con);
                }
                else
                {
                    //a.review, a.reference, a.location,
                    cmd = new SqlCommand(@"SELECT a.kpi_obj_id, a.S_No,  a.Obj_Desc, b.Unit_Desc,  CONVERT (varchar(11), a.Target_date, 106) as Target_date  , a.target, a.Weightage, a.review_status as status_id, a.remarks 
                                FROM KPI_Objective_Mst a, KPI_Tbl_UOM_Mst b  
                                WHERE a.review_status  in (2,3,4,6)  AND a.fin_year = '" + lbl_fin_year.Text + "' and a.UoM = b.Unit_Id and emp_code = '" + lbl_emp_pass.Text + "' and a.company_id = '" + lbl_cid_pass.Text + "'", con);
                }

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if ((dr["S_No"]).ToString() == "1")
                    {
                        kpi_id_1.Text = (dr["KPI_Obj_Id"]).ToString();
                        lbl_kpi_desc_1.Text = (dr["Obj_Desc"]).ToString();
                        lbl_unit_1.Text = (dr["Unit_Desc"]).ToString();
                        lbl_target_1.Text = (dr["Target"]).ToString();
                        lbl_target_dt_1.Text = (dr["Target_date"]).ToString();
                        lbl_weightage_1.Text = (dr["Weightage"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "2")
                    {
                        kpi_id_2.Text = (dr["KPI_Obj_Id"]).ToString();
                        lbl_kpi_desc_2.Text = (dr["Obj_Desc"]).ToString();
                        lbl_unit_2.Text = (dr["Unit_Desc"]).ToString();
                        lbl_target_2.Text = (dr["Target"]).ToString();
                        lbl_target_dt_2.Text = (dr["Target_date"]).ToString();
                        lbl_weightage_2.Text = (dr["Weightage"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "3")
                    {
                        kpi_id_3.Text = (dr["KPI_Obj_Id"]).ToString();
                        lbl_kpi_desc_3.Text = (dr["Obj_Desc"]).ToString();
                        lbl_unit_3.Text = (dr["Unit_Desc"]).ToString();
                        lbl_target_3.Text = (dr["Target"]).ToString();
                        lbl_target_dt_3.Text = (dr["Target_date"]).ToString();
                        lbl_weightage_3.Text = (dr["Weightage"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "4")
                    {
                        kpi_id_4.Text = (dr["KPI_Obj_Id"]).ToString();
                        lbl_kpi_desc_4.Text = (dr["Obj_Desc"]).ToString();
                        lbl_unit_4.Text = (dr["Unit_Desc"]).ToString();
                        lbl_target_4.Text = (dr["Target"]).ToString();
                        lbl_target_dt_4.Text = (dr["Target_date"]).ToString();
                        lbl_weightage_4.Text = (dr["Weightage"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "5")
                    {
                        kpi_id_5.Text = (dr["KPI_Obj_Id"]).ToString();
                        lbl_kpi_desc_5.Text = (dr["Obj_Desc"]).ToString();
                        lbl_unit_5.Text = (dr["Unit_Desc"]).ToString();
                        lbl_target_5.Text = (dr["Target"]).ToString();
                        lbl_target_dt_5.Text = (dr["Target_date"]).ToString();
                        lbl_weightage_5.Text = (dr["Weightage"]).ToString();
                    }
                    ddl_status.SelectedValue = (dr["status_id"]).ToString();
                    //txt_remarks.Text = (dr["Remarks"]).ToString();
                }

                dr.Close();
            }
            catch (Exception eec) { }
            finally
            {
                con.Close();
            }
        }

        public void bind_kpi_rating()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {
                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                ////    eid = eid.Substring(eid.Length - 3);
                //    lbl_emp_pass.Text = eid.ToString();
                //}

                cmd = new SqlCommand(@"SELECT S_No, KPI_Target_Achieved_Dtl,  CONVERT (varchar(11), KPI_Dt_Achievement, 106) as KPI_Dt_Achievement 
                                    FROM KPI_Review_Dtl 
                                    WHERE fin_year = '" + lbl_fin_year.Text + "' and kpi_period = 0 and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "'", con);
                dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    if ((dr["S_No"]).ToString() == "1")
                    {
                        lbl_kpi_target_dtl_1.Text = (dr["KPI_Target_Achieved_Dtl"]).ToString().Replace("<br />", "\r\n");
                        lbl_kpi_achieved_dt_1.Text = (dr["KPI_Dt_Achievement"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "2")
                    {
                        lbl_kpi_target_dtl_2.Text = (dr["KPI_Target_Achieved_Dtl"]).ToString().Replace("<br />", "\r\n");
                        lbl_kpi_achieved_dt_2.Text = (dr["KPI_Dt_Achievement"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "3")
                    {
                        lbl_kpi_target_dtl_3.Text = (dr["KPI_Target_Achieved_Dtl"]).ToString().Replace("<br />", "\r\n");
                        lbl_kpi_achieved_dt_3.Text = (dr["KPI_Dt_Achievement"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "4")
                    {
                        lbl_kpi_target_dtl_4.Text = (dr["KPI_Target_Achieved_Dtl"]).ToString().Replace("<br />", "\r\n");
                        lbl_kpi_achieved_dt_4.Text = (dr["KPI_Dt_Achievement"]).ToString();
                    }
                    if ((dr["S_No"]).ToString() == "5")
                    {
                        lbl_kpi_target_dtl_5.Text = (dr["KPI_Target_Achieved_Dtl"]).ToString().Replace("<br />", "\r\n");
                        lbl_kpi_achieved_dt_5.Text = (dr["KPI_Dt_Achievement"]).ToString();
                    }
                    //ddl_status.SelectedValue = (dr["status_id"]).ToString();
                    //txt_review_remarks.Text = (dr["gen_remarks"]).ToString();
                    //txt_approver_remarks.Text = (dr["approver_remarks"].ToString());
                }

                dr.Close();






            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        public void bind_kpi_review()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 3);
                //    lbl_emp_pass.Text = eid.ToString();
                //}


                cmd = new SqlCommand("SELECT S_No, kpi_rating_approver, kpi_rating_remark_approver, Status_Id, Remarks_Reviewer, Remarks_Approver FROM KPI_Review_Dtl where fin_year = '" + lbl_fin_year.Text + "' and  emp_code = '" + lbl_emp_pass.Text + "' AND kpi_period = 0  and company_id = '" + lbl_cid_pass.Text + "'", con);
                //cmd = new SqlCommand("SELECT S_No, KPI_Rating_Reviewer, KPI_Rating_Remark_Reviewer, Status_Id, Remarks_Reviewer, Remarks_Approver FROM KPI_Review_Dtl where fin_year = '" + lbl_fin_year.Text + "' and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "'", con);
                dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    if ((dr["S_No"]).ToString() == "1")
                    {
                        txt_rating_1.Text = (dr["kpi_rating_approver"]).ToString();
                        txt_kpi_remarks_1.Text = (dr["kpi_rating_remark_approver"]).ToString().Replace("<br />", "\r\n");
                    }
                    if ((dr["S_No"]).ToString() == "2")
                    {
                        txt_rating_2.Text = (dr["kpi_rating_approver"]).ToString();
                        txt_kpi_remarks_2.Text = (dr["kpi_rating_remark_approver"]).ToString().Replace("<br />", "\r\n");
                    }
                    if ((dr["S_No"]).ToString() == "3")
                    {
                        txt_rating_3.Text = (dr["kpi_rating_approver"]).ToString();
                        txt_kpi_remarks_3.Text = (dr["kpi_rating_remark_approver"]).ToString().Replace("<br />", "\r\n");
                    }
                    if ((dr["S_No"]).ToString() == "4")
                    {
                        txt_rating_4.Text = (dr["kpi_rating_approver"]).ToString();
                        txt_kpi_remarks_4.Text = (dr["kpi_rating_remark_approver"]).ToString().Replace("<br />", "\r\n");
                    }
                    if ((dr["S_No"]).ToString() == "5")
                    {
                        txt_rating_5.Text = (dr["kpi_rating_approver"]).ToString();
                        txt_kpi_remarks_5.Text = (dr["kpi_rating_remark_approver"]).ToString().Replace("<br />", "\r\n");
                    }
                    ddl_status.SelectedValue = (dr["Status_Id"]).ToString();
                    txt_review_remarks.Text = (dr["Remarks_Reviewer"]).ToString().Replace("<br />", "\r\n");
                    txt_approver_remarks.Text = (dr["Remarks_Approver"].ToString()).Replace("<br />", "\r\n");
                }

                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        public void bind_factors()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 3);
                //    lbl_emp_pass.Text = eid.ToString();
                //}

                cmd = new SqlCommand("SELECT * FROM KPI_Individual_Factor_Dtl  where fin_year = '" + lbl_fin_year.Text + "' and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "'", con);
                dr = cmd.ExecuteReader();

                dr.Read();

                if (dr == null || !dr.HasRows)
                {

                }
                else
                {
                    ddl1.SelectedValue = dr[4].ToString();
                    ddl2.SelectedValue = dr[5].ToString();
                    ddl3.SelectedValue = dr[6].ToString();
                    ddl4.SelectedValue = dr[7].ToString();
                    ddl5.SelectedValue = dr[8].ToString();
                    ddl6.SelectedValue = dr[9].ToString();
                    ddl7.SelectedValue = dr[10].ToString();
                    ddl8.SelectedValue = dr[11].ToString();
                    ddl9.SelectedValue = dr[12].ToString();
                    ddl10.SelectedValue = dr[13].ToString();

                    ddl_over_rating.SelectedValue = dr[14].ToString();

                    if (dr[14].ToString() == "1")
                    {
                        Lbl_Overall_Rating.Text = "Does Not Meet Expectation";
                    }
                    if (dr[14].ToString() == "2")
                    {
                        Lbl_Overall_Rating.Text = "Meets Some Expectation";
                    }
                    if (dr[14].ToString() == "3")
                    {
                        Lbl_Overall_Rating.Text = "Meets Expectation";
                    }
                    if (dr[14].ToString() == "4")
                    {
                        Lbl_Overall_Rating.Text = "Exceeds Some Expectation";
                    }
                    if (dr[14].ToString() == "5")
                    {
                        Lbl_Overall_Rating.Text = "Exceeds Expectation";
                    }
                    if (dr[14].ToString() == "-1")
                    {
                        Lbl_Overall_Rating.Text = "";
                    }

                    //double score_1, score_2, score_3, score_4, score_5, total_score, score_against;

                    //double factor_1, factor_2, factor_3, factor_4, factor_5, factor_6, factor_7, factor_8, factor_9, factor_10, total_factor, factor_against;

                    //double score_factor;

                    //score_1 = (Convert.ToDouble("0" + lbl_weightage_1.Text) * Convert.ToDouble("0" + txt_rating_1.Text)) / 100;
                    //score_2 = (Convert.ToDouble("0" + lbl_weightage_2.Text) * Convert.ToDouble("0" + txt_rating_2.Text)) / 100;
                    //score_3 = (Convert.ToDouble("0" + lbl_weightage_3.Text) * Convert.ToDouble("0" + txt_rating_3.Text)) / 100;
                    //score_4 = (Convert.ToDouble("0" + lbl_weightage_4.Text) * Convert.ToDouble("0" + txt_rating_4.Text)) / 100;
                    //score_5 = (Convert.ToDouble("0" + lbl_weightage_5.Text) * Convert.ToDouble("0" + txt_rating_5.Text)) / 100;

                    //factor_1 = Convert.ToDouble("0" + ddl1.SelectedValue);
                    //factor_2 = Convert.ToDouble("0" + ddl2.SelectedValue);
                    //factor_3 = Convert.ToDouble("0" + ddl3.SelectedValue);
                    //factor_4 = Convert.ToDouble("0" + ddl4.SelectedValue);
                    //factor_5 = Convert.ToDouble("0" + ddl5.SelectedValue);
                    //factor_6 = Convert.ToDouble("0" + ddl6.SelectedValue);
                    //factor_7 = Convert.ToDouble("0" + ddl7.SelectedValue);
                    //factor_8 = Convert.ToDouble("0" + ddl8.SelectedValue);
                    //factor_9 = Convert.ToDouble("0" + ddl9.SelectedValue);
                    //factor_10 = Convert.ToDouble("0" + ddl10.SelectedValue);

                    //total_score = (score_1 + score_2 + score_3 + score_4 + score_5);

                    //score_against = ((total_score * 80) / 100);

                    //total_factor = ((factor_1 + factor_2 + factor_3 + factor_4 + factor_5 + factor_6 + factor_7 + factor_8 + factor_9 + factor_10));

                    //factor_against = ((total_factor * 20) / 50);

                    //score_factor = Math.Round(score_against + factor_against);

                    //lbl_overall_percentage.Text = score_factor.ToString();

                    //if ((score_factor > 90))
                    //{
                    //    Lbl_Overall_Rating.Text = "Exceeds Expectation";
                    //    ddl_over_rating.SelectedValue = "5";

                    //}
                    //if ((score_factor > 70) && (score_factor <= 90))
                    //{
                    //    Lbl_Overall_Rating.Text = "Exceeds Some Expectation";
                    //    //ddl_over_rating.SelectedValue = "4";

                    //}
                    //if ((score_factor > 60) && (score_factor <= 70))
                    //{
                    //    Lbl_Overall_Rating.Text = "Meets Expectation";
                    //    //ddl_over_rating.SelectedValue = "3";

                    //}
                    //if (((score_factor >= 50) && (score_factor <= 60)))
                    //{
                    //    Lbl_Overall_Rating.Text = "Meets Some Expectation";
                    //    //ddl_over_rating.SelectedValue = "2";
                    //}
                    //if ((score_factor < 50))
                    //{
                    //    Lbl_Overall_Rating.Text = "Does Not Meet Expectation";
                    //    //ddl_over_rating.SelectedValue = "1";
                    //}

                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        public void review_check()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {
                con.Open();
                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 4);
                //    lbl_emp_pass.Text = eid.ToString();
                //}

                cmd = new SqlCommand("SELECT Approver_Eid_1, Approver_Eid_2  FROM KPI_Login_Mst  where emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "'", con);
                dr = cmd.ExecuteReader();

                dr.Read();

                if (dr == null || !dr.HasRows)
                {

                }
                else
                {
                    string eid1 = dr[0].ToString();
                    string eid2 = dr[1].ToString();

                    if (eid1 == eid2)
                    {
                        lbl_approver_type.Text = "A";
                        Pnl_KPI.Enabled = true;
                        Pnl_Individual_Factors.Enabled = true;
                        pnl_overall.Visible = false;


                      //  Btn_Save.Enabled = true;
                    }
                    else
                    {
                        lbl_approver_type.Text = "R";
                        Pnl_KPI.Enabled = false;
                        Pnl_Individual_Factors.Enabled = false;
                        pnl_overall.Visible = false;


                        Btn_Save.Enabled = false;
                        ddl_spl_rating_1.Enabled = false;
                        ddl_spl_rating_2.Enabled = false;
                        ddl_spl_rating_3.Enabled = false;
                    }

                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        //public void bind_spl_achievement()
        //{
        //    SqlConnection con = new SqlConnection(constr);
        //    try
        //    {

        //        con.Open();

        //        SqlDataReader dr;

        //        cmd = new SqlCommand("SELECT * FROM KPI_Achievement_Dtl WHERE fin_year = 2019 and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "' ORDER BY Achievement_Id Desc", con);
        //        dr = cmd.ExecuteReader();

        //        dr.Read();

        //        if (dr == null || !dr.HasRows)
        //        {

        //        }
        //        else
        //        {
        //            //txt_spl_achievement.Text = dr[4].ToString();
        //            //txt_benefit_org.Text = dr[5].ToString();
        //            //txt_involved_stakeholder.Text = dr[6].ToString();

        //            if ((dr["S_No"]).ToString() == "1")
        //            {
        //                txt_ach_dtl_1.Text = (dr["Achievement_Detail"]).ToString();
        //                txt_ach_benefit_1.Text = (dr["Benefit"]).ToString();
        //                ddl_ach_role_1.SelectedValue = (dr["Role"]).ToString();
        //                txt_ach_remark_1.Text = (dr["Remarks"]).ToString();
        //                txt_ach_stakeholder_1.Text = (dr["Involved_Internal"]).ToString();
        //                ddl_spl_rating_1.SelectedValue = (dr["Rating"]).ToString();
        //            }
        //            if ((dr["S_No"]).ToString() == "2")
        //            {
        //                txt_ach_dtl_2.Text = (dr["Achievement_Detail"]).ToString();
        //                txt_ach_benefit_2.Text = (dr["Benefit"]).ToString();
        //                ddl_ach_role_2.SelectedValue = (dr["Role"]).ToString();
        //                txt_ach_remark_2.Text = (dr["Remarks"]).ToString();
        //                txt_ach_stakeholder_2.Text = (dr["Involved_Internal"]).ToString();
        //                ddl_spl_rating_2.SelectedValue = (dr["Rating"]).ToString();
        //            }
        //            if ((dr["S_No"]).ToString() == "3")
        //            {
        //                txt_ach_dtl_3.Text = (dr["Achievement_Detail"]).ToString();
        //                txt_ach_benefit_3.Text = (dr["Benefit"]).ToString();
        //                ddl_ach_role_3.SelectedValue = (dr["Role"]).ToString();
        //                txt_ach_remark_3.Text = (dr["Remarks"]).ToString();
        //                txt_ach_stakeholder_3.Text = (dr["Involved_Internal"]).ToString();
        //                ddl_spl_rating_3.SelectedValue = (dr["Rating"]).ToString();
        //            }
        //        }
        //        dr.Close();
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public void bind_spl_achievement()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                int cidi;
                cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 4);
                //    lbl_emp_pass.Text = eid.ToString();
                //}


                cmd = new SqlCommand("SELECT * FROM KPI_Achievement_Dtl WHERE fin_year = " + lbl_fin_year.Text + " and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "' ORDER BY Achievement_Id ", con);
                dr = cmd.ExecuteReader();

                // dr.Read();
                //if (dr == null || !dr.HasRows)
                //{
                //}
                //else
                //{
                //txt_spl_achievement.Text = dr[4].ToString();
                //txt_benefit_org.Text = dr[5].ToString();
                //txt_involved_stakeholder.Text = dr[6].ToString();

                while (dr.Read())
                {
                    if ((dr["S_No"]).ToString() == "1")
                    {
                        spl_row_1.Visible = true;
                        txt_ach_dtl_1.Text = (dr["Achievement_Detail"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_benefit_1.Text = (dr["Benefit"]).ToString().Replace("<br />", "\r\n");
                        ddl_ach_role_1.SelectedValue = (dr["Role"]).ToString();
                        txt_ach_remark_1.Text = (dr["Remarks"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_stakeholder_1.Text = (dr["Involved_Internal"]).ToString().Replace("<br />", "\r\n");
                        ddl_spl_rating_1.SelectedValue = (dr["Rating"]).ToString();
                    }

                    if ((dr["S_No"]).ToString() == "2")
                    {
                        spl_row_2.Visible = true;
                        txt_ach_dtl_2.Text = (dr["Achievement_Detail"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_benefit_2.Text = (dr["Benefit"]).ToString().Replace("<br />", "\r\n");
                        ddl_ach_role_2.SelectedValue = (dr["Role"]).ToString();
                        txt_ach_remark_2.Text = (dr["Remarks"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_stakeholder_2.Text = (dr["Involved_Internal"]).ToString().Replace("<br />", "\r\n");
                        ddl_spl_rating_2.SelectedValue = (dr["Rating"]).ToString();
                    }

                    if ((dr["S_No"]).ToString() == "3")
                    {
                        spl_row_3.Visible = true;
                        txt_ach_dtl_3.Text = (dr["Achievement_Detail"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_benefit_3.Text = (dr["Benefit"]).ToString().Replace("<br />", "\r\n");
                        ddl_ach_role_3.SelectedValue = (dr["Role"]).ToString();
                        txt_ach_remark_3.Text = (dr["Remarks"]).ToString().Replace("<br />", "\r\n");
                        txt_ach_stakeholder_3.Text = (dr["Involved_Internal"]).ToString().Replace("<br />", "\r\n");
                        ddl_spl_rating_3.SelectedValue = (dr["Rating"]).ToString();
                    }
                }

                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        public void bind_overall()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();

                SqlDataReader dr;

                //string eid = Request.QueryString["emp_code"];
                //lbl_emp_pass.Text = eid.ToString();

                //int cidi;
                //cidi = Convert.ToInt32(lbl_cid_pass.Text);

                //if (cidi == 12)
                //{
                //   // eid = eid.Substring(eid.Length - 4);
                //    lbl_emp_pass.Text = eid.ToString();
                //}

                cmd = new SqlCommand("SELECT * FROM KPI_Achievement_Dtl WHERE fin_year = '" + lbl_fin_year.Text + "' and  emp_code = '" + lbl_emp_pass.Text + "' and company_id = '" + lbl_cid_pass.Text + "' ", con);
                dr = cmd.ExecuteReader();

                dr.Read();

                if (dr == null || !dr.HasRows)
                {

                }
                else
                {
                    //lbl_spl_ach_dtl.Text = dr[4].ToString();
                    //lbl_benefit.Text = dr[5].ToString();
                    //lbl_involved.Text = dr[6].ToString();
                    //ddl_spl_rating.SelectedValue = dr[7].ToString();
                }
                dr.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            if (lbl_fin_year.Text == "2021")
            {
                double score_1, score_2, score_3, score_4, score_5, total_score, score_against;

                double factor_1, factor_2, factor_3, factor_4, factor_5, factor_6, factor_7, factor_8, factor_9, factor_10, total_factor, factor_against;

                double score_factor;

                score_1 = (Convert.ToDouble("0" + lbl_weightage_1.Text) * Convert.ToDouble("0" + txt_rating_1.Text)) / 100;
                score_2 = (Convert.ToDouble("0" + lbl_weightage_2.Text) * Convert.ToDouble("0" + txt_rating_2.Text)) / 100;
                score_3 = (Convert.ToDouble("0" + lbl_weightage_3.Text) * Convert.ToDouble("0" + txt_rating_3.Text)) / 100;
                score_4 = (Convert.ToDouble("0" + lbl_weightage_4.Text) * Convert.ToDouble("0" + txt_rating_4.Text)) / 100;
                score_5 = (Convert.ToDouble("0" + lbl_weightage_5.Text) * Convert.ToDouble("0" + txt_rating_5.Text)) / 100;

                factor_1 = Convert.ToDouble("0" + ddl1.SelectedValue);
                factor_2 = Convert.ToDouble("0" + ddl2.SelectedValue);
                factor_3 = Convert.ToDouble("0" + ddl3.SelectedValue);
                factor_4 = Convert.ToDouble("0" + ddl4.SelectedValue);
                factor_5 = Convert.ToDouble("0" + ddl5.SelectedValue);
                factor_6 = Convert.ToDouble("0" + ddl6.SelectedValue);
                factor_7 = Convert.ToDouble("0" + ddl7.SelectedValue);
                factor_8 = Convert.ToDouble("0" + ddl8.SelectedValue);
                factor_9 = Convert.ToDouble("0" + ddl9.SelectedValue);
                factor_10 = Convert.ToDouble("0" + ddl10.SelectedValue);

                total_score = (score_1 + score_2 + score_3 + score_4 + score_5);

                score_against = ((total_score * 80) / 100);

                total_factor = ((factor_1 + factor_2 + factor_3 + factor_4 + factor_5 + factor_6 + factor_7 + factor_8 + factor_9 + factor_10) * 2);

                factor_against = ((total_factor * 20) / 100);

                score_factor = Math.Round(score_against + factor_against);

                lbl_overall_percentage.Text = score_factor.ToString();

                if ((score_factor > 90))
                {
                    Lbl_Overall_Rating.Text = "Exceeds Expectation";
                    ddl_over_rating.SelectedValue = "5";

                }
                if ((score_factor > 80) && (score_factor <= 90))
                {
                    Lbl_Overall_Rating.Text = "Exceeds Some Expectation";
                    ddl_over_rating.SelectedValue = "4";

                }
                if ((score_factor > 65) && (score_factor <= 80))
                {
                    Lbl_Overall_Rating.Text = "Meets Expectation";
                    ddl_over_rating.SelectedValue = "3";

                }
                if (((score_factor > 55) && (score_factor <= 65)))
                {
                    Lbl_Overall_Rating.Text = "Meets Some Expectation";
                    ddl_over_rating.SelectedValue = "2";
                }
                if ((score_factor <= 55))
                {
                    Lbl_Overall_Rating.Text = "Does Not Meet Expectation";
                    ddl_over_rating.SelectedValue = "1";
                }


                //if (ddl_over_rating.SelectedValue == "5")
                //{
                //if (ddl_over_rating.SelectedValue == "5" && (score_factor > 95))
                //{
                //string kpi_desc = txt_kpidesc.Text;
                //string S_no = j.ToString();

                SqlConnection con = new SqlConnection(constr);

                try
                {
                    con.Open();

                    string eid = Session["emp_code"].ToString();
                    lbl_emp_pass.Text = eid.ToString();

                    int cidi;
                    cidi = Convert.ToInt32(lbl_cid_pass.Text);

                    if (cidi == 12)
                    {
                      //  eid = eid.Substring(eid.Length - 4);
                        lbl_emp_pass.Text = eid.ToString();
                    }

                    for (int j = 1; j <= 5; j++)
                    {
                        string txtkpidesc = "txt_kpi_desc_" + j;
                        string txtrating = "txt_rating_" + j;
                        string txtkpiremarks = "txt_kpi_remarks_" + j;
                        string kpiid = "kpi_id_" + j;

                        TextBox txt_kpidesc = Pnl_KPI.FindControl(txtkpidesc) as TextBox;
                        TextBox txt_rating = Pnl_KPI.FindControl(txtrating) as TextBox;
                        TextBox txt_kpi_remarks = Pnl_KPI.FindControl(txtkpiremarks) as TextBox;
                        Label lbl_kpi_id = Pnl_KPI.FindControl(kpiid) as Label;

                        if (ddl_status.SelectedValue == "4")
                        {
                            if (lbl_approver_type.Text == "A")
                            {
                                approval_status_id = 4;
                            }
                            if (lbl_approver_type.Text == "R")
                            {
                                approval_status_id = 2;
                            }
                        }

                        if (ddl_status.SelectedValue == "3")
                        {

                            approval_status_id = 3;

                        }

                        SqlCommand cmd1 = new SqlCommand(@"UPDATE KPI_Review_Dtl SET KPI_Rating_Approver = '" + txt_rating.Text + "', KPI_Rating_Remark_Approver = @KPI_Rating_Remark_Approver, status_id = '" + approval_status_id + "', Remarks_Approver = @Remarks_Approver " +
                            "WHERE  fin_year  = '" + lbl_fin_year.Text + "' AND company_id = '" + lbl_cid_pass.Text + "' AND kpi_period = 0 AND Emp_Code = '" + lbl_emp_pass.Text + "' AND KPI_Id = '" + lbl_kpi_id.Text + "' ", con);

                        cmd1.Parameters.AddWithValue("@KPI_Rating_Remark_Approver", Convert.ToString(txt_kpi_remarks.Text.Replace("\r\n", "<br />")));
                        cmd1.Parameters.AddWithValue("@Remarks_Approver", Convert.ToString(txt_approver_remarks.Text.Replace("\r\n", "<br />")));

                        cmd1.ExecuteNonQuery();
                    }

                    //--------------------------------FOR Individual Factor Insert

                    cmd = new SqlCommand("Proc_KPI_Individual_Factors_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("fin_year", lbl_fin_year.Text);
                    cmd.Parameters.AddWithValue("company_id", cidi);
                    cmd.Parameters.AddWithValue("emp_code", lbl_emp_pass.Text);

                    cmd.Parameters.AddWithValue("Productivity", Convert.ToInt32("0" + factor_1));
                    cmd.Parameters.AddWithValue("Willingness_Responsibility", Convert.ToInt32("0" + factor_2));
                    cmd.Parameters.AddWithValue("Quality_Work", Convert.ToInt32("0" + factor_3));
                    cmd.Parameters.AddWithValue("On_Time_Delivery", Convert.ToInt32("0" + factor_4));
                    cmd.Parameters.AddWithValue("Innovation_Creativity", Convert.ToInt32("0" + factor_5));
                    cmd.Parameters.AddWithValue("Learning_Initiative", Convert.ToInt32("0" + factor_6));
                    cmd.Parameters.AddWithValue("Communication", Convert.ToInt32("0" + factor_7));
                    cmd.Parameters.AddWithValue("Team_Player", Convert.ToInt32("0" + factor_8));
                    cmd.Parameters.AddWithValue("Leadership_Skills", Convert.ToInt32("0" + factor_9));
                    cmd.Parameters.AddWithValue("Customer_Interaction", Convert.ToInt32("0" + factor_10));

                    cmd.Parameters.AddWithValue("Overall_Rating_Id", Convert.ToInt32("0" + ddl_over_rating.SelectedValue));

                    cmd.Parameters.AddWithValue("Prepared_By", Session["eid"]);
                    cmd.Parameters.AddWithValue("Created_DateTime", Convert.ToDateTime(DateTime.Now.ToString()));

                    cmd.Parameters.AddWithValue("Status", Session["eid"]);


                    cmd.ExecuteNonQuery();


                    SqlCommand cmd2 = new SqlCommand(@"UPDATE kpi_objective_mst SET review_status  = '" + ddl_status.SelectedValue + "'  WHERE fin_year = '" + lbl_fin_year.Text + "' AND company_id = '" + cidi + "' AND emp_code = '" + lbl_emp_pass.Text + "'", con);
                    cmd2.ExecuteNonQuery();



                    //SqlCommand cmd2 = new SqlCommand(@"INSERT INTO KPI_Individual_Factor_Dtl VALUES('" + lbl_fin_year.Text + "', '" + cidi + "', '" + lbl_emp_pass.Text + "', '" + factor_1 + "', '" + factor_2 + "', '" + factor_3 + "','" + factor_4 + "','" + factor_5 + "','" + factor_6 + "', '" + factor_7 + "', '" + factor_8 + "', '" + factor_9 + "', '" + factor_10 + "', '"+ ddl_over_rating.SelectedValue +"','" + Session["eid"] + "', '" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "', '" + ddl_status.SelectedValue + "' )", con);
                    //cmd2.ExecuteNonQuery();

                    //SqlCommand cmd3 = new SqlCommand("Update KPI_Achievement_Dtl set Rating = '" + ddl_spl_rating.SelectedValue + "', Approved_By = '"+ Session["eid"] + "', Approved_DateTime = '" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "' WHERE  fin_year  = '" + lbl_fin_year.Text + "' and company_id = '" + lbl_cid_pass.Text + "' And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);
                    //cmd3.ExecuteNonQuery();

                    for (int i = 1; i <= 3; i++)
                    {
                        string txtachdtl = "txt_ach_dtl_" + i;
                        string txtachbenefit = "txt_ach_benefit_" + i;
                        string ddlachrole = "ddl_ach_role_" + i;
                        string txtachremark = "txt_ach_remark_" + i;
                        string txtachstakeholder = "txt_ach_stakeholder_" + i;
                        string ddlsplrating = "ddl_spl_rating_" + i;

                        TextBox txt_ach_dtl = Pnl_Achievements.FindControl(txtachdtl) as TextBox;
                        TextBox txt_ach_benefit = Pnl_Achievements.FindControl(txtachbenefit) as TextBox;
                        DropDownList ddl_ach_role = Pnl_Achievements.FindControl(ddlachrole) as DropDownList;
                        TextBox txt_ach_remark = Pnl_Achievements.FindControl(txtachremark) as TextBox;
                        TextBox txt_ach_stakeholder = Pnl_Achievements.FindControl(txtachstakeholder) as TextBox;
                        DropDownList ddl_spl_rating = Pnl_Achievements.FindControl(ddlsplrating) as DropDownList;

                        if (txt_ach_dtl.Text.Length > 5)
                        {
                            SqlCommand cmd3 = new SqlCommand("Update KPI_Achievement_Dtl set Rating = '" + ddl_spl_rating.SelectedValue + "', Approved_By = '" + Session["eid"] + "', Approved_DateTime = '" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "' WHERE  fin_year  = '" + lbl_fin_year.Text + "' and company_id = '" + lbl_cid_pass.Text + "' And Emp_Code = '" + lbl_emp_pass.Text + "' AND S_No = '" + i + "' ", con);
                            cmd3.ExecuteNonQuery();
                        }
                    }

                    if (ddl_status.SelectedValue == "4")
                    {
                        if (lbl_approver_type.Text == "A")
                        {
                            result_message = "Action Applied Successfully.";
                        }
                        if (lbl_approver_type.Text == "R")
                        {
                            result_message = "Action Applied Successfully.";
                        }
                    }


                    if (ddl_status.SelectedValue == "3")
                    {
                        result_message = "Action Applied Successfully.";
                    }

                    //SendMail_self();
                    //SendMail_Reviewer();

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + result_message.ToString() + "');", true);



                    Btn_Save.Enabled = false;

                    //Response.Write("<script>alert('As per your rating, his/her Appraisal Category as '" + Lbl_Overall_Rating.Text + "'.')</script>");
                    //Response.Write("<script>window.location.href='Manager_Dashboard.aspx';</script>");

                }

                catch (Exception ex)
                {

                }

                finally
                {
                    con.Close();
                }

            }


            //for current year kpi 2020 changed to 2019
            if (lbl_fin_year.Text == "2019")
            {
                SqlConnection con = new SqlConnection(constr);
                try
                {
                    con.Open();

                    string eid = Session["emp_code"].ToString();
                    lbl_emp_pass.Text = eid.ToString();

                    int cidi;
                    cidi = Convert.ToInt32(lbl_cid_pass.Text);

                    if (cidi == 12)
                    {
                       // eid = eid.Substring(eid.Length - 4);
                        lbl_emp_pass.Text = eid.ToString();
                    }

                    for (int j = 1; j <= 5; j++)
                    {

                        //string txtkpidesc = "txt_kpi_desc_" + j;
                        string lblkpidesc = "lbl_kpi_desc_" + j;
                        string txtrating = "txt_rating_" + j;
                        string txtkpiremarks = "txt_kpi_remarks_" + j;
                        string kpiid = "kpi_id_" + j;

                        //TextBox txt_kpidesc = Pnl_KPI.FindControl(txtkpidesc) as TextBox;
                        Label lbl_kpidesc = Pnl_KPI.FindControl(lblkpidesc) as Label;
                        TextBox txt_rating = Pnl_KPI.FindControl(txtrating) as TextBox;
                        TextBox txt_kpi_remarks = Pnl_KPI.FindControl(txtkpiremarks) as TextBox;
                        Label lbl_kpi_id = Pnl_KPI.FindControl(kpiid) as Label;

                        //string kpi_desc = txt_kpidesc.Text;
                        //string S_no = j.ToString();

                        if (lbl_kpidesc.Text.Length > 10)
                        {
                            cmd = new SqlCommand("Proc_KPI_Rating_Insert", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("fin_year", lbl_fin_year.Text);
                            cmd.Parameters.AddWithValue("company_id", lbl_cid_pass.Text);
                            cmd.Parameters.AddWithValue("emp_code", lbl_emp_pass.Text);

                            //cmd.Parameters.AddWithValue("KPI_Period", 1);
                            cmd.Parameters.AddWithValue("S_No", Convert.ToInt32("0" + j));
                            cmd.Parameters.AddWithValue("kpi_id", Convert.ToInt32(lbl_kpi_id.Text));

                            cmd.Parameters.AddWithValue("KPI_Rating_Rvr1", Convert.ToString(txt_rating.Text));
                            cmd.Parameters.AddWithValue("KPI_Rating_Remark_Rvr1", Convert.ToString(txt_kpi_remarks.Text));

                            cmd.Parameters.AddWithValue("KPI_Rating_Rvr2", Convert.ToString(""));
                            cmd.Parameters.AddWithValue("KPI_Rating_Remark_Rvr2", Convert.ToString(""));

                            cmd.Parameters.AddWithValue("KPI_Rating_HR", Convert.ToString(""));
                            cmd.Parameters.AddWithValue("KPI_Rating_Remark_HR", Convert.ToString(""));

                            cmd.Parameters.AddWithValue("prepared_by", Session["eid"]);
                            cmd.Parameters.AddWithValue("prepared_datetime", Convert.ToDateTime(DateTime.Now.ToString()));
                            cmd.Parameters.AddWithValue("status_id", Convert.ToInt32(ddl_status.SelectedValue));
                            cmd.Parameters.AddWithValue("gen_remarks", Convert.ToString(txt_review_remarks.Text));
                            cmd.Parameters.AddWithValue("approver_remarks", Convert.ToString(txt_approver_remarks.Text));


                            cmd.Parameters.AddWithValue("kpi_period", 0);


                            cmd.ExecuteNonQuery();
                        }
                    }



                    //    cmd1 = new SqlCommand("Update KPI_Mst set status_id = '" + ddl_status.SelectedValue + "' where kpi_period = 0 AND  fin_year  = '" + lbl_fin_year.Text + "'  and company_id = '" + lbl_cid_pass.Text + "'  And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);


                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("Update KPI_Objective_Mst set status_id = '" + ddl_status.SelectedValue + "' where  fin_year  = '" + lbl_fin_year.Text + "' and company_id = '" + lbl_cid_pass.Text + "' And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);
                    cmd2.ExecuteNonQuery();

                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Action applied.');", true);
                    // SendMail();



                    Response.Write("<script>alert('Action applied.')</script>");
                    // Response.Write("<script>window.location.href='Manager_Dashboard.aspx';</script>");

                }

                catch (Exception ex)
                {

                }

                finally
                {
                    con.Close();
                }
            }

            //if (lbl_fin_year.Text == "2021")
            //{
            //    SqlConnection con = new SqlConnection(constr);
            //    try
            //    {
            //        con.Open();

            //        string eid = Request.QueryString["emp_code"];
            //        lbl_emp_pass.Text = eid.ToString();

            //        int cidi;
            //        cidi = Convert.ToInt32(lbl_cid_pass.Text);

            //        if (cidi == 12)
            //        {
            //            eid = eid.Substring(eid.Length - 4);
            //            lbl_emp_pass.Text = eid.ToString();
            //        }

            //        for (int j = 1; j <= 5; j++)
            //        {

            //            //string txtkpidesc = "txt_kpi_desc_" + j;
            //            string lblkpidesc = "lbl_kpi_desc_" + j;
            //            string txtrating = "txt_rating_" + j;
            //            string txtkpiremarks = "txt_kpi_remarks_" + j;
            //            string kpiid = "kpi_id_" + j;

            //            //TextBox txt_kpidesc = Pnl_KPI.FindControl(txtkpidesc) as TextBox;
            //            Label lbl_kpidesc = Pnl_KPI.FindControl(lblkpidesc) as Label;
            //            TextBox txt_rating = Pnl_KPI.FindControl(txtrating) as TextBox;
            //            TextBox txt_kpi_remarks = Pnl_KPI.FindControl(txtkpiremarks) as TextBox;
            //            Label lbl_kpi_id = Pnl_KPI.FindControl(kpiid) as Label;

            //            //string kpi_desc = txt_kpidesc.Text;
            //            //string S_no = j.ToString();

            //            if (lbl_kpidesc.Text.Length > 10)
            //            {
            //                cmd = new SqlCommand("Proc_KPI_Rating_Insert", con);
            //                cmd.CommandType = CommandType.StoredProcedure;

            //                cmd.Parameters.AddWithValue("fin_year", lbl_fin_year.Text);
            //                cmd.Parameters.AddWithValue("company_id", lbl_cid_pass.Text);
            //                cmd.Parameters.AddWithValue("emp_code", lbl_emp_pass.Text);

            //                //cmd.Parameters.AddWithValue("KPI_Period", 1);
            //                cmd.Parameters.AddWithValue("S_No", Convert.ToInt32("0" + j));
            //                cmd.Parameters.AddWithValue("kpi_id", Convert.ToInt32(lbl_kpi_id.Text));

            //                cmd.Parameters.AddWithValue("KPI_Rating_Rvr1", Convert.ToString(txt_rating.Text));
            //                cmd.Parameters.AddWithValue("KPI_Rating_Remark_Rvr1", Convert.ToString(txt_kpi_remarks.Text));

            //                cmd.Parameters.AddWithValue("KPI_Rating_Rvr2", Convert.ToString(""));
            //                cmd.Parameters.AddWithValue("KPI_Rating_Remark_Rvr2", Convert.ToString(""));

            //                cmd.Parameters.AddWithValue("KPI_Rating_HR", Convert.ToString(""));
            //                cmd.Parameters.AddWithValue("KPI_Rating_Remark_HR", Convert.ToString(""));

            //                cmd.Parameters.AddWithValue("prepared_by", Session["eid"]);
            //                cmd.Parameters.AddWithValue("prepared_datetime", Convert.ToDateTime(DateTime.Now.ToString()));
            //                cmd.Parameters.AddWithValue("status_id", Convert.ToInt32(ddl_status.SelectedValue));
            //                cmd.Parameters.AddWithValue("gen_remarks", Convert.ToString(txt_review_remarks.Text));
            //                cmd.Parameters.AddWithValue("approver_remarks", Convert.ToString(txt_approver_remarks.Text));

            //                    cmd.Parameters.AddWithValue("kpi_period", 0);



            //                cmd.ExecuteNonQuery();
            //            }
            //        }

            //        //cmd1 = new SqlCommand("Update KPI_Mst set status_id = '" + ddl_status.SelectedValue + "' where kpi_period = 3 AND fin_year  = '" + lbl_fin_year.Text + "' and company_id = '" + lbl_cid_pass.Text + "' And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);


            //        //if (cidi == 16)
            //        //{
            //        //    cmd1 = new SqlCommand("Update KPI_Mst set status_id = '" + ddl_status.SelectedValue + "' where kpi_period = 1 AND  fin_year  = '" + lbl_fin_year.Text + "'  and company_id = '" + lbl_cid_pass.Text + "'  And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);
            //        //}
            //        //else
            //        //{
            //        //    cmd1 = new SqlCommand("Update KPI_Mst set status_id = '" + ddl_status.SelectedValue + "' where kpi_period = 4 AND  fin_year  = '" + lbl_fin_year.Text + "'  and company_id = '" + lbl_cid_pass.Text + "'  And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);
            //        //}

            //        cmd1.ExecuteNonQuery();

            //        SqlCommand cmd2 = new SqlCommand("Update KPI_Objective_Mst set status_id = '" + ddl_status.SelectedValue + "' where  fin_year  = '" + lbl_fin_year.Text + "' and company_id = '" + lbl_cid_pass.Text + "' And Emp_Code = '" + lbl_emp_pass.Text + "' ", con);
            //        cmd2.ExecuteNonQuery();

            //        //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Action applied.');", true);
            //        //SendMail();
            //        SendMail_self();
            //        SendMail_Reviewer();
            //        Response.Write("<script>alert('Action applied.')</script>");
            //     //   Response.Write("<script>window.location.href='Manager_Dashboard.aspx';</script>");

            //    }

            //    catch (Exception ex)
            //    {

            //    }

            //    finally
            //    {
            //        con.Close();
            //    }
            //}

        }

        public void SendMail()
        {
            SqlConnection con = new SqlConnection(constr);

            try
            {
                con.Open();

                if (ddl_status.SelectedValue == "3")
                {
                    status = "Approved";
                }
                if (ddl_status.SelectedValue == "4")
                {
                    status = "Re-submitted for Correction";
                }

                int cid;
                cid = Convert.ToInt32(lbl_cid_pass.Text);

                string eid = Session["emp_code"].ToString();
                lbl_emp_pass.Text = eid.ToString();

                if (cid == 12)
                {
                    //eid = eid.Substring(eid.Length - 4);
                    lbl_emp_pass.Text = eid.ToString();
                }

                SqlDataReader dr;
                {

                    if (cid == 2)
                    {
                        //cmd = new SqlCommand("select a.Emp_Code, a.elig_levdys, sum(b.leave_days)as Leave_Days, (a.elig_levdys - sum(b.leave_days)) as Balance from trvempmaster a, payinput b where b.fin_year = 2017 and b.company_id = '" + Session["cid"] + "' and a.emp_code = b.emp_code and a.Emp_Code =  '" + Session["eid"] + "' group by a.emp_code, a.elig_levdys", con1);
                        cmd = new SqlCommand("select Emp_code, mailid from   TRVEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 4)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PCSEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 16)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PWSILEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 9999)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   ESDEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 17)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   cargoEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }

                    if (cid == 12)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PEILEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }

                    if (cid == 5)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PRDEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 7)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   MMEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    user_mailid = dr[1].ToString();

                    dr.Close();
                }

                MailMessage mail = new MailMessage();
                //string to_add = Session["approver_mail"].ToString();
                string to_add = user_mailid;
                string from_name = Session["User_name"].ToString();

                mail.To.Add(to_add);

                mail.From = new MailAddress("pep@pricolcorporate.com", "KPI");

                string Body;

                mail.Subject = "Objectives for FY 2021-22";
                Body = "Your Objectives  for FY 2021-22  has been " + status;


                mail.Body = Body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.pricolcorporate.com"; //Or Your SMTP Server Address

                //smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential("pep@pricolcorporate.com", "Welcome@19");
                //Or your Smtp Email ID and Password

                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
        }


        public void SendMail_self()
        {
            SqlConnection con = new SqlConnection(constr);

            try
            {
                con.Open();

                if (ddl_status.SelectedValue == "3")
                {
                    status = "Approved";
                }
                if (ddl_status.SelectedValue == "4")
                {
                    status = "Re-submitted for Correction";
                }

                int cid;
                cid = Convert.ToInt32(lbl_cid_pass.Text);

                string eid = Request.QueryString["emp_code"];

                string to_add = "";

                if (cid == 12)
                {
                    to_add = GetMailIdPEIL(cid, eid);
                }
                else
                {
                    to_add = GetMailId(cid, Convert.ToInt32(eid));
                }


                lbl_emp_pass.Text = eid.ToString();

                //if (cid == 12)
                //{
                //    eid = eid.Substring(eid.Length - 4);
                //    lbl_emp_pass.Text = eid.ToString();
                //}



                MailMessage mail = new MailMessage();
                mail.To.Add(to_add);
                mail.IsBodyHtml = true;

                //string to_add = Session["approver_mail"].ToString();
                string Body = "";
                mail.From = new MailAddress("pep@pricolcorporate.com", "KPI");
                //mail.From = new MailAddress("ashok4988@gmail.com");

                if (ddl_status.SelectedValue == "3")
                {
                    mail.Subject = "Approval of your annual objectives for FY 2021-22";
                    Body = " Dear Associate,  <br/> <br/> Your objectives for FY 2021-2022 have been sucessfully approved ! <br/> <br/> Performance Management System ";
                }
                if (ddl_status.SelectedValue == "4")
                {
                    mail.Subject = "Resubmit information of your annual objectives for FY 2021-22";
                    Body = " Dear Associate,  <br/> <br/> Your objectives for FY 2021-2022 have been asked to resubmit ! <br/> <br/> Performance Management System ";
                }

                mail.Body = Body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.pricolcorporate.com"; //Or Your SMTP Server Address

                //smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential("pep@pricolcorporate.com", "Welcome@19");
                //Or your Smtp Email ID and Password

                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
        }


        public void SendMail_Reviewer()
        {
            try
            {
                string to_add = Session["emp_mailid"].ToString();
                string to_name = Session["User_name"].ToString();

                MailMessage mail = new MailMessage();

                mail.To.Add(to_add);
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("pep@pricolcorporate.com", "KPI");
                mail.Subject = "Annual objectives Approval confirmation of " + lbl_empcode.Text + " - " + lbl_name.Text + " for FY 2021-22";
                string Body = " Dear Associate, <br/> <br/> You have sucessfully approved the annual objectives of " + lbl_empcode.Text + " - " + lbl_name.Text + "! <br/> <br/> Performance Management System ";
                mail.Body = Body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.pricolcorporate.com"; //Or Your SMTP Server Address

                //smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential("pep@pricolcorporate.com", "Welcome@19");
                //Or your Smtp Email ID and Password

                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void DDL_Emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetFormControlValues(this);
            //bind_role();
        }

        private void ResetFormControlValues(System.Web.UI.Control parent)
        {
            foreach (System.Web.UI.Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    ResetFormControlValues(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((System.Web.UI.WebControls.TextBox)c).Text = "";
                            break;
                        case "System.Web.UI.WebControls.dropdownlist":
                            ((System.Web.UI.WebControls.DropDownList)c).ClearSelection();
                            ((System.Web.UI.WebControls.DropDownList)c).SelectedIndex = -1;
                            break;
                        case "System.Web.UI.WebControls.RadioButton":
                            ((System.Web.UI.WebControls.RadioButton)c).Checked = false;
                            ((System.Web.UI.WebControls.RadioButton)c).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButtonList":
                            ((System.Web.UI.WebControls.RadioButtonList)c).SelectedIndex = -1;
                            ((System.Web.UI.WebControls.RadioButtonList)c).Enabled = false;
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((System.Web.UI.WebControls.CheckBox)c).Checked = false;
                            ((System.Web.UI.WebControls.CheckBox)c).Enabled = false;
                            break;
                    }
                }
            }

        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_kpi();
            bind_kpi_rating();
        }

        protected void Btn_Verification_Click(object sender, EventArgs e)
        {
            double score_1, score_2, score_3, score_4, score_5, total_score, score_against;

            double factor_1, factor_2, factor_3, factor_4, factor_5, factor_6, factor_7, factor_8, factor_9, factor_10, total_factor, factor_against;

            double score_factor;

            score_1 = (Convert.ToDouble("0" + lbl_weightage_1.Text) * Convert.ToDouble("0" + txt_rating_1.Text)) / 100;
            score_2 = (Convert.ToDouble("0" + lbl_weightage_2.Text) * Convert.ToDouble("0" + txt_rating_2.Text)) / 100;
            score_3 = (Convert.ToDouble("0" + lbl_weightage_3.Text) * Convert.ToDouble("0" + txt_rating_3.Text)) / 100;
            score_4 = (Convert.ToDouble("0" + lbl_weightage_4.Text) * Convert.ToDouble("0" + txt_rating_4.Text)) / 100;
            score_5 = (Convert.ToDouble("0" + lbl_weightage_5.Text) * Convert.ToDouble("0" + txt_rating_5.Text)) / 100;

            factor_1 = Convert.ToDouble("0" + ddl1.SelectedValue);
            factor_2 = Convert.ToDouble("0" + ddl2.SelectedValue);
            factor_3 = Convert.ToDouble("0" + ddl3.SelectedValue);
            factor_4 = Convert.ToDouble("0" + ddl4.SelectedValue);
            factor_5 = Convert.ToDouble("0" + ddl5.SelectedValue);
            factor_6 = Convert.ToDouble("0" + ddl6.SelectedValue);
            factor_7 = Convert.ToDouble("0" + ddl7.SelectedValue);
            factor_8 = Convert.ToDouble("0" + ddl8.SelectedValue);
            factor_9 = Convert.ToDouble("0" + ddl9.SelectedValue);
            factor_10 = Convert.ToDouble("0" + ddl10.SelectedValue);

            total_score = (score_1 + score_2 + score_3 + score_4 + score_5);

            score_against = ((total_score * 80) / 100);

            total_factor = ((factor_1 + factor_2 + factor_3 + factor_4 + factor_5 + factor_6 + factor_7 + factor_8 + factor_9 + factor_10));

            factor_against = ((total_factor * 20) / 50);

            score_factor = score_against + factor_against;

            lbl_overall_percentage.Text = score_factor.ToString();

            if ((score_factor > 90))
            {
                Lbl_Overall_Rating.Text = "Exceeds Expectation";
                ddl_over_rating.SelectedValue = "5";

            }
            if ((score_factor > 80) && (score_factor <= 90))
            {
                Lbl_Overall_Rating.Text = "Exceeds Some Expectation";
                ddl_over_rating.SelectedValue = "4";

            }
            if ((score_factor > 65) && (score_factor <= 80))
            {
                Lbl_Overall_Rating.Text = "Meets Expectation";
                ddl_over_rating.SelectedValue = "3";

            }
            if (((score_factor > 55) && (score_factor <= 65)))
            {
                Lbl_Overall_Rating.Text = "Meets Some Expectation";
                ddl_over_rating.SelectedValue = "2";
            }
            if ((score_factor <= 55))
            {
                Lbl_Overall_Rating.Text = "Does Not Meet Expectation";
                ddl_over_rating.SelectedValue = "1";
            }

        }

        protected void btn_dispute_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand(@"INSERT INTO KPI_Dispute_Dtl VALUES('" + lbl_fin_year.Text + "', '" + Session["cid"] + "', '" + Session["eid"] + "', '" + lbl_cid_pass.Text + "', '" + lbl_emp_pass.Text + "', '" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "', 7, 0, '" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "' )", con);
                cmd2.ExecuteNonQuery();
                Response.Write("<script>alert('Dispute Raised Successfully.')</script>");
                SendMail_Dispute();
                btn_dispute.Enabled = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SendMail_Dispute()
        {
            SqlConnection con = new SqlConnection(constr);

            try
            {
                con.Open();

                if (ddl_status.SelectedValue == "3")
                {
                    status = "Approved";
                }
                if (ddl_status.SelectedValue == "4")
                {
                    status = "Re-submitted for Correction";
                }

                int cid;
                cid = Convert.ToInt32(lbl_cid_pass.Text);
                string eid = Session["emp_code"].ToString();
                lbl_emp_pass.Text = eid.ToString();

                if (cid == 12)
                {
                    //eid = eid.Substring(eid.Length - 4);
                    lbl_emp_pass.Text = eid.ToString();
                }

                SqlDataReader dr;
                {

                    if (cid == 2)
                    {
                        //cmd = new SqlCommand("select a.Emp_Code, a.elig_levdys, sum(b.leave_days)as Leave_Days, (a.elig_levdys - sum(b.leave_days)) as Balance from trvempmaster a, payinput b where b.fin_year = 2017 and b.company_id = '" + Session["cid"] + "' and a.emp_code = b.emp_code and a.Emp_Code =  '" + Session["eid"] + "' group by a.emp_code, a.elig_levdys", con1);
                        cmd = new SqlCommand("select Emp_code, mailid from   TRVEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 4)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PCSEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 14)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   VMEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }

                    if (cid == 12)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PEILEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }

                    if (cid == 5)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   PRDEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 7)
                    {
                        cmd = new SqlCommand("select Emp_code, mailid from   MMEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    if (cid == 9999)
                    {
                        HR_MailId = "vijayakumar.balasubramaniam@pricol.co.in";
                        cmd = new SqlCommand("select Emp_code, official_mail_id from   ESDEmpMaster  where emp_code = '" + Request.QueryString["Emp_code"] + "' ", con);
                    }
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    user_mailid = dr[1].ToString();

                    dr.Close();
                }

                MailMessage mail = new MailMessage();
                //string to_add = Session["approver_mail"].ToString();
                string to_add = user_mailid;
                string from_name = Session["User_name"].ToString();

                mail.To.Add(HR_MailId);

                mail.From = new MailAddress("pep@pricolcorporate.com", "KPI");
                //mail.From = new MailAddress("ashok4988@gmail.com");
                mail.Subject = "KPI Self Evaluation for FY 2020-21";

                string Body = "KPI Self Evaluation dispute raised, Kindly check the portal.";
                mail.Body = Body;

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
                con.Close();
            }
        }


        protected string GetMailId(int compid, int empid)
        {

            string mailid1 = "";

            string constrnew = ConfigurationManager.ConnectionStrings["PCSConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constrnew))
            {
                using (SqlCommand cmdnew = new SqlCommand("RptProcGetmailid"))
                {
                    cmdnew.CommandType = CommandType.StoredProcedure;
                    cmdnew.Parameters.AddWithValue("@compid", compid);
                    cmdnew.Parameters.AddWithValue("@empcode", empid);

                    cmdnew.Connection = concomp;
                    concomp.Open();
                    mailid1 = Convert.ToString(cmdnew.ExecuteScalar());
                    // concomp.Close();
                    return mailid1;
                }
            }

        }

        protected string GetMailIdPEIL(int compid, string empid)
        {

            string mailid1 = "";

            string constrnew = ConfigurationManager.ConnectionStrings["PCSConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constrnew))
            {
                using (SqlCommand cmdnew = new SqlCommand("RptProcGetmailidPEIL"))
                {
                    cmdnew.CommandType = CommandType.StoredProcedure;
                    cmdnew.Parameters.AddWithValue("@compid", compid);
                    cmdnew.Parameters.AddWithValue("@empcode", empid);

                    cmdnew.Connection = concomp;
                    concomp.Open();
                    mailid1 = Convert.ToString(cmdnew.ExecuteScalar());
                    // concomp.Close();
                    return mailid1;
                }
            }

        }

        protected string GetPEILEcode(string empcode)
        {
            string nm = "";

            string constrnew = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection concomp = new SqlConnection(constrnew))
            {
                using (SqlCommand cmdnew = new SqlCommand("GetPEILIntCode"))
                {
                    cmdnew.CommandType = CommandType.StoredProcedure;

                    cmdnew.Parameters.AddWithValue("@empcode", empcode);
                    cmdnew.Connection = concomp;
                    concomp.Open();
                    nm = Convert.ToString(cmdnew.ExecuteScalar());
                    // concomp.Close();
                    return nm;
                }
            }

        }
    }
}