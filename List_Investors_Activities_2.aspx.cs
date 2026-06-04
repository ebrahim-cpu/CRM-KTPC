using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace CRM
{
    public partial class List_Investors_Activities_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Convert.ToString(Session["Username"]);
            if (string.IsNullOrEmpty(lblUsername.Text))
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                // Create a new SQL connection
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    // Open the connection
                    con.Open();
                    // Create a new SQL command
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Company_Name FROM Investors_Profile", con);
                    // Execute the command and retrieve the results
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    // Bind the results to the drop down list
                    ddlCompanyName.DataSource = dt;
                    ddlCompanyName.DataTextField = "Company_Name";
                    ddlCompanyName.DataValueField = "Company_Name";
                    ddlCompanyName.DataBind();
                    ddlCompanyName.Items.Add(new ListItem("All", ""));
                    ddlCompanyName.Items.FindByText("All").Selected = true; 
                    // Finds the ListItem with the text "All" using the FindByText method and sets its Selected property to "true".
                    // Close the connection
                    con.Close();
                }
                displayAll();
            }
        }
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value from the dropdown
            string selectedCompany = ddlCompanyName.SelectedValue;
            // Debug.WriteLine(selectedCompany); ddlCompanyName mesti AutoPostBack True.
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                string q1 = "SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, ";
                string q2 = "Company_Status, UpdatedDate, UpdatedBy ";
                string q3 = "FROM Investors_Activities WHERE ";
                string q4 = "(@Company_Name = '' OR Company_Name = @Company_Name) AND (@Company_Status = '' OR Company_Status = @Company_Status) ";
                string q5 = "ORDER BY Activity_Date_Start DESC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;
                
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
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

        protected void ddlInvestorStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value from the dropdown
            string selectedCompany = ddlCompanyName.SelectedValue;
            // Debug.WriteLine(selectedCompany); ddlCompanyName mesti AutoPostBack True.
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                string q1 = "SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, ";
                string q2 = "Company_Status, UpdatedDate, UpdatedBy ";
                string q3 = "FROM Investors_Activities WHERE ";
                string q4 = "(@Company_Name = '' OR Company_Name = @Company_Name) AND (@Company_Status = '' OR Company_Status = @Company_Status) ";
                string q5 = "ORDER BY Activity_Date_Start DESC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;

                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
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


        protected void displayAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                // Create a new SQL command

                string q1 = "SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, ";
                string q2 = "Company_Status, UpdatedDate, UpdatedBy ";
                string q3 = "FROM Investors_Activities ";
                string q4 = "";
                string q5 = "ORDER BY Activity_Date_Start DESC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;
                SqlCommand cmd = new SqlCommand(sqlStr, con);                // Execute the command and retrieve the results
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                // Bind the results to the Gridview
                gvActivities.DataSource = dt;
                gvActivities.DataBind();
                // Close the connection
                con.Close();
            }
        }

        protected void gvActivities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the selected record's ID
            int selectedId = (int)gvActivities.DataKeys[e.RowIndex].Value;
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("DELETE FROM Investors_Activities WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", selectedId);
                // Execute the command
                cmd.ExecuteNonQuery();
                // Close the connection
                con.Close();
            }
            // Bind the Gridview again to refresh the data
            BindGridview();
            // Show a notification to the user
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully');", true);
        }
        private void BindGridview()
        {
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                string q1 = "SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, ";
                string q2 = "Company_Status, UpdatedDate, UpdatedBy ";
                string q3 = "FROM Investors_Activities WHERE ";
                string q4 = "(@Company_Name = '' OR Company_Name = @Company_Name) AND (@Company_Status = '' OR Company_Status = @Company_Status) ";
                string q5 = "ORDER BY Activity_Date_Start DESC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;
                SqlCommand cmd = new SqlCommand(sqlStr, con); 
                cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
                // Create a new SQL data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // Create a new data table
                DataTable dt = new DataTable();
                // Fill the data table with data from the database
                da.Fill(dt);
                // Bind the Gridview with the data table
                gvActivities.DataSource = dt;
                gvActivities.DataBind();
                // Close the connection
                con.Close();
            }
        }

        protected void btnFiles_Click(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("FileManage_Investor_Activities.aspx?id=" + id);
        }

        protected void gvActivities_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Retrieve the data source
            DataTable dt = GetActivitiesData();
            // Sort the data based on the column that was clicked
            dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            // Bind the data to the GridView
            gvActivities.DataSource = dt;
            gvActivities.DataBind();
        }
        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending
            string sortDirection = "ASC";
            // Retrieve the last column that was sorted
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                // Check if the same column was clicked again
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            // Save the new values in ViewState for future reference
            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;
            return sortDirection;
        }
        private DataTable GetActivitiesData()
        {
            //Create a new DataTable to store the retrieved data
            DataTable dtActivities = new DataTable();
            //Create a new connection to the database
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                //Open the connection
                conn.Open();
                //Create a new SqlCommand to retrieve the data
                string q1 = "SELECT Id, Company_Name, Activity_Type, Activity_Date_Start, Activity_Description, ";
                string q2 = "Company_Status, UpdatedDate, UpdatedBy ";
                string q3 = "FROM Investors_Activities WHERE ";
                string q4 = "(@Company_Name = '' OR Company_Name = @Company_Name) AND (@Company_Status = '' OR Company_Status = @Company_Status) ";
                string q5 = "ORDER BY Activity_Date_Start DESC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;
                
                
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    //Create a new SqlDataAdapter to fill the DataTable with the retrieved data
                    cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        //Fill the DataTable with the retrieved data
                        da.Fill(dtActivities);
                    }
                    cmd.Parameters.AddWithValue("@Company_Name", ddlCompanyName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue);
                }
                //Close the connection
                conn.Close();
            }
            //Return the retrieved data as a DataTable
            return dtActivities;
        }

        protected void gvActivities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = (int)gvActivities.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("Edit_Investors_Activities.aspx?ID=" + id);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Investors_Activities WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Activity_Type LIKE '%' + @searchKeyword + '%' OR Activity_Description LIKE '%' + @searchKeyword + '%' OR Guid LIKE '%' + @searchKeyword + '%' OR Company_Status LIKE '%' + @searchKeyword + '%'  OR UpdatedDate LIKE '%' + @searchKeyword + '%'  OR UpdatedBy LIKE '%' + @searchKeyword + '%'  ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvActivities.DataSource = dt;
                        gvActivities.DataBind();
                    }
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Universal csv export. Any gridview boleh.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=List_Investors_Activities.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < gvActivities.Columns.Count; k++)
            {
                //add separator
                sb.Append(gvActivities.Columns[k].HeaderText + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < gvActivities.Rows.Count; i++)
            {
                for (int k = 0; k < gvActivities.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(gvActivities.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Investors_Activities WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Activity_Type LIKE '%' + @searchKeyword + '%' OR Activity_Description LIKE '%' + @searchKeyword + '%' OR Guid LIKE '%' + @searchKeyword + '%' OR Company_Status LIKE '%' + @searchKeyword + '%'  OR UpdatedDate LIKE '%' + @searchKeyword + '%'  OR UpdatedBy LIKE '%' + @searchKeyword + '%'  ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvActivities.DataSource = dt;
                        gvActivities.DataBind();
                    }
                }
            }
        }

    }
}