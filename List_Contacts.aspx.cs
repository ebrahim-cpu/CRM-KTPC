using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class List_Contacts : System.Web.UI.Page
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
                else
                {
                    // Load data for the Company_Name dropdown filter.
                    BindCompanyDropdown();
                    // Load all contacts by default.
                    LoadContacts();
                }
            }
        }
        private void BindCompanyDropdown()
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT DISTINCT Company_Name FROM Contacts ORDER BY Company_Name ASC";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlCompanyFilter.DataSource = reader;
                    ddlCompanyFilter.DataTextField = "Company_Name";
                    ddlCompanyFilter.DataValueField = "Company_Name";
                    ddlCompanyFilter.DataBind();

                    ddlCompanyFilter.Items.Insert(0, new ListItem("All", ""));
                }
            }
        }
        private void LoadContacts()
        {
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Id, Company_Id, Company_Name, Name, Position, Mobile FROM Contacts";
                // Apply filtering if a specific Company_Name is selected in the dropdown.
                if (!string.IsNullOrEmpty(ddlCompanyFilter.SelectedValue))
                {
                    selectQuery += " WHERE Company_Name = @CompanyFilter";
                }
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    // Add parameter for the filtering.
                    cmd.Parameters.AddWithValue("@CompanyFilter", ddlCompanyFilter.SelectedValue);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvContacts.DataSource = reader;
                    gvContacts.DataBind();
                }
            }
        }
        protected void DdlCompanyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filter contacts based on the selected Company_Name and refresh the GridView.
            LoadContacts();
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            // Redirect to Edit_Contacts.aspx with the selected Company_Id in the query string.
            Button btnEdit = (Button)sender;
            int contactId = Convert.ToInt32(btnEdit.CommandArgument);
            Response.Redirect("Edit_Contacts.aspx?Id=" + contactId);
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Delete the contact based on the selected Id
            Button btnDelete = (Button)sender;
            int Id = Convert.ToInt32(btnDelete.CommandArgument);
            // Retrieve the connection string from the web.config file.
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM Contacts WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            // After deletion, refresh the GridView to update the contact list.
            LoadContacts();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Contacts WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Company_Phone LIKE '%' + @searchKeyword + '%' OR Name LIKE '%' + @searchKeyword + '%' OR Position LIKE '%' + @searchKeyword + '%' OR Mobile LIKE '%' + @searchKeyword + '%' OR Notes LIKE '%' + @searchKeyword + '%' OR ModifiedBy LIKE '%' + @searchKeyword + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvContacts.DataSource = dt;
                        gvContacts.DataBind();
                    }
                }
            }
        }
        protected void LnkSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                string searchKeyword = txtSearch.Text;
                string query = "SELECT * FROM Contacts WHERE Company_Name LIKE '%' + @searchKeyword + '%' OR Company_Phone LIKE '%' + @searchKeyword + '%' OR Name LIKE '%' + @searchKeyword + '%' OR Position LIKE '%' + @searchKeyword + '%' OR Mobile LIKE '%' + @searchKeyword + '%' OR Notes LIKE '%' + @searchKeyword + '%' OR ModifiedBy LIKE '%' + @searchKeyword + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchKeyword", searchKeyword);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvContacts.DataSource = dt;
                        gvContacts.DataBind();
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
            for (int k = 0; k < gvContacts.Columns.Count; k++)
            {
                //add separator
                sb.Append(gvContacts.Columns[k].HeaderText + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < gvContacts.Rows.Count; i++)
            {
                for (int k = 0; k < gvContacts.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(gvContacts.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
    }
}