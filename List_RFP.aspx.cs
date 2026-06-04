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

namespace CRM
{
    public partial class List_RFP2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Convert.ToString(Session["Username"]);
                Session["FromLocation"] = "List_RFP";
                BindGridView();
                lblUsername.Text = Convert.ToString(Session["Username"]);
                if (string.IsNullOrEmpty(lblUsername.Text))
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        protected void BindGridView()
        {
            // Assuming you have a connection string named "ConnectionString"
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, Days_Till_Expire, RFP_Date_Start, RFP_Date_End FROM Investors_Activities where Is_RFP = 1 ", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvActivities.DataSource = dt;
                        gvActivities.DataBind();
                    }
                }
            }
        }
        protected void gvActivities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int daysTillExpire = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Days_Till_Expire"));

                if (daysTillExpire <= 10)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                else if (daysTillExpire >= 11 && daysTillExpire < 20)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Universal csv export. Any gridview boleh.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=List_RFP.csv");
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
                string q1 = "SELECT Id, Company_Name, Days_Till_Expire, RFP_Date_Start, RFP_Date_End , Is_RFP ";
                string q2 = "";
                string q3 = "FROM Investors_Activities WHERE ";
                string q4 = "Is_RFP = 1 ";
                string q5 = "ORDER BY Days_Till_Expire ASC";
                string sqlStr = q1 + q2 + q3 + q4 + q5;


                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    //Create a new SqlDataAdapter to fill the DataTable with the retrieved data
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        //Fill the DataTable with the retrieved data
                        da.Fill(dtActivities);
                    }
                }
                //Close the connection
                conn.Close();
            }
            //Return the retrieved data as a DataTable
            return dtActivities;
        }
        protected void btnFiles_Click(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("FileManage_Investor_Activities.aspx?id=" + id);
        }
        protected void gvActivities_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = (int)gvActivities.DataKeys[e.NewEditIndex].Value;
            Response.Redirect("Edit_Investors_Activities.aspx?ID=" + id);
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
            BindGridView();
            // Show a notification to the user
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully');", true);
        }
        protected void btnUpdateRFP_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("UpdateInvestorActivities", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        BindGridView();
                        lblMessage.Text = "Update Success";
                    }
                }
            }
            catch (Exception ex)
            {
                // Display the error message to the user
                string errorMessage = "An error occurred while updating the investor activities. Please contact your system administrator.";
                errorMessage += "<br /><br />" + ex.Message;
                lblMessage.Text = errorMessage;
            }

        }
    }
}