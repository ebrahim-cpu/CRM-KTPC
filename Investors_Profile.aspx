<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Investors_Profile.aspx.cs" Inherits="CRM.Investors_Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" SelectCommand="SELECT [Company_Code], [Company_Name], [Company_Address_1], [Company_Address_2], [Company_Postal], [Company_Country], [Company_Phone], [Company_Contact_Person], [Company_Contact_Email], [Company_Industry_Type], [Investment_Type], [Land_Size_Required], [PSF_RM], [Company_Description] FROM [Investors_Profile]"></asp:SqlDataSource>
    </form>
</body>
</html>
