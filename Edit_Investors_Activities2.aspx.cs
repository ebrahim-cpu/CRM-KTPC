using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class Edit_Investors_Activities2 : System.Web.UI.Page
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
                if (Request.QueryString["ID"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    //Get the record from the database using the ID
                    DataTable dt = GetActivityData(id);
                    if (dt.Rows.Count > 0)
                    {
                        //Populate the input fields with the data from the record
                        txtCompanyName.Text = dt.Rows[0]["Company_Name"].ToString();
                        string dateStringEnd = dt.Rows[0]["Activity_Date_End"].ToString();
                        DateTime ActivityDateEnd = DateTime.Parse(dateStringEnd);
                        txtActivityDateStart.Text = Convert.ToDateTime(dt.Rows[0]["Activity_Date_Start"]).ToString("yyyy-MM-dd");
                        txtActivityDescription.Text = dt.Rows[0]["Activity_Description"].ToString();                        
                        String txtInvestorStatus = dt.Rows[0]["Company_Status"].ToString();
                        // Set selected item in ddlInvestorStatus based on txtInvestorStatus
                        ListItem ItemTerpilih = ddlInvestorStatus.Items.FindByValue(txtInvestorStatus);                        
                        if (ItemTerpilih != null)
                        {
                            ddlInvestorStatus.SelectedIndex = ddlInvestorStatus.Items.IndexOf(ItemTerpilih);
                        }
                        hdnID.Value = dt.Rows[0]["ID"].ToString();
                        // Find the company ID based on the company name
                        hdnCompanyId.Value = GetCompanyIdByName(dt.Rows[0]["Company_Name"].ToString());
                        
                        string RFPDateStart = dt.Rows[0]["RFP_Date_Start"].ToString();
                        if (!string.IsNullOrEmpty(RFPDateStart))
                        {                            
                            txtRFPDateStart.Text = Convert.ToDateTime(dt.Rows[0]["RFP_Date_Start"]).ToString("yyyy-MM-dd");
                        }
                        else
                        { txtRFPDateStart.Text = ""; }
                        if (txtRFPDateStart.Text == "01/01/1900") { txtRFPDateStart.Text = ""; }
                        string RFPDateEnd = dt.Rows[0]["RFP_Date_End"].ToString();
                        if (!string.IsNullOrEmpty(RFPDateEnd))
                        {                            
                            txtRFPDateEnd.Text = Convert.ToDateTime(dt.Rows[0]["RFP_Date_End"]).ToString("yyyy-MM-dd");
                        }
                        else
                        { txtRFPDateEnd.Text = ""; }
                        if (txtRFPDateEnd.Text == "01/01/1900") { txtRFPDateEnd.Text = ""; }
                        string IsRFPstr = dt.Rows[0]["Is_RFP"].ToString();
                        bool IsRFP2 = bool.Parse(IsRFPstr);
                        IsRFP.Checked = IsRFP2;
                        txtRFPDescription.Text = dt.Rows[0]["RFP_Description"].ToString();
                        String txtDecisionStatus = dt.Rows[0]["Decision"].ToString();
                        // Set selected item in ddlInvestorStatus based on txtInvestorStatus
                        ListItem ItemTerpilih2 = ddlDecisionStatus.Items.FindByValue(txtDecisionStatus);
                        if (ItemTerpilih2 != null)
                        {
                            ddlDecisionStatus.SelectedIndex = ddlDecisionStatus.Items.IndexOf(ItemTerpilih2);
                        }
                        txtDecisionDescription.Text = dt.Rows[0]["Decision_Description"].ToString();
                        BindGridView(id);
                        BindPhotos(id);
                    }
                }
            }
        }
        private DataTable GetActivityData(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Investors_Activities WHERE Id = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtActivityDateStart.Text)) { txtActivityDateStart.Text = "1900-01-01"; };
            if (string.IsNullOrEmpty(txtRFPDateStart.Text)) { txtRFPDateStart.Text = "1900-01-01"; };
            if (string.IsNullOrEmpty(txtRFPDateEnd.Text)) { txtRFPDateEnd.Text = "1900-01-01"; };
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string q1 = "UPDATE Investors_Activities SET ";
                string q2 = "Activity_Type = @Activity_Type, Activity_Date_Start = @Activity_Date_Start, Activity_Date_End = @Activity_Date_End, ";
                string q3 = "Activity_Description = @Activity_Description, Company_Status = @Company_Status,";
                string q4 = "Decision = @Decision, Decision_Description = @Decision_Description, ";
                string q5 = "Is_RFP = @Is_RFP, RFP_Date_Start = @RFP_Date_Start, RFP_Date_End = @RFP_Date_End, RFP_Description = @RFP_Description, ";
                string q6 = "UpdatedBy = @UpdatedBy, Ip_Address = @Ip_Address, ";
                string q7 = "UpdatedDate = GETDATE() WHERE Id = @ID";
                string query = q1 + q2 + q3 + q4 + q5 + q6 + q7;
                //Debug.WriteLine(query);
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DateTime activityDate;
                    if (!DateTime.TryParse(txtActivityDateStart.Text, out activityDate))
                    {
                        activityDate = new DateTime(1900, 1, 1);
                    }

                    DateTime rfpDateStart;
                    if (!DateTime.TryParse(txtRFPDateStart.Text, out rfpDateStart))
                    {
                        rfpDateStart = new DateTime(1900, 1, 1);
                    }

                    DateTime rfpDateEnd;
                    if (!DateTime.TryParse(txtRFPDateEnd.Text, out rfpDateEnd))
                    {
                        rfpDateEnd = new DateTime(1900, 1, 1);
                    }

                    cmd.Parameters.AddWithValue("@Activity_Type", ""); // txtActivityType.Text
                    cmd.Parameters.AddWithValue("@Activity_Date_Start", activityDate);
                    cmd.Parameters.AddWithValue("@Activity_Date_End", activityDate); // Buang nanti kalau tak perlu.
                    cmd.Parameters.AddWithValue("@Activity_Description", txtActivityDescription.Text);
                    cmd.Parameters.AddWithValue("@ID", hdnID.Value);
                    cmd.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Decision", ddlDecisionStatus.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Decision_Description", txtDecisionDescription.Text);
                    cmd.Parameters.AddWithValue("@Is_RFP", IsRFP.Checked);
                    cmd.Parameters.AddWithValue("@RFP_Date_Start", rfpDateStart);
                    cmd.Parameters.AddWithValue("@RFP_Date_End", rfpDateEnd);
                    cmd.Parameters.AddWithValue("@RFP_Description", txtRFPDescription.Text);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Convert.ToString(Session["Username"]));
                    cmd.Parameters.AddWithValue("@Ip_Address", Convert.ToString(Session["Ip_Address"]));
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //Debug.Print(Session["FromLocation"].ToString());
            if (Convert.ToString(Session["FromLocation"]) == "List_RFP")
            {
                Response.Redirect("List_RFP.aspx");
            }
            else
            {
                // Redirect back to the filtered list using the stored company ID
                Response.Redirect("List_Investors_Activities2.aspx?id=" + hdnCompanyId.Value);
            }
        }

        private string GetCompanyIdByName(string companyName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id FROM Investors_Profile WHERE Company_Name = @Name", con);
                cmd.Parameters.AddWithValue("@Name", companyName);
                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();
                return result?.ToString() ?? "";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List_Investors_Activities2.aspx?id=" + GetCompanyIdByName(txtCompanyName.Text));
        }
        protected void setDateButton_Click(object sender, EventArgs e)
        {
            txtRFPDateStart.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            txtRFPDateEnd.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
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
                foreach (HttpPostedFile file in PhotoUpload1.PostedFiles)
                {
                    // Check file size and type
                    if (file.ContentLength > 52428800) // 50MB (in bytes)
                    {
                        lblMessage.Text = "File size limit exceeded (50MB). Please select a smaller file.";
                        return;
                    }
                    // Check file type
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!(fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".gif" || fileExtension == ".png"))
                    {
                        lblMessage.Text = "Invalid file type. Please upload a valid photo type (jpg, jpeg, gif, png).";
                        return;
                    }
                    //int fileSizeInBytes = (int)FileUpload1.FileContent.Length;
                    //double fileSizeInMb = fileSizeInBytes / 1024.0 / 1024.0;
                    byte[] fileBytes = PhotoUpload1.FileBytes;
                    //int fileSizeInMB = (int)Math.Ceiling((double)fileBytes.Length / 1024 / 1024);
                    int fileSize = fileBytes.Length;
                    string fileType = Path.GetExtension(PhotoUpload1.FileName).Replace(".", "").ToUpper();
                    byte[] fileData = new byte[file.ContentLength];
                    file.InputStream.Read(fileData, 0, file.ContentLength);
                    string constr = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("SP_InsertPhotoIActivity", conn)
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
                        // Add file information to the GridView
                        //DataTable dt = (DataTable)ViewState["Files"];
                        //dt.Rows.Add(Id, file.FileName);
                        //ViewState["Files"] = dt;
                    }
                }
                lblMessage.Text = "Files uploaded successfully.";
                //BindGridView();
            }
            else
            {
                lblMessage.Text = "Please select at least one file to upload.";
            }
        }
    }
}