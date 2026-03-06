using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.PeerToPeer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class Edit_Investors_Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Session[name: "Username"].ToString();
                // Load the existing data from the database.
                int id = Convert.ToInt32(Request.QueryString["id"]);
                hdnID.Value = id.ToString();
                BindCountry(); // ddlCountry
                BindPromotedSector(); // ddlPromotedSector
                BindInvestmentType(); // ddlInvestmentType
                BindPIC();
                BindIndustrialPark();
                LoadData(id);              
                BindGridView(id);
            }
        }
        private void LoadData(int id)
        {
            // Open a connection to the database.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                connection.Open();
                // Load the data from the database.
                SqlCommand command = new SqlCommand("SELECT * FROM Investors_Profile WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                // Fill the form with the data.
                if (reader.Read())
                {
                    txtCompanyName.Text = reader["Company_Name"].ToString();
                    txtCompanyAddress.Text = reader["Company_Address"].ToString();
                    String strCountry = reader["Company_Country"].ToString();
                    ListItem ItemTerpilih4 = ddlCountry.Items.FindByValue(strCountry); // 3. ddlCountry
                    if (ItemTerpilih4 != null)
                    {
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ItemTerpilih4);
                    }
                    txtCompanyPhone.Text = reader["Company_Phone"].ToString();
                    txtCompanyContactPerson.Text = reader["Company_Contact_Person"].ToString();
                    txtCompanyContactEmail.Text = reader["Company_Contact_Email"].ToString();
                    String strPromotedSector = reader["Company_Industry_Type"].ToString(); // 11. ddlPromotedSector
                    ListItem ItemTerpilih = ddlPIC.Items.FindByValue(strPromotedSector);
                    if (ItemTerpilih != null)
                    {
                        ddlPIC.SelectedIndex = ddlPIC.Items.IndexOf(ItemTerpilih);
                    }
                    String strInvestmentType = reader["Investment_Type"].ToString(); // 12. ddlInvestmentType
                    ListItem ItemTerpilih2 = ddlInvestmentType.Items.FindByValue(strInvestmentType);
                    if (ItemTerpilih2 != null)
                    {
                        ddlInvestmentType.SelectedIndex = ddlInvestmentType.Items.IndexOf(ItemTerpilih2);
                    }
                    txtPSFRM.Text = reader["PSF_RM"].ToString();
                    txtCompanyDescription.Text = reader["Company_Description"].ToString();
                    txtLandSizeRequired.Text = reader["Land_Size_Required"].ToString();
                    txtHeadquaters.Text = reader["Headquaters"].ToString();
                    txtYearFounded.Text = reader["Year_Founded"].ToString();
                    txtWebsite.Text = reader["Website"].ToString();
                    txtBranches.Text = reader["Branches"].ToString();
                    txtBuildingSize.Text = reader["Building_Size"].ToString();
                    txtManPowerRequirement.Text = reader["Manpower_Request"].ToString();
                    txtProposedTotalInvestment.Text = reader["Proposed_Investment"].ToString();
                    txtElectricity.Text = reader["Electricity_Request"].ToString();
                    txtWater.Text = reader["Water_Request"].ToString();
                    txtNaturalGas.Text = reader["Natural_Gas_Request"].ToString();
                    txtProjectTimeline.Text = reader["Project_Timeline"].ToString();
                    string StartConstructionDate = reader["Start_Construction_Date"].ToString();
                    if (!string.IsNullOrEmpty(StartConstructionDate) || StartConstructionDate == "01/01/1900")
                    {
                        txtStartConstructionDate.Text = Convert.ToDateTime(reader["Start_Construction_Date"]).ToString("yyyy-MM-dd");
                    }
                    else
                    { txtStartConstructionDate.Text = ""; }
                    if (txtStartConstructionDate.Text == "01/01/1900") { txtStartConstructionDate.Text = ""; }
                    string StartOperationDate = reader["Start_Operation_Date"].ToString();
                    if (!string.IsNullOrEmpty(StartOperationDate))
                    {
                        txtStartOperationDate.Text = Convert.ToDateTime(reader["Start_Operation_Date"]).ToString("yyyy-MM-dd");
                    }
                    else
                    { txtStartOperationDate.Text = ""; }
                    if (txtStartOperationDate.Text == "01/01/1900") { txtStartOperationDate.Text = ""; }
                    string RFPDateStart = reader["RFP_Date_Start"].ToString();
                    if (!string.IsNullOrEmpty(RFPDateStart))
                    {
                        txtRFPDateStart.Text = Convert.ToDateTime(reader["RFP_Date_Start"]).ToString("yyyy-MM-dd");
                    }
                    else
                    { txtRFPDateStart.Text = ""; }
                    if (txtRFPDateStart.Text == "01/01/1900") { txtRFPDateStart.Text = ""; }
                    string RFPDateEnd = reader["RFP_Date_End"].ToString();
                    if (!string.IsNullOrEmpty(RFPDateEnd))
                    {
                        txtRFPDateEnd.Text = Convert.ToDateTime(reader["RFP_Date_End"]).ToString("yyyy-MM-dd");
                    }
                    else
                    { txtRFPDateEnd.Text = ""; }
                    if (txtRFPDateEnd.Text == "01/01/1900") { txtRFPDateEnd.Text = ""; }
                    string IsRFPstr = reader["Is_RFP"].ToString();
                    bool IsRFP2 = bool.Parse(IsRFPstr);
                    IsRFP.Checked = IsRFP2;
                    txtRFPDescription.Text = reader["RFP_Description"].ToString();
                    String strInvestorStatus = reader["Company_Status"].ToString(); // 26. ddlInvestorStatus
                    ListItem ItemTerpilih3 = ddlInvestorStatus.Items.FindByValue(strInvestorStatus);
                    if (ItemTerpilih2 != null)
                    {
                        ddlInvestorStatus.SelectedIndex = ddlInvestorStatus.Items.IndexOf(ItemTerpilih3);
                    }
                    String strDecisionStatus = reader["Decision"].ToString(); // ddl Decision modal
                    ListItem ItemTerpilih5 = ddlDecisionStatus.Items.FindByValue(strDecisionStatus);
                    if (ItemTerpilih5 != null)
                    {
                        ddlDecisionStatus.SelectedIndex = ddlDecisionStatus.Items.IndexOf (ItemTerpilih5);
                    }
                    txtDecisionDescription.Text = reader["Decision_Description"].ToString();
                    if (reader["Date_Inquiry"] != DBNull.Value)
                    {
                        txtDateInquiry.Text = Convert.ToDateTime(reader["Date_Inquiry"]).ToString("yyyy-MM-dd");
                    }
                    txtLeasePeriod.Text = reader["Lease_Period"].ToString();
                    hfLeadsStatus.Value = reader["Leads_Status"].ToString();
                    txtLeadStatus.Text = reader["Leads_Status"].ToString();
                    String strIndustrialPark = reader["industrial_Park_Name"].ToString();
                    ListItem ItemTerpilih7 = industrialPark_ddl.Items.FindByValue(strIndustrialPark);
                    if (ItemTerpilih7 != null)
                    {
                        industrialPark_ddl.SelectedIndex = industrialPark_ddl.Items.IndexOf(ItemTerpilih7);
                    }
                    else if (industrialPark_ddl.Items.Count > 0)
                    {
                        // Fallback to text matching if value doesn't match
                        ListItem itemMatch = industrialPark_ddl.Items.Cast<ListItem>().FirstOrDefault(i => i.Text == strIndustrialPark);
                        if (itemMatch != null) industrialPark_ddl.SelectedIndex = industrialPark_ddl.Items.IndexOf(itemMatch);
                    }
                    // Get the dropdown list control by its ID
                    DropDownList statusSelect = (DropDownList)FindControl("statusSelect");
                    // Loop through the dropdown list items
                    foreach (ListItem item in statusSelect.Items)
                    {
                        // Check if the current item's value matches the selected value from the database
                        if (item.Value == hfLeadsStatus.Value)
                        {
                            // Set the item as selected
                            item.Selected = true;
                            break; // Exit the loop once a match is found
                        }
                    }
                    String strPersonInCharge = reader["PIC"].ToString(); // 29. ddlPersonInCharge 457
                    ListItem ItemTerpilih6 = ddlPersonInCharge.Items.FindByValue(strPersonInCharge);
                    if (ItemTerpilih6 != null)
                    {
                        ddlPersonInCharge.SelectedIndex = ddlPersonInCharge.Items.IndexOf(ItemTerpilih6);
                    }
                    statusImage.ImageUrl = hfLeadsStatus.Value.ToString() + ".png";
                }
                reader.Close();
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int id;
            // Open a connection to the database.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                // Check Null atau empty TextBox Number
                if (string.IsNullOrEmpty(txtLandSizeRequired.Text)) { txtLandSizeRequired.Text = "0"; };
                if (string.IsNullOrEmpty(txtPSFRM.Text)) { txtPSFRM.Text = "0"; };
                if (string.IsNullOrEmpty(txtBuildingSize.Text)) { txtBuildingSize.Text = "0"; };
                if (string.IsNullOrEmpty(txtElectricity.Text)) { txtElectricity.Text = "0"; };
                if (string.IsNullOrEmpty(txtWater.Text)) { txtWater.Text = "0"; };
                if (string.IsNullOrEmpty(txtNaturalGas.Text)) { txtNaturalGas.Text = "0"; };
                if (string.IsNullOrEmpty(txtProposedTotalInvestment.Text)) { txtProposedTotalInvestment.Text = "0"; };
                if (string.IsNullOrEmpty(txtManPowerRequirement.Text)) { txtManPowerRequirement.Text = "0"; };
                DateTime Start_Construction_Date;
                DateTime Start_Operation_Date;
                DateTime RFP_Date_Start;
                DateTime RFP_Date_End;
                if (!string.IsNullOrEmpty(txtStartConstructionDate.Text))
                {
                    Start_Construction_Date = DateTime.Parse(txtStartConstructionDate.Text);
                }
                else
                {
                    Start_Construction_Date = DateTime.Parse("01/01/2023"); // Default Empty Date
                }
                if (!string.IsNullOrEmpty(txtStartOperationDate.Text))
                {
                    Start_Operation_Date = DateTime.Parse(txtStartOperationDate.Text);
                }
                else
                {
                    Start_Operation_Date = DateTime.Parse("01/01/2023"); // Default Empty Date
                }
                if (!string.IsNullOrEmpty(txtRFPDateStart.Text))
                {
                    RFP_Date_Start = DateTime.Parse(txtRFPDateStart.Text);
                }
                else
                {
                    RFP_Date_Start = DateTime.Parse("01/01/2023"); // Default Empty Date
                }
                if (!string.IsNullOrEmpty(txtRFPDateEnd.Text))
                {
                    RFP_Date_End = DateTime.Parse(txtRFPDateEnd.Text);
                }
                else
                {
                    RFP_Date_End = DateTime.Parse("01/01/2023"); // Default Empty Date
                }
                connection.Open();
                // Update the data in the database.
                string q1 = "UPDATE Investors_Profile SET Company_Name = @Company_Name, Company_Address = @Company_Address, ";
                string q2 = "Company_Postal = @Company_Postal, Company_Country = @Company_Country, Company_Phone = @Company_Phone, ";
                string q3 = "Company_Contact_Person = @Company_Contact_Person, Company_Contact_Email = @Company_Contact_Email,";
                string q4 = "Company_Industry_Type = @Company_Industry_Type, Investment_Type = @Investment_Type, PSF_RM = @PSF_RM, ";
                string q5 = "Company_Description = @Company_Description, Land_Size_Required = @Land_Size_Required, Headquaters = @Headquaters,";
                string q6 = "Year_Founded = @Year_Founded, Website = @Website, Branches = @Branches, ";
                string q7 = "Building_Size = @Building_Size, Manpower_Request = @Manpower_Request, Proposed_Investment = @Proposed_Investment,";
                string q8 = "Products = @Products, Electricity_Request = @Electricity_Request, Water_Request = @Water_Request, ";
                string q9 = "Natural_Gas_Request = @Natural_Gas_Request, Project_Timeline = @Project_Timeline, Company_Status = @Company_Status, ";
                string q10 = "Start_Construction_Date = @Start_Construction_Date, Start_Operation_Date = @Start_Operation_Date, ";
                string q11 = "Is_RFP = @Is_RFP, RFP_Date_Start = @RFP_Date_Start, RFP_Date_End = @RFP_Date_End, RFP_Description = @RFP_Description, ";
                string q12 = "Days_Till_Expire = @Days_Till_Expire, Decision = @Decision, Decision_Description = @Decision_Description, ";
                string q13 = "Updatedby = @UpdatedBy, Ip_Address = @Ip_Address, Leads_Status = @Leads_Status,PIC = @PIC, UpdatedDate = GETDATE(), ";
                string q13a = "Date_Inquiry = @DateInquiry, Lease_Period = @LeasePeriod, industrial_Park_Name = @IndustrialPark ";
                string q14 = "WHERE Id = @Id";
                string query = q1 + q2 + q3 + q4 + q5 + q6 + q7 + q8 + q9 + q10 + q11 + q12 + q13 + q13a + q14;
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", hdnID.Value);
                command.Parameters.AddWithValue("@Company_Name", txtCompanyName.Text);
                command.Parameters.AddWithValue("@Company_Address", txtCompanyAddress.Text);
                command.Parameters.AddWithValue("@Company_Postal", ""); // txtCompanyPostal.Text
                command.Parameters.AddWithValue("@Company_Country", ddlCountry.Text.ToString()); // 3.
                command.Parameters.AddWithValue("@Company_Phone", txtCompanyPhone.Text);
                command.Parameters.AddWithValue("@Company_Contact_Person", txtCompanyContactPerson.Text);
                command.Parameters.AddWithValue("@Company_Contact_Email", txtCompanyContactEmail.Text);
                command.Parameters.AddWithValue("@Company_Industry_Type", ddlPIC.Text.ToString()); // 11
                command.Parameters.AddWithValue("@Investment_Type", ddlInvestmentType.Text.ToString()); // 12
                command.Parameters.AddWithValue("@PSF_RM", txtPSFRM.Text);
                command.Parameters.AddWithValue("@Company_Description", txtCompanyDescription.Text);
                command.Parameters.AddWithValue("@Land_Size_Required", txtLandSizeRequired.Text);
                command.Parameters.AddWithValue("@Headquaters", txtHeadquaters.Text);
                command.Parameters.AddWithValue("@Year_Founded", txtYearFounded.Text);
                command.Parameters.AddWithValue("@Website", txtWebsite.Text);
                command.Parameters.AddWithValue("@Branches", txtBranches.Text);
                command.Parameters.AddWithValue("@Building_Size", txtBuildingSize.Text);
                command.Parameters.AddWithValue("@Manpower_Request", txtManPowerRequirement.Text);
                command.Parameters.AddWithValue("@Proposed_Investment", txtProposedTotalInvestment.Text);
                command.Parameters.AddWithValue("@Products", ""); // Dah Buang
                command.Parameters.AddWithValue("@Electricity_Request", txtElectricity.Text);
                command.Parameters.AddWithValue("@Water_Request", txtWater.Text);
                command.Parameters.AddWithValue("@Natural_Gas_Request", txtNaturalGas.Text);
                command.Parameters.AddWithValue("@Industrial_Gas_Request", txtIndustrialGas.Text);
                command.Parameters.AddWithValue("@Project_Timeline", txtProjectTimeline.Text);
                command.Parameters.AddWithValue("@Start_Construction_Date", Start_Construction_Date);
                command.Parameters.AddWithValue("@Start_Operation_Date", Start_Operation_Date);
                command.Parameters.AddWithValue("@Company_Status", ddlInvestorStatus.Text.ToString()); // 26
                command.Parameters.AddWithValue("@Is_RFP", IsRFP.Checked);
                command.Parameters.AddWithValue("@RFP_Date_Start", txtRFPDateStart.Text);
                command.Parameters.AddWithValue("@RFP_Date_End", txtRFPDateEnd.Text);
                command.Parameters.AddWithValue("@RFP_Description", txtRFPDescription.Text.ToString());
                command.Parameters.AddWithValue("@Days_Till_Expire", 0);
                command.Parameters.AddWithValue("@Decision", ddlDecisionStatus.Text.ToString()); // 527
                command.Parameters.AddWithValue("@Decision_Description", txtDecisionDescription.Text);
                command.Parameters.AddWithValue("@UpdatedBy", Session["Username"].ToString());
                command.Parameters.AddWithValue("@Ip_Address", Session["Ip_Address"].ToString());
                command.Parameters.AddWithValue("@Leads_Status", hfLeadsStatus.Value.ToString());
                command.Parameters.AddWithValue("@PIC", ddlPersonInCharge.Text.ToString()); // 458
                command.Parameters.AddWithValue("@IndustrialPark", (industrialPark_ddl != null && industrialPark_ddl.SelectedItem != null) ? industrialPark_ddl.SelectedItem.Text : "");
                command.Parameters.AddWithValue("@DateInquiry", (txtDateInquiry != null && !string.IsNullOrEmpty(txtDateInquiry.Text)) ? (object)txtDateInquiry.Text : (object)DBNull.Value);
                command.Parameters.AddWithValue("@LeasePeriod", (txtLeasePeriod != null && int.TryParse(txtLeasePeriod.Text, out int lp2)) ? lp2 : 0);

                id = Convert.ToInt32(command.ExecuteNonQuery()); //command.ExecuteNonQuery();
                if (id > 0)
                {                    
                    lblMessage.Text = "Data updated successfully";
                }
                // Redirect the user back to the main page.
                //Response.Redirect("Home.aspx");
            }
            Response.Redirect("List_Investors_Profile.aspx");
        }
        private void BindCountry()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID, CountryName FROM Country"))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.Connection = con;
                    con.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    ddlCountry.DataSource = dt;
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataValueField = "CountryName";
                    ddlCountry.DataBind();
                    con.Close();
                }
            }
        }
        private void BindPromotedSector()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Promoted_Sector FROM Sector ORDER BY Promoted_Sector ASC"))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.Connection = con;
                    con.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    // Capitalize the first letter of each word in Promoted_Sector column
                    CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Promoted_Sector"] = textInfo.ToTitleCase(row["Promoted_Sector"].ToString().ToLower());
                    }
                    ddlPIC.DataSource = dt;
                    ddlPIC.DataTextField = "Promoted_Sector";
                    // ddlPromotedSector.DataValueField = "Id";
                    ddlPIC.DataValueField = "Promoted_Sector";
                    ddlPIC.DataBind();
                    con.Close();
                }
            }
            ddlPIC.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindPIC()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, PIC FROM PIC ORDER BY PIC ASC"))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    cmd.Connection = con;
                    con.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    // Capitalize the first letter of each word in ddlPersonInCharge column
                    CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    foreach (DataRow row in dt.Rows)
                    {
                        row["PIC"] = textInfo.ToTitleCase(row["PIC"].ToString().ToLower());
                    }
                    ddlPersonInCharge.DataSource = dt;
                    ddlPersonInCharge.DataTextField = "PIC";
                    ddlPersonInCharge.DataValueField = "PIC";
                    ddlPersonInCharge.DataBind();
                    con.Close();
                }
            }
            ddlPersonInCharge.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindIndustrialPark()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Industrial_Park_Name FROM Industrial_Park ORDER BY Industrial_Park_Name ASC", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        industrialPark_ddl.DataSource = dt;
                        industrialPark_ddl.DataTextField = "Industrial_Park_Name";
                        industrialPark_ddl.DataValueField = "Industrial_Park_Name";
                        industrialPark_ddl.DataBind();
                        con.Close();
                    }
                }
                industrialPark_ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch { }
        }
        private void BindInvestmentType()
        {
            List<string> InvestmentTypeList = new List<string>
            {
                "Greenfield",
                "Brownfield"
            };
            ddlInvestmentType.DataSource = InvestmentTypeList;
            ddlInvestmentType.DataBind();
            //ddlInvestmentType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        // Replaced with using javascript
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List_Investors_Profile.aspx");
        }
        private List<PhotoInfo> GetPhotosFromDatabase(int ContactId)
        {
            List<PhotoInfo> photos = new List<PhotoInfo>();
            // Connect to the database and execute SQL query
            string connectionString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Document_Id, FileData, ContentType FROM Photo_IProfile WHERE IProfile_Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", ContactId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhotoInfo photo = new PhotoInfo
                            {
                                Photo_Id = (int)reader["Document_Id"],
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
                    if (!(fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".gif" || fileExtension == ".png" || fileExtension == ".docx" || fileExtension == ".doc" || fileExtension == ".xlsx" || fileExtension == ".xls" || fileExtension == ".pptx" || fileExtension == ".ppt" || fileExtension == ".pdf"))
                    {
                        lblMessage.Text = "Invalid file type. Please upload a valid photo type (jpg, jpeg, gif, png, doc, docx, xls, xlsx, ppt, pptx).";
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
    }
}
