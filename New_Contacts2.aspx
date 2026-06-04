<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Contacts2.aspx.cs" Inherits="CRM.New_Contacts2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Contact</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <%--START NAV--%>
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">CRM v1.0</a>
                </div>
                <ul class="nav navbar-nav">
                    
                            <li><a href="Dashboard.aspx">Dashboard</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">2. Investor List</a></li>
                            <li><a href="List_Contacts.aspx">3. Contact List</a></li>
                            <%--                            <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>--%>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. Add New Activity</a></li>
                            <li><a href="List_Investors_Activities.aspx">2. Investor's Activity List</a></li>
                            <li><a href="List_RFP.aspx">3. RFP</a></li>
                        </ul>
                    </li>
                </ul>
                <div class="navbar-form navbar-left" runat="server">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" class="form-control" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="lnkSearch" class="btn btn-default" runat="server"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
                        </div>
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUsername" runat="server" ForeColor="#FFFFFF"></asp:Label>
                    &nbsp;<i class="glyphicon glyphicon-user" style="color: #FFFFFF"></i>
                </div>
                <ul class="nav navbar-nav">
                    <li><a href="Logout.aspx">Logout</a></li>
                </ul>
            </div>
        </nav>
        <%--END NAV--%>
        <div class="container">
            <h2>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/istockphoto-1152254942-612x612.jpg" Width="50" />&nbsp;Add New Contact</h2>
            <asp:TextBox ID="txtCompanyId" runat="server" Visible="False"></asp:TextBox>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label for="ddlCompanyId" runat="server" Text="Select Company"></asp:Label>
                    <asp:DropDownList ID="ddlCompanyId" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlCompanyId_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label for="industrialPark_ddl" runat="server" Text="0. Industrial Park"></asp:Label>
                    <asp:DropDownList ID="industrialPark_ddl" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label for="txtCompanyName" runat="server" Text="1. Company Name *"></asp:Label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name - Must fill in." MaxLength="200"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="txtCompanyPhone" runat="server" Text="2. Company Phone"></asp:Label>
                    <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control" placeholder="Company Name - Must fill in." MaxLength="200"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="txtName" runat="server" Text="3. Contact Person Name"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name Of Contact." MaxLength="100"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="txtPosition" runat="server" Text="4. Position"></asp:Label>
                    <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" placeholder="Position Of Contact" MaxLength="100"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="txtMobile" runat="server" Text="5. Mobile"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="" MaxLength="30"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="txtNotes" runat="server" Text="6. Notes"></asp:Label>
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="" MaxLength="300"></asp:TextBox>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
            </div>
            </div>
    </form>
    <hr />
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 4 August 2023</p>
    </div>
</body>
</html>

