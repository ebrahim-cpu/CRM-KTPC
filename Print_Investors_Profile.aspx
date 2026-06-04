<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Investors_Profile.aspx.cs" Inherits="CRM.Print_Investors_Profile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Investors Profile and Activities</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        .grey-row {
            background-color: lightgray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-sm-12">
            <table cellpadding="5">
                <tr>
                    <td colspan="3">
                        <h3><b>INVESTOR PROFILE</b></h3>
                    </td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>A. Investors Profile</b>
                    </td>
                </tr>
                <tr>
                    <td>1. </td>
                    <td><b>&nbsp;Company Name:</b></td>
                    <td>
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>2. </td>
                    <td><b>&nbsp;Address:</b></td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td>3. </td>
                    <td><b>&nbsp;Country:</b></td>
                    <td>
                        <asp:Label ID="lblCountry" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>4. </td>
                    <td><b>&nbsp;Phone:</b></td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>5. </td>
                    <td><b>&nbsp;Website URL:</b></td>
                    <td>
                        <asp:Label ID="lblWebsiteURL" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>6. </td>
                    <td><b>&nbsp;Year Founded:</b></td>
                    <td>
                        <asp:Label ID="lblYearFounded" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>7. </td>
                    <td><b>&nbsp;Headquaters:</b></td>
                    <td>
                        <asp:Label ID="lblHeadquaters" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>8. </td>
                    <td><b>&nbsp;Branches:</b></td>
                    <td>
                        <asp:Label ID="lblBranches" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label1" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>B. Contact Person</b></td>
                </tr>

                <tr>
                    <td>9. </td>
                    <td><b>&nbsp;Contact Person:</b></td>
                    <td>
                        <asp:Label ID="lblContactPerson" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>10. </td>
                    <td><b>&nbsp;Contact Email:</b></td>
                    <td>
                        <asp:Label ID="lblContactEmail" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label2" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>C. Industry and Investment Type</b></td>
                </tr>

                <tr>
                    <td>11. </td>
                    <td><b>&nbsp;Promoted Sector:</b></td>
                    <td>
                        <asp:Label ID="lblIndustryType" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>12. </td>
                    <td><b>&nbsp;Investment Type:</b></td>
                    <td>
                        <asp:Label ID="lblInvestmentType" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label3" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>D. Land and Building Requirement</b></td>
                </tr>

                <tr>
                    <td>13. </td>
                    <td><b>&nbsp;Land Size Required (sf):</b></td>
                    <td>
                        <asp:Label ID="lblLandSizeRequired" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>14. </td>
                    <td><b>&nbsp;PSF (RM):</b></td>
                    <td>
                        <asp:Label ID="lblPSFRM" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>15. </td>
                    <td><b>&nbsp;Building Size (sf):</b></td>
                    <td>
                        <asp:Label ID="lblBuildingSize" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label4" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>E. Utility Requirement</b></td>
                </tr>

                <tr>
                    <td>16. </td>
                    <td><b>&nbsp;Electricity (kWh):</b></td>
                    <td>
                        <asp:Label ID="lblElectricity" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>17. </td>
                    <td><b>&nbsp;Water (m3):</b></td>
                    <td>
                        <asp:Label ID="lblWater" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>18. </td>
                    <td><b>&nbsp;Natural Gas (Nm3/hr):</b></td>
                    <td>
                        <asp:Label ID="lblNaturalGas" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>19. </td>
                    <td><b>&nbsp;Industrial Gas (Nm3/hr):</b></td>
                    <td>
                        <asp:Label ID="lblIndustrialGas" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label5" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>F. Total Investment, Man Power, Timeline</b></td>
                </tr>

                <tr>
                    <td>20. </td>
                    <td><b>&nbsp;Proposed Total Investment (RM):</b></td>
                    <td>
                        <asp:Label ID="lblProposedTotalInvestment" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>21. </td>
                    <td><b>&nbsp;Manpower Requirement:</b></td>
                    <td>
                        <asp:Label ID="lblManpowerRequirement" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>22. </td>
                    <td><b>&nbsp;Project Timeline:</b></td>
                    <td>
                        <asp:Label ID="lblProjectTimeline" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>23. </td>
                    <td><b>&nbsp;Start Construction Date:</b></td>
                    <td>
                        <asp:Label ID="lblStartConstruction" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>24. </td>
                    <td><b>&nbsp;Project Operation Date:</b></td>
                    <td>
                        <asp:Label ID="lblStartOperation" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>25. </td>
                    <td><b>&nbsp;Company Description:</b></td>
                    <td>
                        <asp:Label ID="lblCompanyDescription" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label6" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>G. Investors Status</b></td>
                </tr>

                <tr>
                    <td>26. </td>
                    <td><b>&nbsp;Investors Status:</b></td>
                    <td>
                        <asp:Label ID="lblInvestorsStatus" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>27. </td>
                    <td><b>&nbsp;Decision Making:</b></td>
                    <td>
                        <asp:Label ID="lblDecisionMaking" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>28. </td>
                    <td><b>&nbsp;Internal Status:</b></td>
                    <td>
                        <asp:Label ID="lblInternalStatus" runat="server"></asp:Label></td>
                </tr>
            </table>
            <div class="col-sm-11">
                <h3><b>INVESTOR CONTACTS</b></h3>
                <asp:GridView ID="gvContacts" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>                        
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Position" HeaderText="Position" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" />
                        <%--<asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" BorderStyle="None" />
                </asp:GridView>
            </div>
            <div class="col-sm-11">
                <h3><b>INVESTOR ACTIVITIES</b></h3>
                <asp:GridView ID="gvActivities" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                        <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                        <asp:BoundField DataField="Activity_Date_Start" HeaderText="Date" DataFormatString="{0: dd/MMM/yy}" SortExpression="Activity_Date_Start" />
                        <asp:BoundField DataField="Company_Status" HeaderText="Status" SortExpression="Company_Status" />
                        <asp:BoundField DataField="Activity_Description" HeaderText="Description" />
                        <%--<asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" BorderStyle="None" />
                </asp:GridView>
            </div>
        </div>
    </form>
    <div class="col-sm-12">
        This is a computer generated print out. This document printed on:
        <asp:Label ID="lblTime" runat="server"></asp:Label><br />
        <br />
        Please contact Sales Department for further inquiries.
    </div>
    <br />
    <br />
    <br />
    <hr />
</body>
</html>
