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
    public partial class Print_Investors_Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = 0;
                if (Request.QueryString["Id"] != null)
                {
                    ID = Convert.ToInt32(Request.QueryString["Id"]);
                    LoadInvestorDetails(ID);
                    LoadInvestorContacts(ID);
                }
            }
        }
        private void LoadInvestorDetails(int ID)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Investors_Profile  WHERE Id=@Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", ID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                DateTime currentDateTime = DateTime.Now;
                lblTime.Text = currentDateTime.ToString();
                //lblCompanyCode.Text = reader["Company_Code"].ToString();
                lblCompanyName.Text = reader["Company_Name"].ToString();
                lblAddress.Text = reader["Company_Address"].ToString();
                //lblPostal.Text = reader["Company_Postal"].ToString();
                lblCountry.Text = reader["Company_Country"].ToString();
                lblPhone.Text = reader["Company_Phone"].ToString();
                lblWebsiteURL.Text = reader["Website"].ToString();
                lblYearFounded.Text = reader["Year_Founded"].ToString();
                lblHeadquaters.Text = reader["Headquaters"].ToString();
                lblBranches.Text = reader["Branches"].ToString();
                lblContactPerson.Text = reader["Company_Contact_Person"].ToString();
                lblContactEmail.Text = reader["Company_Contact_Email"].ToString();
                lblIndustryType.Text = reader["Company_Industry_Type"].ToString();
                lblInvestmentType.Text = reader["Investment_Type"].ToString();
                lblLandSizeRequired.Text = reader["Land_Size_Required"].ToString();
                lblPSFRM.Text = reader["PSF_RM"].ToString();
                lblBuildingSize.Text = reader["Building_Size"].ToString();
                lblElectricity.Text = reader["Electricity_Request"].ToString();
                lblWater.Text = reader["Water_Request"].ToString();
                lblNaturalGas.Text = reader["Natural_Gas_Request"].ToString();
                lblIndustrialGas.Text = reader["Industrial_Gas_Request"].ToString();
                lblProposedTotalInvestment.Text = reader["Proposed_Investment"].ToString();
                lblManpowerRequirement.Text = reader["Manpower_Request"].ToString();
                lblProjectTimeline.Text = reader["Project_Timeline"].ToString();
                string StartConstructionDate = reader["Start_Construction_Date"].ToString();
                if (!string.IsNullOrEmpty(StartConstructionDate) || StartConstructionDate == "01/01/1900")
                {
                    DateTime StartConstructionDate2 = DateTime.Parse(StartConstructionDate);
                    lblStartConstruction.Text = StartConstructionDate2.ToString("dd/MM/yyyy");
                }
                else
                { lblStartConstruction.Text = ""; }
                if (lblStartConstruction.Text == "01/01/1900") { lblStartConstruction.Text = ""; }
                string StartOperationDate = reader["Start_Operation_Date"].ToString();
                if (!string.IsNullOrEmpty(StartOperationDate))
                {
                    DateTime StartOperationDate2 = DateTime.Parse(StartOperationDate);
                    lblStartOperation.Text = StartOperationDate2.ToString("dd/MM/yyyy");
                }
                else
                { lblStartOperation.Text = ""; }
                if (lblStartOperation.Text == "01/01/1900") { lblStartOperation.Text = ""; }
                lblCompanyDescription.Text = reader["Company_Description"].ToString();
                lblInvestorsStatus.Text = reader["Company_Status"].ToString();
                LoadInvestorActivities(lblCompanyName.Text);
            }
        }

        private void LoadInvestorActivities(string CompanyName)
        {
            string selectedCompany = CompanyName.ToString();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, Company_Status, Activity_Date_Start, Activity_Description FROM Investors_Activities WHERE Company_Name = @CompanyName ORDER BY Activity_Date_Start DESC", con);
                cmd.Parameters.AddWithValue("@CompanyName", selectedCompany);
                // Execute the command and retrieve the results
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                // Bind the results to the Gridview
                gvActivities.DataSource = dt;
                gvActivities.DataBind();
                // Close the connection
                con.Close();
            }
        }
        private void LoadInvestorContacts(int ID)
        {                                         
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Position, Mobile, Notes FROM Contacts WHERE Company_Id = @Id ORDER BY Name ASC", con);
                cmd.Parameters.AddWithValue("@Id", ID);
                // Execute the command and retrieve the results
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                // Bind the results to the Gridview
                gvContacts.DataSource = dt;
                gvContacts.DataBind();
                // Close the connection
                con.Close();
            }
        }
    }
}
