using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class New_Investors_Profile_V2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try {
                if (Session["Username"] != null)
                {
                    lblUsername.Text = Convert.ToString(Session["Username"]);
                }
                else
                {
                    // Response.Redirect("Default.aspx"); // Temporarily disabled for easier testing
                }
                
                BindCountry();
                BindPromotedSector();
                BindInvestmentType();
                BindInvestorStatus();
                BindIndustrialPark();

                // Set Default Leads Status
                if (ddlLeadsStatus != null)
                {
                    ListItem ItemStatus = ddlLeadsStatus.Items.FindByValue("Cold");
                    if (ItemStatus != null)
                    {
                        ddlLeadsStatus.SelectedIndex = ddlLeadsStatus.Items.IndexOf(ItemStatus);
                    }
                }

                String Default_Construction_Date = DateTime.Now.ToString("yyyy-MM-dd");
                txtStartConstructionDate.Text = Default_Construction_Date;
                txtStartOperationDate.Text = Default_Construction_Date;
                txtDateInquiry.Text = Default_Construction_Date;
                } catch (Exception ex) {
                    lblMessage.Text = "Load Error: " + ex.Message;
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Investors_Profile(
                        Company_Code, Company_Name, Company_Address, Company_Postal, Company_Country, Company_Phone,
                        Website, Year_Founded, Headquaters, Branches, 
                        Company_Contact_Person, Company_Contact_Email, Company_Industry_Type, Investment_Type, Land_Size_Required, PSF_RM, 
                        Building_Size, Electricity_Request, Water_Request, Natural_Gas_Request, Industrial_Gas_Request, Proposed_Investment, Manpower_Request,
                        Project_Timeline, Start_Construction_Date, Start_Operation_Date, Company_Description, Company_Status, 
                        Is_RFP, RFP_Date_Start, RFP_Date_End, RFP_Description, Days_Till_Expire, 
                        Decision, Decision_Description, CreatedBy, Ip_Address, Leads_Status, industrial_Park_Name,
                        Date_Inquiry, Lease_Period, Investors_Decision_Making, Investors_Internal_Status
                    ) VALUES (
                        @CompanyCode, @CompanyName, @CompanyAddress, @CompanyPostal, @CompanyCountry, @CompanyPhone,
                        @Website, @YearFounded, @Headquaters, @Branches, 
                        @CompanyContactPerson, @CompanyContactEmail, @CompanyIndustryType, @InvestmentType, @LandSizeRequired, @PSFRM,
                        @BuildingSize, @Electricity, @Water, @NaturalGas, @IndustrialGas, @ProposedTotalInvestment, @ManPowerRequirement, 
                        @ProjectTimeline, @StartConstructionDate, @StartOperationDate, @CompanyDescription, @CompanyStatus, 
                        @Is_RFP, @RFP_Date_Start, @RFP_Date_End, @RFP_Description, @Days_Till_Expire, 
                        @Decision, @Decision_Description, @CreatedBy, @Ip_Address, @Leads_Status, @IndustrialPark,
                        @DateInquiry, @LeasePeriod, @InvestorsDecisionMaking, @InvestorsInternalStatus
                    ); SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@CompanyCode", (txtCompanyName != null && txtCompanyName.Text.Length > 20) ? txtCompanyName.Text.Substring(0, 20) : (txtCompanyName != null ? txtCompanyName.Text : ""));
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName != null ? txtCompanyName.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyAddress", txtCompanyAddress != null ? txtCompanyAddress.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyPostal", "");
                    cmd.Parameters.AddWithValue("@CompanyCountry", (ddlCountry != null && ddlCountry.SelectedItem != null) ? ddlCountry.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyPhone", txtCompanyPhone != null ? txtCompanyPhone.Text : "");
                    cmd.Parameters.AddWithValue("@Website", txtWebsite != null ? txtWebsite.Text : "");
                    cmd.Parameters.AddWithValue("@YearFounded", txtYearFounded != null ? txtYearFounded.Text : "");
                    cmd.Parameters.AddWithValue("@Headquaters", txtHeadquaters != null ? txtHeadquaters.Text : "");
                    cmd.Parameters.AddWithValue("@Branches", txtBranches != null ? txtBranches.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyContactPerson", txtCompanyContactPerson != null ? txtCompanyContactPerson.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyContactEmail", txtCompanyContactEmail != null ? txtCompanyContactEmail.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyIndustryType", (ddlPromotedSector != null && ddlPromotedSector.SelectedItem != null) ? ddlPromotedSector.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@InvestmentType", (ddlInvestmentType != null && ddlInvestmentType.SelectedItem != null) ? ddlInvestmentType.SelectedItem.Text : "");
                    
                    decimal val;
                    cmd.Parameters.AddWithValue("@LandSizeRequired", (txtLandSizeRequired != null && decimal.TryParse(txtLandSizeRequired.Text, out val)) ? val : 0);
                    cmd.Parameters.AddWithValue("@PSFRM", (txtPSFRM != null && decimal.TryParse(txtPSFRM.Text, out val)) ? val : 0);
                    cmd.Parameters.AddWithValue("@BuildingSize", (txtBuildingSize != null && decimal.TryParse(txtBuildingSize.Text, out val)) ? val : 0);
                    cmd.Parameters.AddWithValue("@Electricity", txtElectricity != null ? txtElectricity.Text : "");
                    cmd.Parameters.AddWithValue("@Water", txtWater != null ? txtWater.Text : "");
                    cmd.Parameters.AddWithValue("@NaturalGas", txtNaturalGas != null ? txtNaturalGas.Text : "");
                    cmd.Parameters.AddWithValue("@IndustrialGas", txtIndustrialGas != null ? txtIndustrialGas.Text : "");
                    cmd.Parameters.AddWithValue("@ProposedTotalInvestment", txtProposedTotalInvestment != null ? txtProposedTotalInvestment.Text : "");
                    cmd.Parameters.AddWithValue("@ManPowerRequirement", (txtManPowerRequirement != null && decimal.TryParse(txtManPowerRequirement.Text, out val)) ? val : 0);

                    cmd.Parameters.AddWithValue("@ProjectTimeline", txtProjectTimeline != null ? txtProjectTimeline.Text : "");
                    cmd.Parameters.AddWithValue("@StartConstructionDate", (txtStartConstructionDate != null && !string.IsNullOrEmpty(txtStartConstructionDate.Text)) ? (object)txtStartConstructionDate.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartOperationDate", (txtStartOperationDate != null && !string.IsNullOrEmpty(txtStartOperationDate.Text)) ? (object)txtStartOperationDate.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CompanyDescription", txtCompanyDescription != null ? txtCompanyDescription.Text : "");
                    cmd.Parameters.AddWithValue("@CompanyStatus", ddlInvestorStatus != null ? ddlInvestorStatus.SelectedValue : "");
                    
                    cmd.Parameters.AddWithValue("@Is_RFP", IsRFP != null ? IsRFP.Checked : false);
                    cmd.Parameters.AddWithValue("@RFP_Date_Start", (txtRFPDateStart != null && !string.IsNullOrEmpty(txtRFPDateStart.Text)) ? (object)txtRFPDateStart.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RFP_Date_End", (txtRFPDateEnd != null && !string.IsNullOrEmpty(txtRFPDateEnd.Text)) ? (object)txtRFPDateEnd.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RFP_Description", txtRFPDescription != null ? txtRFPDescription.Text : "");
                    cmd.Parameters.AddWithValue("@Days_Till_Expire", 0);
                    
                    cmd.Parameters.AddWithValue("@Decision", ddlDecisionStatus != null ? ddlDecisionStatus.SelectedValue : "");
                    cmd.Parameters.AddWithValue("@Decision_Description", txtDecisionDescription != null ? txtDecisionDescription.Text : "");
                    
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["Username"] != null ? Session["Username"].ToString() : "System");
                    cmd.Parameters.AddWithValue("@Ip_Address", HttpContext.Current.Request.UserHostAddress);
                    cmd.Parameters.AddWithValue("@Leads_Status", ddlLeadsStatus != null ? ddlLeadsStatus.SelectedValue : "");
                    cmd.Parameters.AddWithValue("@IndustrialPark", industrialPark_ddl != null && industrialPark_ddl.SelectedItem != null ? industrialPark_ddl.SelectedItem.Text : "");
                    cmd.Parameters.AddWithValue("@DateInquiry", (txtDateInquiry != null && !string.IsNullOrEmpty(txtDateInquiry.Text)) ? (object)txtDateInquiry.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LeasePeriod", (txtLeasePeriod != null && int.TryParse(txtLeasePeriod.Text, out int lp)) ? lp : 0);
                    cmd.Parameters.AddWithValue("@InvestorsDecisionMaking", txtInvestorsDecisionMaking != null ? txtInvestorsDecisionMaking.Text : "");
                    cmd.Parameters.AddWithValue("@InvestorsInternalStatus", txtInvestorsInternalStatus != null ? txtInvestorsInternalStatus.Text : "");
                    
                    object newIdObj = cmd.ExecuteScalar();
                    if (newIdObj != null)
                    {
                        int newId = Convert.ToInt32(newIdObj);
                        string quickQuery = "UPDATE Investors_Profile SET Investors_Decision_Making = @val1, Investors_Internal_Status = @val2 WHERE Id = @Id";
                        using (SqlCommand quickCmd = new SqlCommand(quickQuery, con))
                        {
                            quickCmd.Parameters.AddWithValue("@val1", txtInvestorsDecisionMaking != null ? txtInvestorsDecisionMaking.Text : "");
                            quickCmd.Parameters.AddWithValue("@val2", txtInvestorsInternalStatus != null ? txtInvestorsInternalStatus.Text : "");
                            quickCmd.Parameters.AddWithValue("@Id", newId);
                            quickCmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
                Response.Redirect("List_Investors_Profile.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Save Error: " + ex.Message;
            }
        }

        private void BindCountry()
        {
            try {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT ID, CountryName FROM Country ORDER BY CountryName", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        ddlCountry.DataSource = dt;
                        ddlCountry.DataTextField = "CountryName";
                        ddlCountry.DataValueField = "CountryName";
                        ddlCountry.DataBind();
                        con.Close();
                    }
                }
                ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
            } catch {}
        }

        private void BindPromotedSector()
        {
            try {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Id, Promoted_Sector FROM Sector ORDER BY Promoted_Sector ASC", con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                        TextInfo textInfo = cultureInfo.TextInfo;
                        foreach (DataRow row in dt.Rows)
                        {
                            row["Promoted_Sector"] = textInfo.ToTitleCase(row["Promoted_Sector"].ToString().ToLower());
                        }
                        ddlPromotedSector.DataSource = dt;
                        ddlPromotedSector.DataTextField = "Promoted_Sector";
                        ddlPromotedSector.DataValueField = "Id";
                        ddlPromotedSector.DataBind();
                        con.Close();
                    }
                }
                ddlPromotedSector.Items.Insert(0, new ListItem("--Select--", "0"));
            } catch {}
        }

        private void BindInvestmentType()
        {
            try {
                List<string> InvestmentTypeList = new List<string> { "Greenfield", "Brownfield" };
                ddlInvestmentType.DataSource = InvestmentTypeList;
                ddlInvestmentType.DataBind();
                ddlInvestmentType.Items.Insert(0, new ListItem("--Select--", "0"));
            } catch {}
        }

        private void BindInvestorStatus()
        {
            try {
                List<ListItem> InvestorStatusTypeList = new List<ListItem>
                {
                    new ListItem("1. Preliminary Enquiry", "Preliminary Enquiry"),
                    new ListItem("2. Due Dilligence", "Due Dillingence"),
                    new ListItem("3. RFI", "RFI"),
                    new ListItem("4. Site Visit", "Site Visit"),
                    new ListItem("5. RFP", "RFP"),
                    new ListItem("6. Decision", "Decision"),
                    new ListItem("7. Letter Of Intent", "Letter Of Intent"),
                    new ListItem("8. Lease Agreement", "Lease Agreement"),
                    new ListItem("9. Collection of First Payment", "Collection of First Payment")
                };
                ddlInvestorStatus.DataSource = InvestorStatusTypeList;
                ddlInvestorStatus.DataBind();
                ddlInvestorStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            } catch {}
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

        protected void BtnCancel_Click(object sender, EventArgs e) { Response.Redirect("List_Investors_Profile.aspx"); }
        protected void BtnSaveDecisionDetails_Click(object sender, EventArgs e) {}
        protected void BtnSaveRFPDetails_Click(object sender, EventArgs e) {}
    }
}