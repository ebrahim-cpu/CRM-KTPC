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
    public partial class FileManage_Investors_Profile2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                LoadInvestorDetails(id);
                LoadInvestorContacts(id);
                BindGridView(id);
                BindGridView2();
                BindPhotos(id);                
            }
        }
        private void BindGridView(int Id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Document_IProfile WHERE IProfile_Id = @Id  ", connection);
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
                        SqlCommand cmd = new SqlCommand("SP_PhotoIProfile", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@IProfile_Id", ContactId);
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
                        string lorongFotoSasaran = Path.Combine(lorongFolderOutput, $"{namaFailSahaja}_Profil_{DateTime.Now:yyyyMMddHHmmss}{extensionAsal}");
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
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string filePath = lnk.CommandArgument;
            string backupFolder = Server.MapPath("~/Backup/Profile/");
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileExt = Path.GetExtension(filePath);
            string newFileName = fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExt;
            File.Copy(filePath, backupFolder + newFileName, true); // File Copy Operations.
            File.Delete(filePath); // File Delete Operations.
            int id = Convert.ToInt32(Request.QueryString["id"]);
            //BindFiles(id);
        }
        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string filePath = e.CommandArgument.ToString();
                Debug.WriteLine(filePath);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(filePath);
                Response.End();
            }
        }
        private void LoadInvestorDetails(int ID)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString);
            connection.Open();
            string query = "SELECT * FROM Investors_Profile  WHERE Id=@Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", ID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                DateTime currentDateTime = DateTime.Now;
                //lblTime.Text = currentDateTime.ToString();
                //lblCompanyCode.Text = reader["Company_Code"].ToString();
                lblCompanyName.Text = reader["Company_Name"].ToString();
                lblAddress.Text = reader["Company_Address"].ToString();
                //lblPostal.Text = reader["Company_Postal"].ToString();
                lblCountry.Text = reader["Company_Country"].ToString();
                lblPhone.Text = reader["Company_Phone"].ToString();
                lblWebsiteURL.Text = reader["Website"].ToString();
                lblYearFounded.Text = reader["Year_Founded"].ToString();
                lblHeadquaters.Text = reader["Headquaters"].ToString();
                lblBranches.Text = reader["Branches"].ToString();
                lblContactPerson.Text = reader["Company_Contact_Person"].ToString();
                lblContactEmail.Text = reader["Company_Contact_Email"].ToString();
                lblIndustryType.Text = reader["Company_Industry_Type"].ToString();
                lblInvestmentType.Text = reader["Investment_Type"].ToString();
                lblLandSizeRequired.Text = reader["Land_Size_Required"].ToString();
                lblPSFRM.Text = reader["PSF_RM"].ToString();
                lblBuildingSize.Text = reader["Building_Size"].ToString();
                lblElectricity.Text = reader["Electricity_Request"].ToString();
                lblWater.Text = reader["Water_Request"].ToString();
                lblNaturalGas.Text = reader["Natural_Gas_Request"].ToString();
                lblIndustrialGas.Text = reader["Industrial_Gas_Request"].ToString();
                lblProposedTotalInvestment.Text = reader["Proposed_Investment"].ToString();
                lblManpowerRequirement.Text = reader["Manpower_Request"].ToString();
                lblProjectTimeline.Text = reader["Project_Timeline"].ToString();
                string StartConstructionDate = reader["Start_Construction_Date"].ToString();
                if (!string.IsNullOrEmpty(StartConstructionDate) || StartConstructionDate == "01/01/1900")
                {
                    DateTime StartConstructionDate2 = DateTime.Parse(StartConstructionDate);
                    lblStartConstruction.Text = StartConstructionDate2.ToString("dd/MM/yyyy");
                }
                else
                { lblStartConstruction.Text = ""; }
                if (lblStartConstruction.Text == "01/01/1900") { lblStartConstruction.Text = ""; }
                string StartOperationDate = reader["Start_Operation_Date"].ToString();
                if (!string.IsNullOrEmpty(StartOperationDate))
                {
                    DateTime StartOperationDate2 = DateTime.Parse(StartOperationDate);
                    lblStartOperation.Text = StartOperationDate2.ToString("dd/MM/yyyy");
                }
                else
                { lblStartOperation.Text = ""; }
                if (lblStartOperation.Text == "01/01/1900") { lblStartOperation.Text = ""; }
                lblCompanyDescription.Text = reader["Company_Description"].ToString();
                lblInvestorsStatus.Text = reader["Company_Status"].ToString();
                LoadInvestorActivities(lblCompanyName.Text);
            }
        }
        private void LoadInvestorActivities(string CompanyName)
        {
            string selectedCompany = CompanyName.ToString();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Id, Company_Name, Company_Status, Activity_Date_Start, Activity_Description FROM Investors_Activities WHERE Company_Name = @CompanyName ORDER BY Activity_Date_Start DESC", con);
                cmd.Parameters.AddWithValue("@CompanyName", selectedCompany);
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
        private void LoadInvestorContacts(int ID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Open the connection
                con.Open();
                // Create a new SQL command
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, Position, Mobile, Notes FROM Contacts WHERE Company_Id = @Id ORDER BY Name ASC", con);
                cmd.Parameters.AddWithValue("@Id", ID);
                // Execute the command and retrieve the results
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                // Bind the results to the Gridview
                gvContacts.DataSource = dt;
                gvContacts.DataBind();
                // Close the connection
                con.Close();
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
                        SqlCommand cmd = new SqlCommand("SP_InsertDocumentIProfile", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@IProfile_Id", contactId);
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
                    "DELETE FROM Document_IProfile WHERE Document_Id = @FileId", connection);
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
                    string query = "SELECT Document_Id, FileName, ContentType, FileData FROM Photo_IProfile WHERE IProfile_Id =" + Id;
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
        // GridView2 Section
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the values from the data row
                string text = DataBinder.Eval(e.Row.DataItem, "BookMark_Text").ToString();
                string url = DataBinder.Eval(e.Row.DataItem, "BookMark_URL").ToString();
                // Create a clickable link with the specified text and URL
                HyperLink hyperLink = new HyperLink
                {
                    Text = text,
                    NavigateUrl = url,
                    Target = "_blank"
                };
                // Clear the cell and add the hyperlink control
                e.Row.Cells[1].Controls.Clear();
                e.Row.Cells[1].Controls.Add(hyperLink);
            }
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            BindGridView2();
        }
        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            BindGridView2();
        }
        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the updated values from the textboxes.
            GridViewRow row = GridView2.Rows[e.RowIndex];
            int id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);
            TextBox txtBookMarkText = (TextBox)row.FindControl("txtBookMark_Text");
            TextBox txtBookMarkDescription = (TextBox)row.FindControl("txtBookMark_Description");
            TextBox txtBookMarkURL = (TextBox)row.FindControl("txtBookMark_URL");
            TextBox txtBookMarkKeyword = (TextBox)row.FindControl("txtBookMark_Keyword");
            string newText = txtBookMarkText.Text;
            string newDescription = txtBookMarkDescription.Text;
            string newURL = txtBookMarkURL.Text;
            string newKeyword = txtBookMarkKeyword.Text;
            // Repeat the above code for other columns (Description, Category, Feedback).
            // Update the database with the new values.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                string updateQuery = "UPDATE Bookmark_iProfile " +
                                     "SET BookMark_Text = @Text, BookMark_URL = @URL, BookMark_Description = @Description, BookMark_Keyword = @Keyword, ModifiedBy = @ModifiedBy, ModifiedDate=@ModifiedDate " +
                                     "WHERE Bookmark_Id = @Id";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Text", newText);
                command.Parameters.AddWithValue("@Description", newDescription);
                command.Parameters.AddWithValue("@URL", newURL);
                command.Parameters.AddWithValue("@Keyword", newKeyword);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@ModifiedBy", Session["Username"]);
                command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    // Update successful.
                    GridView2.EditIndex = -1;
                }
                else
                {
                    // Handle update failure (e.g., display an error message).
                }
            }
            BindGridView2(); // Rebind data to reflect the changes.
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookmarkId = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM Bookmark_iProfile WHERE Bookmark_Id = @BookmarkId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@BookmarkId", bookmarkId);
                    try
                    {
                        connection.Open();
                        // Execute the delete query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Check if any rows were deleted
                        if (rowsAffected > 0)
                        {
                            // You can add a success message or refresh your GridView
                        }
                        else
                        {
                            // Handle the case where no rows were deleted
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Log the error, display an error message, or take appropriate action
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            BindGridView2(); // Rebind data after deleting.
        }
        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = "ASC"; // Default sorting direction

            if (ViewState["SortDirection"] != null)
            {
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    ViewState["SortDirection"] = "DESC";
                    sortDirection = "DESC";
                }
                else
                {
                    ViewState["SortDirection"] = "ASC";
                }
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            // Store the current sort information in ViewState
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;
            BindGridView2(sortExpression, sortDirection);
        }
        private void BindGridView2(string sortExpression = null, string sortDirection = null)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Bookmark_iProfile WHERE IProfile_Id = @IProfile_Id ";
                // Create the SqlCommand and add parameters
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IProfile_Id", Id);
                // Check if there is a sort expression and direction in ViewState
                if (!string.IsNullOrEmpty(sortExpression) && !string.IsNullOrEmpty(sortDirection))
                {
                    query += " ORDER BY " + sortExpression + " " + sortDirection;
                }
                else if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
                {
                    // Apply the previous sorting from ViewState
                    query += " ORDER BY " + ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
                }
                // Update the SqlCommand with the modified query
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dt = dataSet.Tables[0];
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
        }
        protected void BtnSimpan_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                // Create a new SqlConnection and SqlCommand to execute the INSERT query.
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO Bookmark_iProfile (IProfile_Id, BookMark_Text, BookMark_URL, BookMark_Description, BookMark_Keyword, CreatedBy, ModifiedBy) " +
                                         "VALUES (@IProfile_Id, @BookMark_Text, @BookMark_URL, @BookMark_Description, @BookMark_Keyword, @CreatedBy, @ModifiedBy)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        // Add parameters and their values.
                        cmd.Parameters.AddWithValue("@IProfile_Id", Id);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["Username"]);
                        cmd.Parameters.AddWithValue("@ModifiedBy", Session["Username"]);
                        cmd.Parameters.AddWithValue("@BookMark_Text", txt1.Text);
                        cmd.Parameters.AddWithValue("@BookMark_URL", txt2.Text);
                        cmd.Parameters.AddWithValue("@BookMark_Description", txt3.Text);
                        cmd.Parameters.AddWithValue("@BookMark_Keyword", txt4.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                // Data saved successfully, display a JavaScript alert and redirect.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data has been saved.'); window.location='" + ResolveUrl("Dashboard.aspx") + "';", true);
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log or display an error message if needed.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('An error occurred: " + ex.Message + "');", true);
            }
            BindGridView2(); // Rebind data after inserting.
        }

    }
}
