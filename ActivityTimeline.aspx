<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityTimeline.aspx.cs" Inherits="CRM.ActivityTimeline" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Investor Activity Timeline</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        .timeline {
            position: relative;
            padding: 20px 0;
            margin-top: 10px;
            list-style: none;
        }

        .timeline:before {
            top: 0;
            bottom: 0;
            position: absolute;
            content: " ";
            width: 3px;
            background-color: #e6e9ec;
            left: 70px;
            margin-left: -1.5px;
        }

        .timeline > li {
            margin-bottom: 25px;
            position: relative;
        }

        .timeline > li:before,
        .timeline > li:after {
            content: " ";
            display: table;
        }

        .timeline > li:after {
            clear: both;
        }

        .timeline > li > .timeline-panel {
            width: calc(100% - 100px);
            float: right;
            border: 1px solid #e1e6eb;
            border-radius: 8px;
            padding: 18px;
            position: relative;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.03);
            background: #fff;
            transition: all 0.2s ease-in-out;
        }
        
        .timeline > li > .timeline-panel:hover {
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.08);
            transform: translateY(-2px);
            border-color: #c5d0dc;
        }

        .timeline > li > .timeline-badge {
            color: #fff;
            width: 42px;
            height: 42px;
            line-height: 42px;
            font-size: 1.4em;
            text-align: center;
            position: absolute;
            top: 15px;
            left: 49px;
            background-color: #337ab7;
            z-index: 10;
            border-radius: 50%;
            border: 3px solid #fff;
            box-shadow: 0 2px 5px rgba(0,0,0,0.15);
        }

        .timeline-title {
            margin-top: 0;
            font-weight: 700;
            color: #2b3d51;
            font-size: 18px;
        }

        .timeline-body > p {
            margin-bottom: 0;
            color: #555;
            line-height: 1.6;
            font-size: 14px;
        }
        
        .timeline-date {
            float: left;
            width: 45px;
            text-align: right;
            font-size: 11px;
            color: #667;
            font-weight: bold;
            margin-top: 22px;
            line-height: 1.3;
        }
        
        .timeline-date .day {
            font-size: 18px;
            color: #2b3d51;
            display: block;
        }

        .status-badge {
            display: inline-block;
            padding: 4px 8px;
            font-size: 11px;
            font-weight: bold;
            color: #fff;
            border-radius: 4px;
            margin-bottom: 8px;
            box-shadow: 1px 1px 3px rgba(0,0,0,0.1);
        }
        
        .timeline-footer {
            margin-top: 15px;
            border-top: 1px solid #f0f3f6;
            padding-top: 12px;
        }
    </style>
</head>
<body style="background-color: #f5f7fa;">
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
                            <li><a href="List_Investors_Profile2.aspx">2b. Investor List (Stacked)</a></li>
                            <li><a href="List_Contacts.aspx">3. Contact List</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. New Activity</a></li>
                            <li><a href="List_Investors_Activities.aspx">2. Activity List</a></li>
                            <li class="active"><a href="ActivityTimeline.aspx">3. Activity Timeline</a></li>
                            <li><a href="List_RFP.aspx">4. RFP</a></li>
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

        <div class="container" style="margin-top: 30px; margin-bottom: 50px;">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default" style="border-radius: 8px; box-shadow: 0 4px 15px rgba(0,0,0,0.05);">
                        <div class="panel-heading" style="background-color: #2b3d51; color: #fff; font-weight: bold; border-top-left-radius: 7px; border-top-right-radius: 7px;">
                            <h3 class="panel-title" style="font-size: 18px; padding: 5px 0;"><i class="glyphicon glyphicon-tasks"></i> Investor Activity Timeline</h3>
                        </div>
                        <div class="panel-body">
                            <!-- Filter Section -->
                            <div class="row" style="margin-bottom: 25px; padding-bottom: 15px; border-bottom: 1px dashed #e1e6eb;">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label for="ddlCompanyName" class="control-label" style="font-weight: bold; color: #555;">Select Company:</label>
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label for="ddlInvestorStatus" class="control-label" style="font-weight: bold; color: #555;">Select Status:</label>
                                        <asp:DropDownList ID="ddlInvestorStatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlInvestorStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="Preliminary Enquiry">1. Preliminary Enquiry</asp:ListItem>
                                            <asp:ListItem Value="Due Dilligence">2. Due Dilligence</asp:ListItem>
                                            <asp:ListItem Value="RFI">3. RFI</asp:ListItem>
                                            <asp:ListItem Value="Site Visit">4. Site Visit</asp:ListItem>
                                            <asp:ListItem Value="RFP">5. RFP</asp:ListItem>
                                            <asp:ListItem Value="Decision">6. Decision</asp:ListItem>
                                            <asp:ListItem Value="Letter Of Intent">7. Letter Of Intent</asp:ListItem>
                                            <asp:ListItem Value="Lease Agreement">8. Lease Agreement</asp:ListItem>
                                            <asp:ListItem Value="Collection of First Payment">9. Collection of First Payment</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2" style="margin-top: 25px; text-align: right;">
                                    <a href="New_Investors_Activities.aspx" class="btn btn-primary btn-block" title="Add New Activity">
                                        <i class="glyphicon glyphicon-plus"></i> New Activity
                                    </a>
                                </div>
                            </div>

                            <!-- Timeline Display -->
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    
                                    <asp:PlaceHolder ID="phNoData" runat="server" Visible="false">
                                        <div class="alert alert-warning text-center" style="margin-top: 20px; border-radius: 6px;">
                                            <i class="glyphicon glyphicon-info-sign" style="font-size: 18px; vertical-align: middle; margin-right: 5px;"></i>
                                            No activities found for the selected company and status filter.
                                        </div>
                                    </asp:PlaceHolder>

                                    <ul class="timeline">
                                        <asp:Repeater ID="rptTimeline" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="timeline-date">
                                                        <span class="day"><%# Eval("Activity_Date_Start", "{0:dd}") %></span>
                                                        <span><%# Eval("Activity_Date_Start", "{0:MMM yy}") %></span>
                                                    </div>
                                                    <div class="timeline-badge bg-primary">
                                                        <i class="glyphicon glyphicon-comment" style="margin-top: 8px;"></i>
                                                    </div>
                                                    <div class="timeline-panel">
                                                        <div class="timeline-heading">
                                                            <span class="status-badge" style='<%# GetStatusBadgeStyle(Eval("Company_Status").ToString()) %>'>
                                                                <%# Eval("Company_Status") %>
                                                            </span>
                                                            <h4 class="timeline-title"><%# Eval("Company_Name") %></h4>
                                                            <p style="margin-bottom: 0;">
                                                                <small class="text-muted">
                                                                    <i class="glyphicon glyphicon-time"></i> Updated on <%# Eval("UpdatedDate", "{0:dd-MMM-yyyy}") %> by <strong><%# Eval("UpdatedBy") %></strong>
                                                                </small>
                                                            </p>
                                                        </div>
                                                        <div class="timeline-body" style="margin-top: 12px; border-top: 1px dashed #f0f3f6; padding-top: 12px;">
                                                            <p><%# Eval("Activity_Description").ToString().Replace("\n", "<br />") %></p>
                                                        </div>
                                                        <div class="timeline-footer">
                                                            <a href='Edit_Investors_Activities.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-xs" title="Edit Activity">
                                                                <i class="glyphicon glyphicon-pencil"></i> Edit
                                                            </a>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-default btn-xs" title="Delete Activity" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                                                                <i class="glyphicon glyphicon-trash"></i> Delete
                                                            </asp:LinkButton>
                                                            <a href='FileManage_Investor_Activities.aspx?id=<%# Eval("Id") %>' class="btn btn-default btn-xs" title="Activity Files">
                                                                <i class="glyphicon glyphicon-paperclip"></i> Files
                                                            </a>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    
    <hr />
    <div class="container text-center" style="margin-bottom: 20px;">
        <p class="text-muted">&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 6 Jun 2026</p>
    </div>
</body>
</html>
