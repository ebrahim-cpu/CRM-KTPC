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
    public partial class DeletePhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Photo_Id"]))
                {
                    int photoId = int.Parse(Request.QueryString["Photo_Id"]);
                    if (DeletePhotoFromDatabase(photoId))
                    {
                        ConfirmationMessage.Text = "Photo deleted successfully.";
                    }
                    else
                    {
                        ConfirmationMessage.Text = "Failed to delete the photo.";
                    }
                }
                else
                {
                    ConfirmationMessage.Text = "Photo ID is missing.";
                }
            }
        }

        private bool DeletePhotoFromDatabase(int photoId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Photo_Contact WHERE Photo_Id = @Photo_Id", connection))
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