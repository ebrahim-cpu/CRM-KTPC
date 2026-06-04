<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CRM.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRM Dashboard</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        .panel-heading { font-weight: bold; font-size: 18px; }
        .stat-number { font-size: 80px; margin: 20px 0; font-weight: bold; }
        .hot-status { background-color: red; font-weight: bold; color: yellow; padding: 2px 5px; border-radius: 5px; box-shadow: 2px 2px 4px rgba(0,0,0,0.3); }
        .warm-status { background-color: orange; font-weight: bold; color: black; padding: 2px 5px; border-radius: 5px; box-shadow: 2px 2px 4px rgba(0,0,0,0.3); }
        .cold-status { background-color: blue; font-weight: bold; color: white; padding: 2px 5px; border-radius: 5px; box-shadow: 2px 2px 4px rgba(0,0,0,0.3); }
        .green-status { background-color: forestgreen; font-weight: bold; color: white; padding: 2px 5px; border-radius: 5px; box-shadow: 2px 2px 4px rgba(0,0,0,0.3); }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="Dashboard.aspx">CRM v1.0</a>
                </div>
                <ul class="nav navbar-nav">
                    
                    <li class="active"><a href="Dashboard.aspx">Dashboard</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">2. Investor List</a></li>
                            <li><a href="List_Investors_Profile2.aspx">2b. Investor List (Stacked)</a></li>
                            <li><a href="List_Contacts.aspx">3. Contact List</a></li>
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

        <div class="container" style="margin-top: 50px;">
            <h2 class="text-center" style="margin-bottom: 30px;">Leads Status Overview</h2>
            <div class="row text-center">
                <div class="col-md-3">
                    <div class="panel" style="border: 2px solid red;">
                        <div class="panel-heading" style="background-color: red; color: yellow;">HOT</div>
                        <div class="panel-body">
                            <div class="stat-number"><asp:Label ID="lblHot" runat="server">0</asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel" style="border: 2px solid orange;">
                        <div class="panel-heading" style="background-color: orange; color: black;">WARM</div>
                        <div class="panel-body">
                            <div class="stat-number"><asp:Label ID="lblWarm" runat="server">0</asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel" style="border: 2px solid blue;">
                        <div class="panel-heading" style="background-color: blue; color: white;">COLD</div>
                        <div class="panel-body">
                            <div class="stat-number"><asp:Label ID="lblCold" runat="server">0</asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel" style="border: 2px solid forestgreen;">
                        <div class="panel-heading" style="background-color: forestgreen; color: white;">COMPLETED</div>
                        <div class="panel-body">
                            <div class="stat-number"><asp:Label ID="lblCompleted" runat="server">0</asp:Label></div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Expandable Panels Section -->
            <div class="row" style="margin-top: 40px;">
                <div class="col-md-12">
                    <div class="panel-group" id="accordionMain">

                        <!-- HOT Panel -->
                        <div class="panel panel-danger">
                            <div class="panel-heading" style="background-color: red; color: yellow;">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionMain" href="#collapseHot" style="text-decoration:none;">
                                        <i class="glyphicon glyphicon-fire"></i> HOT LEADS <span class="badge pull-right" style="margin-top: 2px;"><%= lblHot.Text %></span>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseHot" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="panel-group" id="accordionHot">
                                        <asp:Repeater ID="rptHot" runat="server">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" style="background-color: #f9f9f9;">
                                                        <h4 class="panel-title" style="font-size: 14px;">
                                                            <a data-toggle="collapse" data-parent="#accordionHot" href='#collapseCompany_<%# Eval("Id") %>' style="text-decoration:none;">
                                                                <i class="glyphicon glyphicon-triangle-right"></i> <%# Container.ItemIndex + 1 %>. <%# Eval("Company_Name") %>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id='collapseCompany_<%# Eval("Id") %>' class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <b>PIC:</b> <%# Eval("PIC") %><br />
                                                                    <b>Country:</b> <%# Eval("Company_Country") %><br />
                                                                    <b>Date Inquiry:</b> <%# Eval("Date_Inquiry", "{0:dd-MMM-yyyy}") %>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <b>Sector:</b> <%# Eval("Company_Industry_Type") %><br />
                                                                    <b>Land Required:</b> <%# Eval("Land_Size_Required") %> acres<br />
                                                                    <b>Decision Making:</b> <%# Eval("Investors_Decision_Making") %>
                                                                </div>
                                                                <div class="col-sm-12" style="margin-top: 10px;">
                                                                    <a href='Edit_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Edit Profile">
                                                                        <i class="glyphicon glyphicon-pencil"></i> Edit
                                                                    </a><a href='FileManage_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Manage Files">
                                                                        <i class="glyphicon glyphicon-paperclip"></i> Files
                                                                    </a>
                                                                    
                                                                    <a href='Print_Investors_Profile.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Print Profile">
                                                                        <i class="glyphicon glyphicon-print"></i> Print
                                                                    </a>
                                                                    <a href='company_contacts.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Company Contacts">
                                                                        <i class="glyphicon glyphicon-phone"></i> Contacts <span class="badge"><%# Eval("ContactCount") %></span>
                                                                    </a>
                                                                    <a href='List_Investors_Activities2.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Company Activities">
                                                                        <i class="glyphicon glyphicon-list-alt"></i> Activities <span class="badge"><%# Eval("ActivityCount") %></span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- WARM Panel -->
                        <div class="panel panel-warning" style="border-color: orange;">
                            <div class="panel-heading" style="background-color: orange; color: black;">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionMain" href="#collapseWarm" style="text-decoration:none;">
                                        <i class="glyphicon glyphicon-map-marker"></i> WARM LEADS <span class="badge pull-right" style="margin-top: 2px;"><%= lblWarm.Text %></span>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseWarm" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="panel-group" id="accordionWarm">
                                        <asp:Repeater ID="rptWarm" runat="server">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" style="background-color: #f9f9f9;">
                                                        <h4 class="panel-title" style="font-size: 14px;">
                                                            <a data-toggle="collapse" data-parent="#accordionWarm" href='#collapseCompany_<%# Eval("Id") %>' style="text-decoration:none;">
                                                                <i class="glyphicon glyphicon-triangle-right"></i> <%# Container.ItemIndex + 1 %>. <%# Eval("Company_Name") %>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id='collapseCompany_<%# Eval("Id") %>' class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <b>PIC:</b> <%# Eval("PIC") %><br />
                                                                    <b>Country:</b> <%# Eval("Company_Country") %><br />
                                                                    <b>Date Inquiry:</b> <%# Eval("Date_Inquiry", "{0:dd-MMM-yyyy}") %>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <b>Sector:</b> <%# Eval("Company_Industry_Type") %><br />
                                                                    <b>Land Required:</b> <%# Eval("Land_Size_Required") %> acres<br />
                                                                    <b>Decision Making:</b> <%# Eval("Investors_Decision_Making") %>
                                                                </div>
                                                                <div class="col-sm-12" style="margin-top: 10px;">
                                                                    <a href='Edit_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Edit Profile">
                                                                        <i class="glyphicon glyphicon-pencil"></i> Edit
                                                                    </a><a href='FileManage_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Manage Files">
                                                                        <i class="glyphicon glyphicon-paperclip"></i> Files
                                                                    </a>
                                                                    
                                                                    <a href='Print_Investors_Profile.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Print Profile">
                                                                        <i class="glyphicon glyphicon-print"></i> Print
                                                                    </a>
                                                                    <a href='company_contacts.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Company Contacts">
                                                                        <i class="glyphicon glyphicon-phone"></i> Contacts <span class="badge"><%# Eval("ContactCount") %></span>
                                                                    </a>
                                                                    <a href='List_Investors_Activities2.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Company Activities">
                                                                        <i class="glyphicon glyphicon-list-alt"></i> Activities <span class="badge"><%# Eval("ActivityCount") %></span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- COLD Panel -->
                        <div class="panel panel-primary" style="border-color: blue;">
                            <div class="panel-heading" style="background-color: blue; color: white;">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionMain" href="#collapseCold" style="text-decoration:none;">
                                        <i class="glyphicon glyphicon-ice-lolly"></i> COLD LEADS <span class="badge pull-right" style="margin-top: 2px;"><%= lblCold.Text %></span>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseCold" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="panel-group" id="accordionCold">
                                        <asp:Repeater ID="rptCold" runat="server">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" style="background-color: #f9f9f9;">
                                                        <h4 class="panel-title" style="font-size: 14px;">
                                                            <a data-toggle="collapse" data-parent="#accordionCold" href='#collapseCompany_<%# Eval("Id") %>' style="text-decoration:none;">
                                                                <i class="glyphicon glyphicon-triangle-right"></i> <%# Container.ItemIndex + 1 %>. <%# Eval("Company_Name") %>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id='collapseCompany_<%# Eval("Id") %>' class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <b>PIC:</b> <%# Eval("PIC") %><br />
                                                                    <b>Country:</b> <%# Eval("Company_Country") %><br />
                                                                    <b>Date Inquiry:</b> <%# Eval("Date_Inquiry", "{0:dd-MMM-yyyy}") %>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <b>Sector:</b> <%# Eval("Company_Industry_Type") %><br />
                                                                    <b>Land Required:</b> <%# Eval("Land_Size_Required") %> acres<br />
                                                                    <b>Decision Making:</b> <%# Eval("Investors_Decision_Making") %>
                                                                </div>
                                                                <div class="col-sm-12" style="margin-top: 10px;">
                                                                    <a href='Edit_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Edit Profile">
                                                                        <i class="glyphicon glyphicon-pencil"></i> Edit
                                                                    </a><a href='FileManage_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Manage Files">
                                                                        <i class="glyphicon glyphicon-paperclip"></i> Files
                                                                    </a>
                                                                    
                                                                    <a href='Print_Investors_Profile.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Print Profile">
                                                                        <i class="glyphicon glyphicon-print"></i> Print
                                                                    </a>
                                                                    <a href='company_contacts.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Company Contacts">
                                                                        <i class="glyphicon glyphicon-phone"></i> Contacts <span class="badge"><%# Eval("ContactCount") %></span>
                                                                    </a>
                                                                    <a href='List_Investors_Activities2.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Company Activities">
                                                                        <i class="glyphicon glyphicon-list-alt"></i> Activities <span class="badge"><%# Eval("ActivityCount") %></span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- COMPLETED Panel -->
                        <div class="panel panel-success" style="border-color: forestgreen;">
                            <div class="panel-heading" style="background-color: forestgreen; color: white;">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionMain" href="#collapseCompleted" style="text-decoration:none;">
                                        <i class="glyphicon glyphicon-ok"></i> COMPLETED <span class="badge pull-right" style="margin-top: 2px;"><%= lblCompleted.Text %></span>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseCompleted" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="panel-group" id="accordionCompleted">
                                        <asp:Repeater ID="rptCompleted" runat="server">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" style="background-color: #f9f9f9;">
                                                        <h4 class="panel-title" style="font-size: 14px;">
                                                            <a data-toggle="collapse" data-parent="#accordionCompleted" href='#collapseCompany_<%# Eval("Id") %>' style="text-decoration:none;">
                                                                <i class="glyphicon glyphicon-triangle-right"></i> <%# Container.ItemIndex + 1 %>. <%# Eval("Company_Name") %>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id='collapseCompany_<%# Eval("Id") %>' class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <b>PIC:</b> <%# Eval("PIC") %><br />
                                                                    <b>Country:</b> <%# Eval("Company_Country") %><br />
                                                                    <b>Date Inquiry:</b> <%# Eval("Date_Inquiry", "{0:dd-MMM-yyyy}") %>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <b>Sector:</b> <%# Eval("Company_Industry_Type") %><br />
                                                                    <b>Land Required:</b> <%# Eval("Land_Size_Required") %> acres<br />
                                                                    <b>Decision Making:</b> <%# Eval("Investors_Decision_Making") %>
                                                                </div>
                                                                <div class="col-sm-12" style="margin-top: 10px;">
                                                                    <a href='Edit_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Edit Profile">
                                                                        <i class="glyphicon glyphicon-pencil"></i> Edit
                                                                    </a><a href='FileManage_Investors_Profile2.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Manage Files">
                                                                        <i class="glyphicon glyphicon-paperclip"></i> Files
                                                                    </a>
                                                                    
                                                                    <a href='Print_Investors_Profile.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Print Profile">
                                                                        <i class="glyphicon glyphicon-print"></i> Print
                                                                    </a>
                                                                    <a href='company_contacts.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-sm" title="Company Contacts">
                                                                        <i class="glyphicon glyphicon-phone"></i> Contacts <span class="badge"><%# Eval("ContactCount") %></span>
                                                                    </a>
                                                                    <a href='List_Investors_Activities2.aspx?id=<%# Eval("Id") %>' target="_blank" class="btn btn-default btn-sm" title="Company Activities">
                                                                        <i class="glyphicon glyphicon-list-alt"></i> Activities <span class="badge"><%# Eval("ActivityCount") %></span>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            
        </div>
    </form>
</body>
</html>


