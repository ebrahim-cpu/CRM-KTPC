using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Convert.ToString(Session["Username"]);
                if (string.IsNullOrEmpty(lblUsername.Text))
                {
                    Response.Redirect("Default.aspx");
                }

                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            int hot = 0, warm = 0, cold = 0, completed = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                string query = "SELECT Leads_Status, COUNT(*) as Total FROM Investors_Profile GROUP BY Leads_Status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string status = reader["Leads_Status"].ToString();
                        int count = Convert.ToInt32(reader["Total"]);

                        if (status == "Hot") hot = count;
                        else if (status == "Warm") warm = count;
                        else if (status == "Cold") cold = count;
                        else if (status == "Completed") completed = count;
                    }
                    con.Close();
                }

                // Now load specific leads details into the Repeaters
                string dataQuery = @"SELECT p.*, 
                                     (SELECT COUNT(*) FROM Contacts c WHERE c.Company_Id = p.Id) as ContactCount,
                                     (SELECT COUNT(*) FROM Investors_Activities a WHERE a.Company_Name = p.Company_Name) as ActivityCount
                                     FROM Investors_Profile p 
                                     ORDER BY p.Company_Name ASC";
                using (SqlCommand cmdData = new SqlCommand(dataQuery, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmdData))
                    {
                        DataTable dtAllLeads = new DataTable();
                        sda.Fill(dtAllLeads);

                        if (dtAllLeads.Rows.Count > 0)
                        {
                            DataView dvHot = new DataView(dtAllLeads);
                            dvHot.RowFilter = "Leads_Status = 'Hot'";
                            rptHot.DataSource = dvHot;
                            rptHot.DataBind();

                            DataView dvWarm = new DataView(dtAllLeads);
                            dvWarm.RowFilter = "Leads_Status = 'Warm'";
                            rptWarm.DataSource = dvWarm;
                            rptWarm.DataBind();

                            DataView dvCold = new DataView(dtAllLeads);
                            dvCold.RowFilter = "Leads_Status = 'Cold'";
                            rptCold.DataSource = dvCold;
                            rptCold.DataBind();

                            DataView dvCompleted = new DataView(dtAllLeads);
                            dvCompleted.RowFilter = "Leads_Status = 'Completed'";
                            rptCompleted.DataSource = dvCompleted;
                            rptCompleted.DataBind();
                        }
                    }
                }
            }

            lblHot.Text = hot.ToString();
            lblWarm.Text = warm.ToString();
            lblCold.Text = cold.ToString();
            lblCompleted.Text = completed.ToString();
        }

        protected string GetLeadsStatusStyle(string status)
        {
            switch (status.ToLower())
            {
                case "hot": return "hot-status";
                case "warm": return "warm-status";
                case "cold": return "cold-status";
                case "completed": return "green-status";
                default: return "";
            }
        }
    }
}
