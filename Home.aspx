<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CRM.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to CRM Apps for Kulim Technology Park Corporation</title>
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
                            <%--                            <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>--%>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. Add New Activity</a></li>
                            <li><a href="List_Investors_Activities.aspx">2. Investor's Activity List</a></li>
                            <li><a href="ActivityTimeline.aspx">3. Activity Timeline</a></li><li><a href="List_RFP.aspx">4. RFP</a></li>
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
        <div>
            <div class="container">
                <div class="row justify-content-center align-items-center">
                    <div class="col-12 col-md-6 image-wrapper">
                        <img src="/images/crm-modules.jpg" alt="KTPC CRM 2023" />
                    </div>
                    <div class="col-12 col-md">
                        <div class="text-wrapper">

                            <p class="mbr-text mbr-fonts-style display-4">
                                <br />
                                <br />
                            </p>
                            <div class="mbr-section-btn mt-3"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <div class="row align-center mbr-white">
                <div class="row">
                    <p class="mbr-text mb-0 mbr-fonts-style mbr-white align-center display-7">
                        <span data-toggle="modal" data-target="#myModalUpdates">
                            <a class="btn btn-default" data-toggle="tooltip" data-placement="bottom" title="Update Log"><span class="glyphicon glyphicon-cog"></span></a>
                        </span>
                        Â© Copyright 2023 Kulim Technology Park Corporation Sdn Bhd. All Rights Reserved.
                    </p>
                </div>

            </div>
        </div>
        <!-- START MODAL -->
        <div class="modal fade" id="myModalUpdates" role="dialog">
            <div class="modal-dialog" role="document">              
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">System Updates</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <table>
                                <tr>
                                    <td>&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/spanner.png" />&nbsp;</td>
                                    <td>
                                        <p class="mbr-text mb-0 mbr-fonts-style mbr-white align-center display-7">
                                            Upgrades: 1/Aug/2023: Attachment file size increased to max 50MB.<br />
                                            Add visitor counters and Last Visit to Login Page.<br />
                                            Error Fix: Files upload in edit investors profile uploaded to the corrected folders.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
            <!-- END MODAL -->
        </div>
    </form>
    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/smoothscroll/smooth-scroll.js"></script>
    <script src="assets/ytplayer/index.js"></script>
    <script src="assets/dropdown/js/navbar-dropdown.js"></script>
    <script src="assets/theme/js/script.js"></script>
</body>
</html>
