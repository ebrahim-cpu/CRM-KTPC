<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_Investors_Activities_2.aspx.cs" Inherits="CRM.List_Investors_Activities_2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List Of Investor's Activities</title>
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
                            <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">2. Investor List I</a></li>
                            <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. New Activity</a></li>
                            <li><a href="List_Investors_Activities.aspx">2. Activity List</a></li>
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
            </div>
        </nav>
        <%--END NAV--%>

        <div class="col-sm-12">
            Select investor:
            <asp:DropDownList ID="ddlCompanyName" runat="server" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          &nbsp;&nbsp;&nbsp;&nbsp;Select Status:
        <asp:DropDownList ID="ddlInvestorStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvestorStatus_SelectedIndexChanged" >
            <asp:ListItem Value="Preliminary Enquiry">1. Preliminary Enquiry</asp:ListItem>
            <asp:ListItem Value="Due Dilligence">2. Due Dilligence</asp:ListItem>
            <asp:ListItem Value="RFI">3. RFI</asp:ListItem>
            <asp:ListItem Value="Site Visit">4. Site Visit</asp:ListItem>
            <asp:ListItem Value="RFP">5. RFP</asp:ListItem>
            <asp:ListItem Value="Decision">6. Decision</asp:ListItem>
            <asp:ListItem Value="Letter Of Intent">7. Letter Of Intent</asp:ListItem>
            <asp:ListItem Value="Lease Agreement">8. Lease Agreement</asp:ListItem>
            <asp:ListItem Value="Collection of First Payment">9. Collection of First Payment</asp:ListItem>
            <asp:ListItem Value="" Selected="True">All</asp:ListItem>
        </asp:DropDownList>

        </div>
      

        <div class="col-sm-6" align="left">
            <h2>Activity List</h2>
        </div>
        <div class="col-sm-6" align="right">
            <a href="New_Investors_Activities.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="New Investor Activity"><span class="glyphicon glyphicon-plus"></span></a>
            <a href="Home.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Home"><span class="glyphicon glyphicon-home"></span></a>

            <asp:Button ID="btnExport" runat="server" class="btn btn-primary" Text="Export to Excel" OnClick="btnExport_Click" />
            <%--<a href="New_Investors_Profile.aspx" class="btn btn-primary">Add New Investors</a>
                    <a href="New_Investors_Activities.aspx" class="btn btn-primary">Add New Activities</a>--%>
            <span data-toggle="modal" data-target="#helpDialog">
                <a class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Apps Assistance"><span class="glyphicon glyphicon-cog"></span></a>
            </span>
        </div>
        <div class="col-sm-12">
            <asp:GridView ID="gvActivities" runat="server" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" OnRowDeleting="gvActivities_RowDeleting" DataKeyNames="Id" CellPadding="4" ForeColor="#000000" GridLines="None" AllowSorting="True" OnSorting="gvActivities_Sorting" OnRowEditing="gvActivities_RowEditing">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <%-- <asp:BoundField DataField="Id" HeaderText="ID" /> --%>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                    <asp:BoundField DataField="Activity_Date_Start" HeaderText="Date" DataFormatString="{0: dd/MMM/yy}" SortExpression="Activity_Date_Start" />

                    <asp:BoundField DataField="Company_Status" HeaderText="Status" SortExpression="Company_Status" />
                    <asp:BoundField DataField="Activity_Description" HeaderText="Description" />
                    <asp:BoundField DataField="UpdatedDate" HeaderText="Updated" DataFormatString="{0: dd/MMM/yy}" SortExpression="UpdatedDate" />
                    <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" SortExpression="UpdatedBy" />


                    <%--<asp:CommandField ShowDeleteButton="True" />--%>
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

        <div id="helpDialog" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #a1a1a1; color: #ffffff">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><span class="glyphicon glyphicon-comment"></span>&nbsp;Apps Assistance</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-sm-11">
                            1.<a href="List_Investors_Profile.aspx" class="btn btn-primary">List Of Registered Investors</a><br />
                            This will display full list of Registered Investors in this CRM Apps.
                        </div>
                        <div class="col-sm-11">
                            2.<a href="New_Investors_Profile.aspx" class="btn btn-primary">Add New Investors</a><br />
                            This will bring you to the new Investor's Registration Form.
                        </div>
                        <div class="col-sm-11">
                            3.<a href="List_Investors_Activities.aspx" class="btn btn-primary">List Of Investor's Activities</a>
                            This will display lisf of Investor's Activities. You will need to select Investors Name in the drop down list at the top left hand side. 
                        </div>
                        <div class="col-sm-11">
                            4.<a href="New_Investors_Activities.aspx" class="btn btn-primary">Add New Activities</a>
                            This will bring you to the new Investor's Activities Registration Form. You need to select the investor's Name.  
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%-- <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-success" CommandName="Update" Width="100px" />--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <hr />
    <div class="col-sm-11">
        Please select the Investor's Name to get the list of activities for each Investors. Click button Files (green) to upload and attach files for each activities. 
    Click button Edit (blue) to edit Investor's activities record. Click button Delete (red) to delete Investor's activities record.
    </div>
    <br />

    <hr />
    <div class="container">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 15 March 2023</p>
    </div>
</body>
</html>
