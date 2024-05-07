using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace HRKpi
{
    public partial class MainMenu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblcompname.Text = Session["CompanyName"].ToString();
              
                string MenuList = GenerateMenu(Convert.ToInt32(Session["UserID"]), Convert.ToString(Session["UserName"]));
                lblUser.Text = Session["UserName"].ToString();
                Literal1.Text = MenuList;
            }
        }


        private string GenerateMenu(int uid, string uname)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var oStringBuilder = new StringBuilder();

            try
            {
                SqlCommand cmd = new SqlCommand("GenerateParentMenu", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@grpid", Convert.ToInt32(Session["UserLevel"]));
                
                cmd.Connection = conn;
                conn.Open();

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string MenuURL = Convert.ToString(row["url"]);
                        string MenuName = Convert.ToString(row["title"]);
                        string ParentID = Convert.ToString(row["parentid"]);
                        string menuid = Convert.ToString(row["menu_id"]);

                        string line = null;

                        if (CheckChildExist(Convert.ToInt32(menuid), Convert.ToInt32(Session["UserLevel"])) == 1)
                        {
                            //  line = String.Format(@"<li class='nav -item'><a href = '#' runat='server' class='nav-ink'><p>{0}</p></a></li>", MenuName);
                            //To Change Drop Down List Color - line = String.Format(@"<li runat='server' class='nav-item'> <a href='#' class='nav-link' runat='server'><p>{0}<i class='right  fas fa-caret-down pe-7s-angle-down caret-left'></i> </p></a><ul class='nav nav-treeview'  style='background-color:lightblue>", MenuName);


                            line = String.Format(@"<li runat='server' class='nav-item'> <a href='#' class='nav-link' runat='server'><p>{0}<i class='right  fas fa-caret-down pe-7s-angle-down caret-left'></i> </p></a><ul class='nav nav-treeview'>", MenuName);

                        }
                        else
                        {
                            if (MenuURL.Length != 0)
                            {
                                if (MenuName == "Home")
                                {
                                    line = String.Format(@"<li class='nav-item'><a href = ""{0}"" runat='server' class='nav-link'> <i class='nav-icon fas fa-home'></i><p>{1}</p></a></li>", MenuURL, MenuName);
                                }
                                else
                                {
                                    line = String.Format(@"<li class='nav-item'><a href = ""{0}"" runat='server' class='nav-link'><p>{1}</p></a></li>", MenuURL, MenuName);
                                }
                            }
                            else
                            {

                                line = String.Format(@"<li class='nav-item'><a href = '#' runat='server' class='nav-link'><p>{0}</p></a></li>", MenuName);

                            }


                        }

                        oStringBuilder.Append(line);
                        line = GetChildString(uid, menuid);
                        oStringBuilder.Append(line);
                    }


                }


                cmd.Dispose();
                conn.Close();
                conn.Dispose();

                return oStringBuilder.ToString();

            }

            catch
            {
                throw;
            }


            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }

        protected int CheckChildExist(int vvmenuid, int vvuid)
        {
            int childexist = 0;
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());

            try
            {
                SqlCommand cmd1 = new SqlCommand("CheckChildExist", conn1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@menuid", vvmenuid);
                cmd1.Parameters.AddWithValue("@uid", vvuid);
                cmd1.Connection = conn1;
                conn1.Open();
                childexist = Convert.ToInt32(cmd1.ExecuteScalar());
                cmd1.Dispose();
                conn1.Close();
                return childexist;
            }


            catch
            {
                throw;
            }


            finally
            {
                if (conn1.State == ConnectionState.Open)
                {
                    conn1.Close();
                    conn1.Dispose();
                }
            }

        }

        private string GetChildString(int vuid, string vparentid)
        {
            var vstringBuilder = new StringBuilder();
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            DataTable dt2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter();
            try
            {
                SqlCommand cmd2 = new SqlCommand("GenerateChildMenu");

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@uid", Convert.ToInt32(vuid));
                cmd2.Parameters.AddWithValue("@parentid", Convert.ToInt32(vparentid));
                cmd2.Parameters.AddWithValue("@grpid", Convert.ToInt32(Session["UserLevel"]));
                cmd2.Connection = conn2;
                conn2.Open();
                adapter2 = new SqlDataAdapter(cmd2);
                adapter2.Fill(dt2);

                if (dt2.Rows.Count != 0)
                {
                    foreach (DataRow row1 in dt2.Rows)
                    {
                        string MenuURL = Convert.ToString(row1["url"]);
                        string MenuName = Convert.ToString(row1["title"]);
                        string ParentID = Convert.ToString(row1["parentid"]);
                        //string line1 = String.Format(@"<li><a href=""{0}"">{1}</a></li>", MenuURL, MenuName);
                        string line1 = String.Format(@"<li class='nav-item'><a href = ""{0}"" runat='server' class='nav-link'><i class='far fa-circle nav-icon'></i><p>{1}</p></a></li>", MenuURL, MenuName);
                        vstringBuilder.Append(line1);
                    }

                    vstringBuilder.Append("</ul> </li>");
                }
                dt2.Dispose();
                cmd2.Dispose();
                conn2.Close();
                return vstringBuilder.ToString();
            }

            catch
            {
                throw;
            }


            finally
            {
                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                    conn2.Dispose();
                }
            }

        }


    }
}