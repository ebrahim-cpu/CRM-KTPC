<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_RFP.aspx.cs" Inherits="CRM.List_RFP2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>List Of Active Request For Proposal</title>
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
                    <a class="navbar-brand" href="Dashboard.aspx">CRM v1.0</a>
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
                            <%-- <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>  --%>
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
                            <asp:LinkButton ID="lnkSearch" class="btn btn-default" runat="server" OnClick="lnkSearch_Click"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>
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
        <div class="col-sm-6" align="left">
            <h2>RFP - Monitoring List</h2>
        </div>
        <div class="col-sm-6" align="right">
            <a href="Dashboard.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Home"><span class="glyphicon glyphicon-home"></span></a>
            <asp:Button ID="btnExport" runat="server" class="btn btn-primary" Text="Export to Excel" OnClick="btnExport_Click" />
            <asp:Button ID="btnUpdateRFP" runat="server" class="btn btn-primary" Text="Update RFP List" OnClick="btnUpdateRFP_Click" />
            <%--<a href="New_Investors_Profile.aspx" class="btn btn-primary">Add New Investors</a>
                    <a href="New_Investors_Activities.aspx" class="btn btn-primary">Add New Activities</a>--%>
        </div>
        <div class="col-sm-12">
            <asp:GridView ID="gvActivities" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvActivities_RowDataBound" CssClass="table table-hover" GridLines="None" CellPadding="4" AllowSorting="True" OnSorting="gvActivities_Sorting" DataKeyNames="Id" OnRowDeleting="gvActivities_RowDeleting" OnRowEditing="gvActivities_RowEditing">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                    <asp:BoundField DataField="Days_Till_Expire" HeaderText="Days Till Dateline" SortExpression="Days_Till_Expire" />
                    <asp:BoundField DataField="RFP_Date_Start" HeaderText="RFP Date" DataFormatString="{0:dd/MMM/yyyy}" SortExpression="RFP_Date_Start" />
                    <asp:BoundField DataField="RFP_Date_End" HeaderText="RFP Expires" DataFormatString="{0:dd/MMM/yyyy}" SortExpression="RFP_Date_End" />
                    <asp:TemplateField ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" data-toggle="tooltip" title="Edit" CssClass="btn btn-primary btn-xs" CommandName="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" data-toggle="tooltip" title="Delete" CssClass="btn btn-danger btn-xs" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                            <asp:LinkButton ID="btnFiles" runat="server" data-toggle="tooltip" title="Files" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("Id") %>' OnClick="btnFiles_Click"><span class="glyphicon glyphicon-paperclip"></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" BorderStyle="None" />
            </asp:GridView>
        </div>
    </form>
    <div class="col-sm-12">
        
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br />
        Please click <b>"Update RFP List" </b>to refresh the Days Till Dateline column. Don't forget to switch OFF the RFP  if the task is done.
    </div>
        
    <div class="col-sm-12">
        Legend
        <table>
            <tr><td style="background-color: red;">Red</td><td>:</td><td>Less than 10 Days till Dateline.</td></tr>
            <tr><td style="background-color: yellow;">Yellow</td><td>:</td><td>Less than 20 Days till Dateline.</td></tr>
        </table><hr />
    </div>
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 18 April 2023</p>
    </div>
</body>
</html>
