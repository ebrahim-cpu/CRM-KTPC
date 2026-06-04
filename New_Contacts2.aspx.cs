using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class New_Contacts2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                BindIndustrialPark();
                // Check if the "Id" query string parameter is provided.
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    // Get the "Id" value from the query string.
                    int companyId = Convert.ToInt32(Request.QueryString["Id"]);
                    // Load data from Investors_Profile and bind it to the dropdown list.
                    BindCompanyDropdown(companyId);
                    // Set the selected value for the dropdown list based on the provided "Id".
                    ddlCompanyId.SelectedValue = companyId.ToString();
                    // Get and display the corresponding Company_Name in the "txtCompanyName" textbox.
                    txtCompanyName.Text = GetCompanyNameById(companyId);
                    txtCompanyPhone.Text = GetCompanyPhoneById(companyId);
                    txtCompanyId.Text = companyId.ToString();
                }
                else
                {
                    // If "Id" parameter is not provided, simply bind the dropdown without pre-selection.
                    BindCompanyDropdown();
                }
            }                
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            // Create a new SqlConnection and SqlCommand to execute the INSERT query.
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO Contacts (Company_Id, Company_Name, Company_Phone, Name, Position, Mobile, Notes, industrial_Park_Name) " +
                                     "VALUES (@CompanyId, @CompanyName, @CompanyPhone, @Name, @Position, @Mobile, @Notes, @IndustrialPark)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    // Add parameters and their values.
                    cmd.Parameters.AddWithValue("@CompanyId", txtCompanyId.Text);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                    cmd.Parameters.AddWithValue("@CompanyPhone", txtCompanyPhone.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@IndustrialPark", industrialPark_ddl != null && industrialPark_ddl.SelectedItem != null && industrialPark_ddl.SelectedItem.Value != "0" ? industrialPark_ddl.SelectedItem.Text : "");
                    // Open the connection and execute the query.
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            // After saving, redirect back to the company contacts list for this company.
            Response.Redirect("company_contacts.aspx?id=" + txtCompanyId.Text);
        }
        private void BindCompanyDropdown(int selectedCompanyId = 0)
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Id, Company_Name FROM Investors_Profile";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlCompanyId.DataSource = reader;
                    ddlCompanyId.DataTextField = "Company_Name";
                    ddlCompanyId.DataValueField = "Id";
                    ddlCompanyId.DataBind();
                }
            }
            // Add a default item to the dropdown list.
            ddlCompanyId.Items.Insert(0, new ListItem("Select Company", "0"));
            // Set the selected value based on the provided "selectedCompanyId".
            if (selectedCompanyId != 0)
            {
                ddlCompanyId.SelectedValue = selectedCompanyId.ToString();
            }
        }
        private string GetCompanyNameById(int companyId)
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Company_Name FROM Investors_Profile WHERE Id = @CompanyId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }
        private string GetCompanyPhoneById(int companyId)
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Company_Phone FROM Investors_Profile WHERE Id = @CompanyId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }
        private void BindCompanyDropdown()
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Id, Company_Name FROM Investors_Profile";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlCompanyId.DataSource = reader;
                    ddlCompanyId.DataTextField = "Company_Name";
                    ddlCompanyId.DataValueField = "Id";
                    ddlCompanyId.DataBind();
                }
            }
            // Add a default item to the dropdown list.
            ddlCompanyId.Items.Insert(0, new ListItem("Select Company", "0"));
        }
        protected void DdlCompanyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the Company_Name textbox with the selected Company_Name from the dropdown list.
            int selectedCompanyId = Convert.ToInt32(ddlCompanyId.SelectedValue);
            txtCompanyName.Text = GetCompanyNameById(selectedCompanyId);
            txtCompanyName.Text = ddlCompanyId.SelectedItem.Text;
            txtCompanyId.Text = ddlCompanyId.SelectedItem.Value.ToString();
        }

        private void BindIndustrialPark()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Industrial_Park_Name FROM Industrial_Park ORDER BY Industrial_Park_Name ASC", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        industrialPark_ddl.DataSource = dt;
                        industrialPark_ddl.DataTextField = "Industrial_Park_Name";
                        industrialPark_ddl.DataValueField = "Industrial_Park_Name";
                        industrialPark_ddl.DataBind();
                        con.Close();
                    }
                }
                industrialPark_ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch { }
        }
    }
}