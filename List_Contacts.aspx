<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_Contacts.aspx.cs" Inherits="CRM.List_Contacts" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>List Of Contacts</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        /* Solid border */
        hr.solid {
            border-top: 3px solid #bbb;
        }
        /* Rounded border */
        hr.rounded {
            border-top: 5px solid #bbb;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--START NAV--%>
            <nav class="navbar navbar-inverse">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="Home.aspx">CRM v1.0</a>
                    </div>
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="Home.aspx">Home</a></li>
                        <li class="dropdown">
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                                <li><a href="List_Investors_Profile.aspx">2. Investor List</a></li>
                                <li><a href="List_Contacts.aspx">3. Contact List</a></li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="New_Investors_Activities.aspx">1. New Activity</a></li>
                                <li><a href="List_Investors_Activities.aspx">2. Activity List</a></li>
                                <li><a href="List_RFP.aspx">3. RFP</a></li>
                            </ul>
                        </li>
                    </ul>
                    <div class="navbar-form navbar-left" runat="server">
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="lnkSearch" class="btn btn-default" runat="server" OnClick="LnkSearch_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
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
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <label>Filter by Company Name:</label>
                    <asp:DropDownList ID="ddlCompanyFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlCompanyFilter_SelectedIndexChanged">
                        <asp:ListItem Text="All Companies" Value=""></asp:ListItem>
                    </asp:DropDownList><br />
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-6" align="left">
                        <h2><asp:Image ID="Image1" runat="server" ImageUrl="~/images/phone_552489.png" Height="50px" />&nbsp;Contacts</h2>
                    </div>
                    <div class="col-sm-6" align="right">
                        <a href="New_Contacts.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="New Contact"><span class="glyphicon glyphicon-plus"></span></a>
                        <a href="Home.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Home"><span class="glyphicon glyphicon-home"></span></a>
                        <asp:Button ID="btnExport" runat="server" class="btn btn-primary" Text="Export to Excel" OnClick="BtnExport_Click" />
                    </div>
                </div>

                <div>
                    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-condensed" CellPadding="4" ForeColor="#000000" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Company_Id" HeaderText="Company Id" Visible="false" />
                            <asp:BoundField DataField="Company_Name" HeaderText="Company Name" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Position" HeaderText="Position" />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditContact" CommandArgument='<%# Eval("Id") %>' OnClick="BtnEdit_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteContact" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this contact?');" OnClick="BtnDelete_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <hr />
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 4 August 2023</p>
    </div>
</body>
</html>

