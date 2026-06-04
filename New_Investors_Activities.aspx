<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Investors_Activities.aspx.cs" Inherits="CRM.New_Investors_Activities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <title>Add New Investors Activities</title>
    <style>
        /* Solid border */
        hr.solid {
            border-top: 3px solid #1e17c1;
        }
        /* Rounded border */
        hr.rounded {
            border-top: 5px solid #1e17c1;
            border-radius: 5px;
        }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
            border-radius: 10px;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 2px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
                border-radius: 50%;
            }

        .toggle-switch {
            display: inline-block;
            width: 40px;
            height: 20px;
            margin-bottom: 0;
            padding: 0;
            position: relative;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
        }

            .toggle-switch input[type="checkbox"] {
                opacity: 0;
                width: 0;
                height: 0;
            }

                .toggle-switch input[type="checkbox"] + .slider {
                    background-color: #ccc;
                    position: absolute;
                    top: 0;
                    left: 0;
                    right: 0;
                    bottom: 0;
                    transition: .4s;
                }

                .toggle-switch input[type="checkbox"]:checked + .slider {
                    background-color: #2196F3;
                }

            .toggle-switch .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 2px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
                border-radius: 50%;
            }

            .toggle-switch input[type="checkbox"]:checked + .slider:before {
                -webkit-transform: translateX(20px);
                -ms-transform: translateX(20px);
                transform: translateX(20px);
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(20px);
            -ms-transform: translateX(20px);
            transform: translateX(20px);
        }

        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <script>
        $(document).ready(function () {
            // hide the modal initially
            $('#myModal').modal('hide');
            $('#myModalDesision').modal('hide');

            // show the modal when RFP is selected
            $('#<%= ddlInvestorStatus.ClientID %>').change(function () {
                if ($(this).val() === 'RFP') {
                    $('#myModal').modal('show');
                }
                if ($(this).val() === 'Decision') {
                    $('#myModalDecision').modal('show');
                }
            });
        });

        /*Line 84 271*/
        function validateCompanyName() {
            var nameTextBox = document.getElementById("txtCompanyName");
            if (nameTextBox.value.trim() == "") {
                alert("Company Name cannot be blank.");
                nameTextBox.focus();
                return false;
            }
            return true;
        }

    </script>
</head>
<body>

    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
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
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. New Activity</a></li>
                            <li><a href="List_Investors_Activities.aspx">2. Activity List</a></li>
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
        <div class="container">
            <div class="col-sm-12">
                <h2>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-lg"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                    New Activity</h2>
                <hr class="rounded" />
            </div>
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="ddlCompanyName">1. Company Name</label>
                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" DataTextField="Company_Name" DataValueField="Company_Name" AutoPostBack="True"></asp:DropDownList>
                </div>
                <%--            <div class="form-group">
                <label for="txtActivityType">Activity Type</label>
                <asp:TextBox ID="txtActivityType" runat="server" CssClass="form-control" placeholder="Activity Type" MaxLength="100"></asp:TextBox>
            </div>--%>
                <div class="form-group">
                    <label for="txtActivityDateStart">2. Activity Date</label>
                    <asp:TextBox ID="txtActivityDateStart" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label for="ddlInvestorStatus" runat="server">3. Investors Status</asp:Label>
                    <asp:DropDownList ID="ddlInvestorStatus" CssClass="form-control" runat="server">
                        <asp:ListItem Value="">--Select--</asp:ListItem>
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
                <div class="form-group">
                    4.
                <label for="txtActivityDescription">Activity Description</label>
                    <asp:TextBox ID="txtActivityDescription" runat="server" CssClass="form-control" placeholder="Activity Description" TextMode="MultiLine" Rows="5" MaxLength="300"></asp:TextBox>
                </div>
                
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="BtnCancel_Click" />
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
        </div>
        <!-- Start Modal RFP -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <!-- Start Modal content RFP-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Enter RFP Details</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>RFP ON/OFF:</label>
                            <label class="toggle-switch">
                                <asp:CheckBox ID="IsRFP" runat="server" />
                                <span class="slider"></span>
                            </label>
                        </div>
                        <div class="form-group">
                            <label for="rfpDate">RFP Date Start:</label>
                            <asp:TextBox ID="txtRFPDateStart" CssClass="form-control" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="rfpDate">RFP Date End:</label>
                            <asp:TextBox ID="txtRFPDateEnd" CssClass="form-control" runat="server" placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtRFPDescription">RFP Comments:</label>
                            <asp:TextBox ID="txtRFPDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSaveRfpDetails" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveRFPDetails_Click" />
                    </div>
                </div>
                <!-- End Modal content RFP-->
            </div>
        </div>
        <!-- End Modal RFP -->

        <!-- Start Modal Decision -->
        <div class="modal fade" id="myModalDecision" role="dialog">
            <div class="modal-dialog">
                <!-- Start Modal content RFP-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Enter Decision Details</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:Label for="ddlDecisionStatus" runat="server" Text="Decision Status"></asp:Label>
                            <asp:DropDownList ID="ddlDecisionStatus" CssClass="form-control" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="On Hold">1. On Hold</asp:ListItem>
                                <asp:ListItem Value="Extension Of Timeline">2. Extension Of Timeline</asp:ListItem>
                                <asp:ListItem Value="Proceed to Lease Agreement">3. Proceed to Lease Agreement</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtDecisionDescription">Decision Comments:</label>
                            <asp:TextBox ID="txtDecisionDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="3" MaxLength="200"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveDecisionDetails_Click" />
                    </div>
                </div>
                <!-- End Modal content Decision-->
            </div>
        </div>
        <!-- End Modal Decision -->
    </form>
    <hr />
    <div class="container">
        <p>&copy; <%: DateTime.Now.Year %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 6 March 2023</p>
    </div>
</body>
</html>
