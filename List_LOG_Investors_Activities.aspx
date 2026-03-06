<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_LOG_Investors_Activities.aspx.cs" Inherits="CRM.List_LOG_Investors_Activities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AUDIT TRAIL - Investors Activities</title>
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
                    <li class="active"><a href="Home.aspx">Home</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Profile.aspx">New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">Investor List</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">New Investor's Activities</a></li>
                            <li><a href="List_Investors_Activities.aspx">Investor's Activity List</a></li>
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
                </div>
            </div>
        </nav>
        <%--END NAV--%>
        <div class="container-fluid">
            <h1>Audit Trail: Investor's Activities</h1>
            <asp:GridView ID="GridView1" CssClass="table table-hover table-condensed" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowSorting="True" AllowPaging="True">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            No.
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="CounterLabel" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                    <asp:BoundField DataField="Activity_Date_Start" HeaderText="Activity Date" SortExpression="Activity_Date_Start" />
                    <asp:BoundField DataField="Company_Status" HeaderText="Status" SortExpression="Company_Status" />
                    <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" SelectCommand="GetInvestorsActivitiesLog" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Id" Name="sortExpression" Type="String" />
                    <asp:Parameter DefaultValue="DESC" Name="sortDirection" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
