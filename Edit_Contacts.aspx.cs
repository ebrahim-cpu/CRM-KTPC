using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRM
{
    public partial class Edit_Contacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Session["Username"].ToString();
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    int contactId = Convert.ToInt32(Request.QueryString["Id"]);
                    PopulateContactDetails(contactId);
                    BindGridView(contactId);
                    PopulateContactPhotos(contactId);
                }
                else
                {
                    Response.Redirect("List_Contacts.aspx");
                }
            }
        }
        private void PopulateContactDetails(int contactId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Company_Id, Company_Name, Company_Phone, Name, Position, Mobile, Notes " +
                                     "FROM Contacts WHERE Id = @ContactId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ContactId", contactId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate the fields with the retrieved data.
                        txtCompanyId.Text = reader["Company_Id"].ToString();
                        txtCompanyName.Text = reader["Company_Name"].ToString();
                        txtCompanyPhone.Text = reader["Company_Phone"].ToString();
                        txtName.Text = reader["Name"].ToString();
                        txtPosition.Text = reader["Position"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        txtNotes.Text = reader["Notes"].ToString();
                    }
                    else
                    {
                        // If the contact with the provided "Id" is not found, redirect to the appropriate page.
                        Response.Redirect("List_Contacts.aspx");
                    }
                }
            }
        }
        private void PopulateContactPhotos(int contactId)
        {
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
                        System.Drawing.Image image = ByteArrayToImage(photo.FileData);
                        // Create an image control
                        System.Web.UI.WebControls.Image imgControl = new System.Web.UI.WebControls.Image
                        {
                            ImageUrl = "data:image/" + imageFormat + ";base64," + Convert.ToBase64String(photo.FileData),
                            AlternateText = "Image " + photo.Photo_Id
                        };
                        // Create a delete button
                        Button deleteButton = new Button
                        {
                            Text = "Delete",
                            PostBackUrl = "ConfirmDelete.aspx?Photo_Id=" + photo.Photo_Id
                        };
                        Panel imagePanel = new Panel();
                        imagePanel.Controls.Add(imgControl);
                        imagePanel.Controls.Add(deleteButton);
                        imageContainer.Controls.Add(imagePanel);
                    }
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Update the contact information in the database.
            int contactId = Convert.ToInt32(Request.QueryString["Id"]);
            string companyName = txtCompanyName.Text;
            string companyPhone = txtCompanyPhone.Text;
            string name = txtName.Text;
            string position = txtPosition.Text;
            string mobile = txtMobile.Text;
            string notes = txtNotes.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Contacts SET Company_Name = @CompanyName, Company_Phone = @CompanyPhone, " +
                                     "Name = @Name, Position = @Position, Mobile = @Mobile, Notes = @Notes " +
                                     "WHERE Id = @ContactId";
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@CompanyPhone", companyPhone);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@ContactId", contactId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            // After saving, redirect the user to a confirmation page or any other desired page.
            Response.Redirect("List_Contacts.aspx");
        }
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            int ContactId = Convert.ToInt32(Request.QueryString["Id"]);
            if (PhotoUpload1.HasFiles)
            {
                foreach (HttpPostedFile muatNaikFail in PhotoUpload1.PostedFiles)
                {
                    string lorongFolderOutput = Server.MapPath("~/ConvertedImages/");
                    string lorongFolderFotoAsal = Server.MapPath("~/OriginalPhoto/");
                    string namaFailAsal = Path.GetFileName(muatNaikFail.FileName);
                    string namaFailSahaja = Path.GetFileNameWithoutExtension(muatNaikFail.FileName);
                    string extensionAsal = Path.GetExtension(namaFailAsal).ToLower();
                    if (muatNaikFail.ContentLength > 52428800) // 50MB (in bytes)
                    {
                        lblMessage.Text = "File size limit exceeded (50MB). Please select a smaller file.";
                        return;
                    }
                    string extenxionFail = Path.GetExtension(muatNaikFail.FileName).ToLower();
                    if (!(extenxionFail == ".jpg" || extenxionFail == ".jpeg" || extenxionFail == ".gif" || extenxionFail == ".png"))
                    {
                        lblMessage.Text = "Invalid file type. Please upload a valid photo type (jpg, jpeg, gif, png).";
                        return;
                    }
                    string namaFailSementara = $"{Guid.NewGuid()}{extensionAsal}";
                    string lorongFailSementara = Path.Combine(lorongFolderOutput, namaFailSementara);
                    muatNaikFail.SaveAs(lorongFailSementara);
                    // Resize and get the resized image data
                    byte[] resizedImageData;
                    using (System.Drawing.Image gambarAsal = System.Drawing.Image.FromFile(lorongFailSementara))
                    {
                        if (ImageResizer.IsImageWidthGreaterThanOrEqualTo(gambarAsal, 640))
                        {
                            int lebarBaru = 640;
                            int tinggiBaru = (int)(((float)lebarBaru / gambarAsal.Width) * gambarAsal.Height);
                            using (System.Drawing.Image gambarBersaizBaru = new Bitmap(lebarBaru, tinggiBaru))
                            using (Graphics geraphics = Graphics.FromImage(gambarBersaizBaru))
                            {
                                geraphics.DrawImage(gambarAsal, 0, 0, lebarBaru, tinggiBaru);
                                // Convert the resized image to a byte array
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    gambarBersaizBaru.Save(ms, gambarAsal.RawFormat);
                                    resizedImageData = ms.ToArray();
                                }
                            }
                        }
                        else
                        {
                            // If no resizing needed, use the original image data
                            using (MemoryStream ms = new MemoryStream())
                            {
                                gambarAsal.Save(ms, gambarAsal.RawFormat);
                                resizedImageData = ms.ToArray();
                            }
                        }
                    }
                    // Save the resized image data to the database
                    int saizFail = resizedImageData.Length;
                    string jenisFail = extensionAsal.Replace(".", "").ToUpper();
                    string constr = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertPhotoContact", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@Contact_Id", ContactId);
                        cmd.Parameters.AddWithValue("@FileName", muatNaikFail.FileName);
                        cmd.Parameters.AddWithValue("@ContentType", muatNaikFail.ContentType);
                        cmd.Parameters.AddWithValue("@FileData", resizedImageData);
                        cmd.Parameters.AddWithValue("@FileSize", saizFail);
                        cmd.Parameters.AddWithValue("@FileType", jenisFail);
                        cmd.Parameters.AddWithValue("@UploadedBy", "Sistem");
                        int Id = 0;
                        try
                        {
                            conn.Open();
                            Id = (int)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            // handle the exception here
                        }
                        finally
                        {
                            conn.Close();
                        }
                        // Save the resized image to the "ConvertedImages" folder
                        string lorongFotoSasaran = Path.Combine(lorongFolderOutput, $"{namaFailSahaja}_Contact_{DateTime.Now:yyyyMMddHHmmss}{extensionAsal}");
                        using (MemoryStream ms = new MemoryStream(resizedImageData))
                        using (System.Drawing.Image gambarBersaiz = System.Drawing.Image.FromStream(ms))
                        {
                            gambarBersaiz.Save(lorongFotoSasaran, gambarBersaiz.RawFormat);
                        }
                    }
                    File.Delete(lorongFailSementara);
                    string lorongFotoAsal = Path.Combine(lorongFolderFotoAsal, $"{namaFailAsal}_{DateTime.Now:yyyyMMddHHmmss}{extensionAsal}");
                    muatNaikFail.SaveAs(lorongFotoAsal);
                }
                lblMessage.Text = "Files uploaded and resized successfully.";
            }
            else
            {
                lblMessage.Text = "Please select at least one file to upload.";
            }
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
        private System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }
        protected void BtnUploadDocument_Click(object sender, EventArgs e)
        {
            int contactId = Convert.ToInt32(Request.QueryString["Id"]);
            if (DocumentUpload1.HasFiles)
            {
                foreach (HttpPostedFile file in DocumentUpload1.PostedFiles)
                {
                    // Check file size and type
                    if (file.ContentLength > 52428800) // 50MB (in bytes)
                    {
                        lblMessage.Text = "File size limit exceeded (50MB). Please select a smaller file.";
                        return;
                    }
                    // Check file type
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!(fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".gif" || fileExtension == ".png" || fileExtension == ".docx" || fileExtension == ".xlsx" || fileExtension == ".pptx" || fileExtension == ".pdf"))
                    {
                        lblMessage.Text = "Invalid file type. Please upload a valid document type (jpg, jpeg, gif, png).";
                        return;
                    }
                    //int fileSizeInBytes = (int)FileUpload1.FileContent.Length;
                    byte[] fileBytes = DocumentUpload1.FileBytes;
                    //int fileSizeInMB = (int)Math.Ceiling((double)fileBytes.Length / 1024 / 1024);
                    int fileSize = fileBytes.Length;
                    //string fileType = Path.GetExtension(FileUpload1.FileName);
                    string fileType = Path.GetExtension(DocumentUpload1.FileName).Replace(".", "").ToUpper();
                    byte[] fileData = new byte[file.ContentLength];
                    file.InputStream.Read(fileData, 0, file.ContentLength);
                    string constr = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertDocumentContact", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@Contact_Id", contactId);
                        cmd.Parameters.AddWithValue("@FileName", file.FileName);
                        cmd.Parameters.AddWithValue("@ContentType", file.ContentType);
                        cmd.Parameters.AddWithValue("@FileData", fileData);
                        cmd.Parameters.AddWithValue("@FileSize", fileSize);
                        cmd.Parameters.AddWithValue("@FileType", fileType);
                        cmd.Parameters.AddWithValue("@UploadedBy", "Sistem");
                        int Id = 0;
                        try
                        {
                            conn.Open();
                            Id = (int)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            // handle the exception here
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                lblMessage.Text = "Files uploaded successfully.";
            }
            else
            {
                lblMessage.Text = "Please select at least one file to upload.";
            }
        }
        private void DeleteFile(int fileId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Document_Contact WHERE Document_Id = @FileId", connection);
                cmd.Parameters.AddWithValue("@FileId", fileId);
                cmd.ExecuteNonQuery();
            }
        }
        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int DocumentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex]["Document_Id"]);
            DeleteFile(DocumentId);
            BindGridView(DocumentId);
            e.Cancel = true;
        }
        private void SortGridView(string sortExpression)
        {
            int contactId = Convert.ToInt32(Request.QueryString["Id"]);
            string direction = "ASC";
            if (ViewState["SortDirection"] != null && ViewState["SortDirection"].ToString() == "ASC")
            {
                direction = "DESC";
            }
            ViewState["SortDirection"] = direction;
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Document_Id, Contact_Id, FileName,ContentType, FileSize, FileType FROM Document_Contact WHERE Contact_Id = @ContactId";
                query += " ORDER BY " + sortExpression + " " + direction;
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ContactId", contactId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            SortGridView(e.SortExpression);
        }
        private void BindGridView(int contactId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Document_Contact WHERE Contact_Id = @Contact_Id  ", connection);
                cmd.Parameters.AddWithValue("@Contact_Id", contactId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }
}