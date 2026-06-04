using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRM
{
    public partial class company_contacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int companyId = 0;
                    if (int.TryParse(Request.QueryString["id"], out companyId))
                    {
                        LoadCompanyName(companyId);
                        LoadContacts(companyId);
                        lnkAddContact.NavigateUrl = "New_Contacts2.aspx?id=" + companyId;
                    }
                    else
                    {
                        lblMessage.Text = "Invalid Company ID format.";
                    }
                }
                else
                {
                    lblMessage.Text = "No Company ID provided.";
                }
            }
        }

        private void LoadCompanyName(int companyId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Company_Name FROM Investors_Profile WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", companyId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lblCompanyName.Text = " - " + result.ToString();
                }
            }
        }

        private void LoadContacts(int companyId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Position, Mobile, Notes FROM Contacts WHERE Company_Id = @Id ORDER BY Name ASC", con);
                cmd.Parameters.AddWithValue("@Id", companyId);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    rptContacts.DataSource = dt;
                    rptContacts.DataBind();
                }
                else
                {
                    lblMessage.Text = "No contacts found for this company.";
                }
            }
        }
    }
}
