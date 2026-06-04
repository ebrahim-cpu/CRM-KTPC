using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class ActivityTimeline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || string.IsNullOrEmpty(Session["Username"].ToString()))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            lblUsername.Text = Session["Username"].ToString();

            if (!IsPostBack)
            {
                PopulateCompanies();
                
                // If a company ID/name is passed in URL query string, select it
                if (Request.QueryString["company"] != null)
                {
                    string company = Request.QueryString["company"].ToString();
                    ListItem item = ddlCompanyName.Items.FindByValue(company);
                    if (item != null)
                    {
                        ddlCompanyName.ClearSelection();
                        item.Selected = true;
                    }
                }
                
                BindTimelineData();
            }
        }

        private void PopulateCompanies()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Company_Name FROM Investors_Profile ORDER BY Company_Name ASC", con);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                
                ddlCompanyName.DataSource = dt;
                ddlCompanyName.DataTextField = "Company_Name";
                ddlCompanyName.DataValueField = "Company_Name";
                ddlCompanyName.DataBind();
                
                ddlCompanyName.Items.Insert(0, new ListItem("All", ""));
                con.Close();
            }
        }

        private void BindTimelineData()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                string query;
                SqlCommand cmd;
                string searchKeyword = txtSearch.Text.Trim();

                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    query = @"SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, Company_Status, UpdatedDate, UpdatedBy 
                             FROM Investors_Activities 
                             WHERE Company_Name LIKE '%' + @search + '%' OR Activity_Type LIKE '%' + @search + '%' OR Activity_Description LIKE '%' + @search + '%' OR Company_Status LIKE '%' + @search + '%' 
                             ORDER BY Activity_Date_Start DESC";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@search", searchKeyword);
                }
                else
                {
                    query = @"SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, Company_Status, UpdatedDate, UpdatedBy 
                             FROM Investors_Activities 
                             WHERE (@Company_Name = '' OR Company_Name = @Company_Name) 
                             AND (@Company_Status = '' OR Company_Status = @Company_Status) 
                             ORDER BY Activity_Date_Start DESC";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
                }

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    rptTimeline.DataSource = dt;
                    rptTimeline.DataBind();
                    rptTimeline.Visible = true;
                    phNoData.Visible = false;
                }
                else
                {
                    rptTimeline.Visible = false;
                    phNoData.Visible = true;
                }

                con.Close();
            }
        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = ""; // Clear search when dropdown selection changes
            BindTimelineData();
        }

        protected void ddlInvestorStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = ""; // Clear search when dropdown selection changes
            BindTimelineData();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            BindTimelineData();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int selectedId = Convert.ToInt32(btn.CommandArgument);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Investors_Activities WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", selectedId);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            BindTimelineData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Activity deleted successfully');", true);
        }

        protected string GetStatusBadgeStyle(string status)
        {
            switch (status)
            {
                case "Preliminary Enquiry": return "background-color: #5bc0de;";
                case "Due Dilligence": return "background-color: #f0ad4e;";
                case "RFI": return "background-color: #337ab7;";
                case "Site Visit": return "background-color: #5cb85c;";
                case "RFP": return "background-color: #d9534f;";
                case "Decision": return "background-color: #6f5499;";
                case "Letter Of Intent": return "background-color: #a94442;";
                case "Lease Agreement": return "background-color: #3c763d;";
                case "Collection of First Payment": return "background-color: #8a6d3b;";
                default: return "background-color: #777777;";
            }
        }
    }
}
