<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List_Investors_Profile.aspx.cs" Inherits="CRM.List_Investors_Profile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>List Of Investor's Activities</title>
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
        .hot-status {
            background-color: red;
            font-weight: bold;
            color: yellow;
            padding: 5px;
            border-radius: 5px; /* Add border-radius to create a more curvy background */
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3); /* Add box-shadow for a slight shadow effect */
            /* Add more styles as needed */
        }
        .warm-status {
            background-color: orange;
            font-weight: bold;
            color: black;
            padding: 5px;
            border-radius: 5px; /* Add border-radius to create a more curvy background */
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3); /* Add box-shadow for a slight shadow effect */
            /* Add more styles as needed */
        }
        .cold-status {
            background-color: blue;
            font-weight: bold;
            color: white;
            padding: 5px;
            border-radius: 5px; /* Add border-radius to create a more curvy background */
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3); /* Add box-shadow for a slight shadow effect */
            /* Add more styles as needed */
        }
        .green-status {
            background-color: forestgreen;
            font-weight: bold;
            color: white;
            padding: 5px;
            border-radius: 5px; /* Add border-radius to create a more curvy background */
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3); /* Add box-shadow for a slight shadow effect */
            /* Add more styles as needed */
        }
        .brown-status {
            background-color: saddlebrown;
            font-weight: bold;
            color: white;
            padding: 5px;
            border-radius: 5px; /* Add border-radius to create a more curvy background */
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3); /* Add box-shadow for a slight shadow effect */
            /* Add more styles as needed */
        }
        /* Style for the LinkButton */
        .btn-leads-status {
            display: inline-block;
            padding: 5px 10px;
            border: 1px solid #ccc;
            background-color: #f0f0f0;
            border-radius: 5px;
            box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
            text-decoration: none;
            color: #333;
        }
            /* Hover effect for the LinkButton */
            .btn-leads-status:hover {
                background-color: #e0e0e0;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                <div class="col-sm-6" align="left">
                    <h2>Investors List</h2>
                </div>
                <div class="col-sm-6" align="right">
                    <a href="New_Investors_Profile.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="New Investor"><span class="glyphicon glyphicon-plus"></span></a>
                    <a href="Home.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Home"><span class="glyphicon glyphicon-home"></span></a>
                    <a href="List_Contacts.aspx" class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Contacts"><span class="glyphicon glyphicon-phone-alt"></span></a>
                    <asp:Button ID="btnExport" runat="server" class="btn btn-primary" Text="Export to Excel" OnClick="BtnExport_Click" />
                </div>
            </div>
            <asp:GridView ID="gvActivities" runat="server" CssClass="table table-hover table-condensed" AutoGenerateColumns="False" OnRowDeleting="GvActivities_RowDeleting" DataKeyNames="Id" CellPadding="4" ForeColor="#000000" GridLines="None" AllowSorting="True" OnSorting="GvActivities_Sorting" OnRowEditing="GvActivities_RowEditing" OnSelectedIndexChanged="GvActivities_SelectedIndexChanged" OnRowDataBound="gvActivities_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Company_Name" HeaderText="Company" SortExpression="Company_Name" />
                    <asp:BoundField DataField="PIC" HeaderText="PIC" SortExpression="PIC" />
                    <asp:BoundField DataField="Company_Status" HeaderText="Status" SortExpression="Company_Status" />
                    <asp:BoundField DataField="Land_Size_Required" HeaderText="Land (acre)" SortExpression="Land_Size_Required" />
                    <asp:BoundField DataField="Building_Size" HeaderText="Building (sf)" SortExpression="Building_Size" />
                    <asp:BoundField DataField="Company_Industry_Type" HeaderText="Promoted Sector" SortExpression="Company_Industry_Type" />
                    <asp:TemplateField HeaderText="Invest Type" SortExpression="Investment_Type" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnInvestmentType" runat="server" Text='<%# Eval("Investment_Type") %>'
                                CssClass='<%# GetLeadsStatusStyle(Eval("Investment_Type").ToString()) %>'
                                CommandName="Sort" CommandArgument="Investment_Type" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Company_Country" HeaderText="Country" SortExpression="Company_Country" />
                    <asp:TemplateField HeaderText="Status" SortExpression="Leads_Status" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnLeadsStatus" runat="server" Text='<%# Eval("Leads_Status") %>'
                                CssClass='<%# GetLeadsStatusStyle(Eval("Leads_Status").ToString()) %>'
                                CommandName="Sort" CommandArgument="Leads_Status" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" data-toggle="tooltip" title="Edit" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("Id") %>' OnClick="BtnEdit_Click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" title="Delete" CssClass="btn btn-danger btn-xs" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                            <asp:LinkButton ID="btnFiles" runat="server" title="Files" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("Id") %>' OnClick="BtnFiles_Click"><span class="glyphicon glyphicon-paperclip"></span></asp:LinkButton>
                            <asp:LinkButton ID="btnPrint" runat="server" title="Print" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("Id") %>' OnClick="BtnPrint_Click"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>
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
                        <div class="col-sm-11">
                            5.<a href="List_LOG_Investors_Activities.aspx" class="btn btn-primary">Audit Trail Activities Data</a>
                            Audit Trail.   
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
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 6 Jun 2023</p>
    </div>
</body>
</html>

