using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CRM
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                UpdateVisitorCount();
            }
        }
        protected void AuditTrailAkses()
        {
            SqlConnection konn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ToString());
            konn.Open();
            String kuery = @"INSERT INTO LOG_Acess(Username,Department,Division,LocationAccess) VALUES (@Username,@Department,@Division,@LocationAccess)";
            SqlCommand kmd = new SqlCommand(kuery, konn);
            kmd.Parameters.AddWithValue("@Username", Session["Username"]);
            kmd.Parameters.AddWithValue("@Department", Session["Department"]);
            kmd.Parameters.AddWithValue("@Division", Session["Divisyen"]);
            kmd.Parameters.AddWithValue("@LocationAccess", CapaiIPaddress());
            Session["Ip_Address"] = CapaiIPaddress();
            int kambing = Convert.ToInt32(kmd.ExecuteScalar());
            konn.Close();
        }
        protected string CapaiIPaddress()
        {
            string ipaddress = Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
            if (ipaddress == null || ipaddress == "")
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var Kebenaran = new FormsAuth.LdapAuthentication("LDAP://KHTP");
            Session["Username"] = txtUsername.Text; // untuk run local
            bool PenggunaSah = true; // untuk run local
            Label1.Text = PenggunaSah.ToString();
            Label3.Text = PenggunaSah.ToString();
            if (PenggunaSah)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HQConnectionString"].ToString());
                conn.Open();
                Session["Username"] = txtUsername.Text.Trim();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT IsAdmin_CRM,IsUser_CRM, IsHead_CRM,Username,Department,Divisyen FROM Pekerja WHERE Username ='{0}'", Session["Username"]);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Session["IsAdmin"] = dt.Rows[0]["IsAdmin_CRM"];
                        Session["IsUser"] = dt.Rows[0]["IsUser_CRM"];
                        Session["IsHead"] = dt.Rows[0]["IsHead_CRM"];
                        Session["Username"] = dt.Rows[0]["Username"];
                        Session["Department"] = dt.Rows[0]["Department"];
                        Session["Divisyen"] = dt.Rows[0]["Divisyen"];
                        AuditTrailAkses();
                        if ((bool)(Session["IsAdmin"]))
                        {
                            Response.Redirect("Home.aspx"); // Access approved.
                        }
                        else
                        if ((bool)(Session["IsUser"]))
                        {
                            Response.Redirect("Home.aspx"); // Access approved.
                        }
                        else
                        {
                            Response.Redirect("Default.aspx"); // Access denied. Return to login page.
                        }
                    }
                }
            }
            else
            {
                Label2.Text = "Username atau Password tak betul!";
                Label2.ForeColor = Color.Red;
                Label2.Visible = true;
                Label3.Text = DateTime.Now.ToString();
                Label1.Text = "";
                Label4.Text = "";
                Label5.Text = "";
            }
            //Response.Redirect("Home.aspx");
        }
        private void UpdateVisitorCount()
        {
            string pageName = "Default.aspx"; // Change this based on your page name
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            // Get the current visitor count and last visited date from the database
            int visitCount = 0;
            DateTime lastVisited = DateTime.Now;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Check if the page entry already exists in the database
                SqlCommand checkCmd = new SqlCommand("SELECT VisitCount, LastVisited FROM VisitorCount WHERE PageName = @PageName", connection);
                checkCmd.Parameters.AddWithValue("@PageName", pageName);
                //SqlDataReader reader = checkCmd.ExecuteReader();
                SqlDataReader reader = checkCmd.ExecuteReader();
                //checkCmd.ExecuteNonQuery();
                if (reader.HasRows)
                {
                    reader.Read();
                    visitCount = Convert.ToInt32(reader["VisitCount"]);
                    lastVisited = Convert.ToDateTime(reader["LastVisited"]);
                    Label3.Text = "Visitor Count:";
                    //Label3.ForeColor = Color.Red;
                    Label1.Visible = true;
                    Label1.Text = visitCount.ToString();
                    Label2.Text = "Last Visit:";
                    Label4.Text=lastVisited.ToString();
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    // If the page entry doesn't exist, insert a new record for the page
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO VisitorCount (PageName, VisitCount, LastVisited) VALUES (@PageName, @VisitCount, @LastVisited)", connection);
                    insertCmd.Parameters.AddWithValue("@PageName", pageName);
                    insertCmd.Parameters.AddWithValue("@VisitCount", visitCount);
                    insertCmd.Parameters.AddWithValue("@LastVisited", lastVisited);
                    insertCmd.ExecuteNonQuery();
                }
                // Update the visit count and last visited date
                visitCount++;
                lastVisited = DateTime.Now;
                // Update the database with the new values
                SqlCommand updateCmd = new SqlCommand("UPDATE VisitorCount SET VisitCount = @VisitCount, LastVisited = @LastVisited WHERE PageName = @PageName", connection);
                updateCmd.Parameters.AddWithValue("@PageName", pageName);
                updateCmd.Parameters.AddWithValue("@VisitCount", visitCount);
                updateCmd.Parameters.AddWithValue("@LastVisited", lastVisited);
                updateCmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
