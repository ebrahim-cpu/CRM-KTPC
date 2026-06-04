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
    public partial class List_Investors_Profile2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                lblUsername.Text = Convert.ToString(Session["Username"]);
                if (string.IsNullOrEmpty(lblUsername.Text)) {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    gvActivities.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                    LoadDataAndBind();
                }
            }
        }

        private void LoadDataAndBind()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                string query;
                SqlCommand cmd;
                string searchKeyword = txtSearch.Text.Trim();
                
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    query = "SELECT Id, Company_Name, Date_Inquiry, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status, PIC, Investors_Decision_Making, Investors_Internal_Status FROM Investors_Profile ORDER BY Company_Name ASC";
                    cmd = new SqlCommand(query, con);
                }
                else
                {
                    query = "SELECT Id, Company_Name, Date_Inquiry, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status, PIC, Investors_Decision_Making, Investors_Internal_Status FROM Investors_Profile WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Company_Country LIKE '%' + @searchKeyword + '%' OR Company_Phone LIKE '%' + @searchKeyword + '%' OR Company_Contact_Email LIKE '%' + @searchKeyword + '%' OR Company_Industry_Type LIKE '%' + @searchKeyword + '%' OR Investment_Type LIKE '%' + @searchKeyword + '%' OR Leads_Status LIKE '%' + @searchKeyword + '%' OR PIC LIKE '%' + @searchKeyword + '%' ORDER BY Company_Name ASC";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                }

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                gvActivities.DataSource = dt;
                gvActivities.DataBind();
                con.Close();
            }
        }

        protected void DisplayAll()
        {            
            LoadDataAndBind();
        }
        protected void GvActivities_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Get the selected record's ID and Company Name
                int selectedId = (int)gvActivities.DataKeys[e.RowIndex].Values["Id"];
                string companyName = gvActivities.DataKeys[e.RowIndex].Values["Company_Name"].ToString();
                
                // Create a new SQL connection
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    // Open the connection
                    con.Open();
                    
                    // 1. Delete associated activities first to avoid foreign key constraints
                    // Note: The relationship seems to be based on Company_Name
                    SqlCommand cmdActivities = new SqlCommand("DELETE FROM Investors_Activities WHERE Company_Name = @CompanyName", con);
                    cmdActivities.Parameters.AddWithValue("@CompanyName", companyName);
                    cmdActivities.ExecuteNonQuery();

                    // 2. Delete the investor profile
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted');", true);
            }
            catch (Exception ex)
            {
                // Show error message if deletion fails
                string errorMsg = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error deleting record: " + errorMsg + "');", true);
            }
        }
        private void BindGridview()
        {
            LoadDataAndBind();
        }

        protected void gvActivities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActivities.PageIndex = e.NewPageIndex;
            LoadDataAndBind();
        }

        protected void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvActivities.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            gvActivities.PageIndex = 0;
            LoadDataAndBind();
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
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, Date_Inquiry, PIC, Company_Country, Land_Size_Required, Building_Size, Company_Status, Company_Industry_Type, Investment_Type, Leads_Status, Investors_Decision_Making, Investors_Internal_Status FROM Investors_Profile Order by Company_Name ASC", conn))
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
            gvActivities.PageIndex = 0;
            LoadDataAndBind();
        }
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=List_Investors_Profile2.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            
            DataTable dt = GetActivitiesData();
            StringBuilder sb = new StringBuilder();
            
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                sb.Append(dt.Columns[k].ColumnName + ",");
            }
            sb.Append("\r\n");
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    string cellData = dt.Rows[i][k].ToString().Replace(",", " ");
                    // formatting dates if applicable
                    if (dt.Columns[k].DataType == typeof(DateTime) && !string.IsNullOrEmpty(dt.Rows[i][k].ToString()))
                    {
                        cellData = Convert.ToDateTime(dt.Rows[i][k]).ToString("dd-MMM-yyyy");
                    }
                    sb.Append(cellData + ",");
                }
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
            gvActivities.PageIndex = 0;
            LoadDataAndBind();
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
                    case "Completed":
                    cssClass = "green-status";
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