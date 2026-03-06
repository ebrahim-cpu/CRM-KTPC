using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CRM.DisplayPhoto1;

namespace CRM
{
    public partial class DisplayPhoto2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if Contact_Id is present in the query string
                if (!string.IsNullOrEmpty(Request.QueryString["Contact_Id"]))
                {
                    int contactId = int.Parse(Request.QueryString["Contact_Id"]);
                    // Fetch photo data from the database
                    List<PhotoInfo> photos = GetPhotosFromDatabase(contactId);
                    if (photos.Count > 0)
                    {
                        // Display images and delete buttons
                        foreach (PhotoInfo photo in photos)
                        {
                            string imageFormat = GetImageFormat(photo.ContentType);
                            if (!string.IsNullOrEmpty(imageFormat))
                            {
                                // Convert the byte array into an image
                                System.Drawing.Image image = byteArrayToImage(photo.FileData);
                                // Create an image control
                                System.Web.UI.WebControls.Image imgControl = new System.Web.UI.WebControls.Image();
                                imgControl.ImageUrl = "data:image/" + imageFormat + ";base64," + Convert.ToBase64String(photo.FileData);
                                imgControl.AlternateText = "Image " + photo.Photo_Id;
                                // Create a delete button
                                Button deleteButton = new Button();
                                deleteButton.Text = "Delete";
                                deleteButton.PostBackUrl = "ConfirmDelete.aspx?Photo_Id=" + photo.Photo_Id;
                                //deleteButton.CommandArgument = photo.Photo_Id.ToString();
                                //deleteButton.Click += DeleteButton_Click;
                                // Add the image and delete button to a container
                                Panel imagePanel = new Panel();
                                imagePanel.Controls.Add(imgControl);
                                imagePanel.Controls.Add(deleteButton);
                                imageContainer.Controls.Add(imagePanel);
                            }
                        }
                    }
                }
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            int photoId = int.Parse(deleteButton.CommandArgument);

            // Display a confirmation dialog
            string confirmScript = $"if (confirm('Are you sure you want to delete this photo?')) {{ window.location = 'DeletePhoto.aspx?Photo_Id={photoId}' }}";
            ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmDelete", confirmScript, true);
        }
        private List<PhotoInfo> GetPhotosFromDatabase(int contactId)
        {
            List<PhotoInfo> photos = new List<PhotoInfo>();
            // Connect to the database and execute SQL query
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Photo_Id, FileData, ContentType FROM Photo_Contact WHERE Contact_Id = @Contact_Id", connection))
                {
                    command.Parameters.AddWithValue("@Contact_Id", contactId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhotoInfo photo = new PhotoInfo
                            {
                                Photo_Id = (int)reader["Photo_Id"],
                                FileData = (byte[])reader["FileData"],
                                ContentType = reader["ContentType"].ToString()
                            };
                            photos.Add(photo);
                        }
                    }
                }
            }
            return photos;
        }
        private string GetImageFormat(string contentType)
        {
            switch (contentType.ToLower())
            {
                case "image/jpeg":
                case "image/jpg":
                    return "jpeg";
                case "image/gif":
                    return "gif";
                case "image/png":
                    return "png";
                default:
                    return string.Empty;
            }
        }
        private System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }

    }
}