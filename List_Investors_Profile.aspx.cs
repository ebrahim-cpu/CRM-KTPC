using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;

namespace CRM
{
    public partial class List_Investors_Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                lblUsername.Text = Session["Username"].ToString();
                if (string.IsNullOrEmpty(lblUsername.Text)) {
                    Response.Redirect("Default.aspx");
                }
                else
                DisplayAll();
            }
        }
        protected void DisplayAll()
        {            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, PIC, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status FROM Investors_Profile Order by Company_Name ASC", con);
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
        protected void GvActivities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the selected record's ID
            int selectedId = (int)gvActivities.DataKeys[e.RowIndex].Value;
            // Create a new SQL connection
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("DELETE FROM Investors_Profile WHERE Id = @Id", con);
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
                SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status FROM Investors_Profile Order by Company_Name ASC", con);
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
        protected void BtnFiles_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("FileManage_Investors_Profile.aspx?id=" + id);
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("Print_Investors_Profile.aspx?id=" + id);
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("Edit_Investors_Profile.aspx?id=" + id);
        }
        protected void GvActivities_Sorting(object sender, GridViewSortEventArgs e)
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
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, PIC, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status FROM Investors_Profile Order by Company_Name ASC", conn))
                {
                    //Create a new SqlDataAdapter to fill the DataTable with the retrieved data
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        //Fill the DataTable with the retrieved data
                        da.Fill(dtActivities);
                    }
                }
                conn.Close();
            }
            //Return the retrieved data as a DataTable
            return dtActivities;
        }
        protected void GvActivities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = (int)gvActivities.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("Edit_Investors_Profile.aspx?ID=" + id);
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Investors_Profile WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Company_Country LIKE '%' + @searchKeyword + '%' OR Company_Phone LIKE '%' + @searchKeyword + '%' OR Company_Contact_Email LIKE '%' + @searchKeyword + '%' OR Company_Industry_Type LIKE '%' + @searchKeyword + '%' OR Investment_Type LIKE '%' + @searchKeyword + '%' OR Leads_Status LIKE '%' + @searchKeyword + '%'";
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
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // Universal csv export. Any gridview boleh.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=List_Investors_Profile.csv");
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
        protected void GvActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvActivities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void LnkSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Investors_Profile WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Company_Country LIKE '%' + @searchKeyword + '%' OR Company_Phone LIKE '%' + @searchKeyword + '%' OR Company_Contact_Email LIKE '%' + @searchKeyword + '%' OR Company_Industry_Type LIKE '%' + @searchKeyword + '%' OR Investment_Type LIKE '%' + @searchKeyword + '%' OR Leads_Status LIKE '%' + @searchKeyword + '%' OR PIC LIKE '%' + @searchKeyword + '%'";
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
        protected string GetLeadsStatusStyle(string leadsStatus)
        {
            string cssClass = "";
            switch (leadsStatus)
            {
                case "Hot":
                    cssClass = "hot-status";
                    break;
                case "Warm":
                    cssClass = "warm-status";
                    break;
                case "Cold":
                    cssClass = "cold-status";
                    break;
                case "Greenfield":
                    cssClass = "green-status";
                    break;
                case "Brownfield":
                    cssClass = "brown-status";
                    break;
                // Add more cases if needed
                default:
                    break;
            }
            return cssClass;
        }
    }
}