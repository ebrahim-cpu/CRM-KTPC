using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class FileManage_Investor_Activities2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                BindGridView(id);
                BindPhotos(id);
                
            }
        }

        protected void BtnUploadDocument_Click(object sender, EventArgs e)
        {
            int ActivityId = Convert.ToInt32(Request.QueryString["ID"]);
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
                        lblMessage.Text = "Invalid file type. Please upload a valid photo type (jpg, jpeg, gif, png).";
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
                        SqlCommand cmd = new SqlCommand("SP_InsertDocumentIActivity", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@IActivity_Id", ActivityId);
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
        protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            SortGridView(e.SortExpression);
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
        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int DocumentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex]["Document_Id"]);
            DeleteFile(DocumentId);
            BindGridView(DocumentId);
            e.Cancel = true;
        }
        private void DeleteFile(int fileId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Document_IActivity WHERE Document_Id = @FileId", connection);
                cmd.Parameters.AddWithValue("@FileId", fileId);
                cmd.ExecuteNonQuery();
            }
        }
        private void BindPhotos(int Id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Document_Id, FileName, ContentType, FileData FROM Photo_IActivity WHERE IActivity_Id =" + Id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dtPhotos = new DataTable();
                            adapter.Fill(dtPhotos);
                            repeaterPhotos.DataSource = dtPhotos;
                            repeaterPhotos.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred: " + ex.Message;
            }
        }
        private void BindGridView(int Id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Document_IActivity WHERE IActivity_Id = @Id  ", connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            int ActivityId = Convert.ToInt32(Request.QueryString["ID"]);
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
                        SqlCommand cmd = new SqlCommand("SP_InsertPhotoIActivity", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@IActivity_Id", ActivityId);
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
                        string lorongFotoSasaran = Path.Combine(lorongFolderOutput, $"{namaFailSahaja}_Activity_{DateTime.Now:yyyyMMddHHmmss}{extensionAsal}");
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

    }
}