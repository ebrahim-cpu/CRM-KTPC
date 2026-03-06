using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class ConfirmDelete_ActivityPhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Photo_Id"]))
                {
                    int photoId = int.Parse(Request.QueryString["Photo_Id"]);
                    ConfirmationMessage.Text = "Are you sure you want to delete this photo?";
                    ConfirmButton.CommandArgument = photoId.ToString();
                }
                else
                {
                    ConfirmationMessage.Text = "Photo ID is missing.";
                    ConfirmButton.Enabled = false;
                }
            }
        }
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            Button confirmButton = (Button)sender;
            int photoId = int.Parse(confirmButton.CommandArgument);
            if (DeletePhotoFromDatabase(photoId))
            {
                // Redirect back to the original page or a different page as needed
                Response.Redirect("List_Investors_Activities.aspx");
            }
            else
            {
                ConfirmationMessage.Text = "Failed to delete the photo.";
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the original page
            Response.Redirect("List_Investors_Activities.aspx");
        }
        private bool DeletePhotoFromDatabase(int photoId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Photo_IActivity WHERE Document_Id = @Photo_Id", connection))
                    {
                        command.Parameters.AddWithValue("@Photo_Id", photoId);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false;
            }
        }
    }
}