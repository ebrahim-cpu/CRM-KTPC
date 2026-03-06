using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.CodeDom.Compiler;
using System.IO;

namespace CRM
{
    public partial class New_Investors_Activities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Session["Username"].ToString();
                if (string.IsNullOrEmpty(lblUsername.Text))
                {
                    Response.Redirect("Default.aspx");
                }                
                txtActivityDateStart.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") ;
                BindDropdownList();                
            }
        }
        private void BindDropdownList()
        {
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Company_Name FROM Investors_Profile", con);
                // Create a new SQL data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // Create a new data table
                DataTable dt = new DataTable();
                // Fill the data table with data from the database
                da.Fill(dt);
                // Bind the dropdown list with the data table
                ddlCompanyName.DataSource = dt;
                ddlCompanyName.DataBind();
                // Close the connection
                con.Close();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int id;
            if (string.IsNullOrEmpty(txtActivityDateStart.Text)) { txtActivityDateStart.Text = "1/1/1900"; };
            if (string.IsNullOrEmpty(txtRFPDateStart.Text)) { txtRFPDateStart.Text = "1/1/1900"; };
            if (string.IsNullOrEmpty(txtRFPDateEnd.Text)) { txtRFPDateEnd.Text = "1/1/1900"; };
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                string q1 = "INSERT INTO Investors_Activities ( ";
                string q2 = "Company_Name, Activity_Type, Activity_Date_Start, Activity_Date_End,";
                string q3 = "Activity_Description, Company_Status, Decision, Decision_Description, ";
                string q4 = "Is_RFP, RFP_Date_Start, RFP_Date_End, RFP_Description, CreatedBy, UpdatedBy, Ip_Address " ;
                string q5 = ") VALUES (";
                string q6 = "@Company_Name, @Activity_Type, @Activity_Date_Start, ";
                string q7 = "@Activity_Date_End, @Activity_Description,@Company_Status, @Decision, @Decision_Description, ";
                string q8 = "@Is_RFP, @RFP_Date_Start, @RFP_Date_End, @RFP_Description, ";
                string q9 = "@CreatedBy, @UpdatedBy, @Ip_Address ";
                string q10 = "); SELECT SCOPE_IDENTITY();";
                string query = q1 + q2 + q3 + q4 + q5 + q6 + q7 + q8 + q9 + q10;
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand(query, con);
                // Add the parameters to the SQL command
                cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                cmd.Parameters.AddWithValue("@Activity_Type", ""); // Hide sekejap txtActivityType
                cmd.Parameters.AddWithValue("@Activity_Date_Start", txtActivityDateStart.Text);
                cmd.Parameters.AddWithValue("@Activity_Date_End", txtActivityDateStart.Text); // Nanti buang kalau tak perlu
                cmd.Parameters.AddWithValue("@Activity_Description", txtActivityDescription.Text);
                cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@Decision", ddlDecisionStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@Decision_Description", txtDecisionDescription.Text);
                cmd.Parameters.AddWithValue("@Is_RFP", IsRFP.Checked);
                cmd.Parameters.AddWithValue("@RFP_Date_Start", txtRFPDateStart.Text);
                cmd.Parameters.AddWithValue("@RFP_Date_End", txtRFPDateEnd.Text);
                cmd.Parameters.AddWithValue("@RFP_Description", txtRFPDescription.Text);
                cmd.Parameters.AddWithValue("@Createdby", Session["Username"].ToString());
                cmd.Parameters.AddWithValue("@Updatedby", Session["Username"].ToString());
                cmd.Parameters.AddWithValue("@Ip_Address", Session["Ip_Address"].ToString());
                id = Convert.ToInt32(cmd.ExecuteScalar());
                // Execute the SQL command
                // int rowsInserted = cmd.ExecuteNonQuery();
                // Close the connection
                con.Close();
                // Save the files to the server                
                if (id > 0)
                {
                    // Show a success message
                    lblMessage.Text = "Data saved successfully";
                    Response.Redirect("List_Investors_Activities.aspx");
                }
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List_Investors_Activities.aspx");
        }
        protected void btnSaveRFPDetails_Click(object sender, EventArgs e)
        {
        }
        protected void btnSaveDecisionDetails_Click(object sender, EventArgs e)
        {
        }
    }
}
    



//ExecuteNonQuery is used to execute a Transact-SQL statement or a stored procedure that does not 
//    return a value, such as an INSERT, UPDATE, DELETE, or SET statement. It returns an integer indicating the number of rows affected by the query.

//ExecuteScalar, on the other hand, is used to execute a Transact-SQL statement or 
//a stored procedure that returns a single value, such as a SELECT COUNT(*) statement.
//    It returns the first column of the first row in the result set returned by the query. 
//    If the query returns multiple results or no results, it returns null.

//In the above example, ExecuteScalar is used to get the identity of the inserted row so 
//that it can be used to construct the folder name where the uploaded files will be saved.