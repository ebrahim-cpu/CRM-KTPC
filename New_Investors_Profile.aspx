<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Investors_Profile.aspx.cs"
    Inherits="CRM.New_Investors_Profile" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>New Investors</title>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
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

            .toggle-switch input[type="checkbox"]+.slider {
                background-color: #ccc;
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                transition: .4s;
            }

            .toggle-switch input[type="checkbox"]:checked+.slider {
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

            .toggle-switch input[type="checkbox"]:checked+.slider:before {
                -webkit-transform: translateX(20px);
                -ms-transform: translateX(20px);
                transform: translateX(20px);
            }

            input:checked+.slider {
                background-color: #2196F3;
            }

            input:focus+.slider {
                box-shadow: 0 0 1px #2196F3;
            }

            input:checked+.slider:before {
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
        <script>
            function updateStatusImage() {
                var selectedStatus = $('#ddlLeadsStatus').val();
                switch (selectedStatus) {
                    case "Hot":
                        $("#statusImage").attr("src", "hot.png");
                        break;
                    case "Warm":
                        $("#statusImage").attr("src", "warm.png");
                        break;
                    case "Cold":
                        $("#statusImage").attr("src", "cold.png");
                        break;
                    default:
                        $("#statusImage").attr("src", "cold.png");
                        break;
                }
            }
        </script>
    </head>

    <body>
        <form id="form1" runat="server" method="post" enctype="multipart/form-data">
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
                                    <li><a href="List_Investors_Profile.aspx">2. Investor List</a></li>
                                    <li><a href="List_Contacts.aspx">3. Contact List</a></li>
                                    <%-- <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a>
                            </li>--%>
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
                                    <asp:LinkButton ID="lnkSearch" class="btn btn-default" runat="server"><i
                                            class="glyphicon glyphicon-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUsername" runat="server"
                                ForeColor="#FFFFFF"></asp:Label>
                            &nbsp;<i class="glyphicon glyphicon-user" style="color: #FFFFFF"></i>
                        </div>
                        <ul class="nav navbar-nav">
                            <li><a href="Logout.aspx">Logout</a></li>
                        </ul>
                    </div>
                </nav>
                <%--END NAV--%>
                    <div class="col-sm-12">
                        <center>
                            <h2>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-lg"><span
                                        class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                New Investor
                            </h2>
                        </center>
                    </div>
                    <div class="col-sm-6">
                        <b>A. Investors Profile</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            1.
                            <asp:Label for="industrialPark_ddl" runat="server" Text="Industrial Park"></asp:Label>
                            <asp:DropDownList ID="industrialPark_ddl" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            2.
                            <asp:Label for="txtDateInquiry" runat="server" Text="Date Inquiry"></asp:Label>
                            <asp:TextBox ID="txtDateInquiry" runat="server" CssClass="form-control" type="date">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            3.
                            <asp:Label for="txtLeasePeriod" runat="server" Text="Lease Period (Years)"></asp:Label>
                            <asp:TextBox ID="txtLeasePeriod" runat="server" CssClass="form-control" type="number"
                                min="0" max="100" step="1"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            4.
                            <asp:Label for="txtCompanyName" runat="server" Text="Company Name *"></asp:Label>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"
                                placeholder="Company Name - Must fill in." MaxLength="200"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            5.
                            <asp:Label for="txtCompanyAddress" runat="server" Text="Company Address"></asp:Label>
                            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control"
                                placeholder="Company Address" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            6.
                            <asp:Label for="ddlCountry" runat="server" Text="Country"></asp:Label>
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            7.
                            <asp:Label for="txtCompanyPhone" runat="server" Text="Telephone Number"></asp:Label>
                            <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control"
                                placeholder="Phone or Mobile" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            8.
                            <asp:Label for="txtWebsite" runat="server" Text="Website URL"></asp:Label>
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="www."
                                MaxLength="200"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            9.
                            <asp:Label for="txtYearFounded" runat="server" Text="Year Founded"></asp:Label>
                            <asp:TextBox ID="txtYearFounded" runat="server" CssClass="form-control"
                                placeholder="Year Founded" MaxLength="4"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            10.
                            <asp:Label for="txtHeadquaters" runat="server" Text="Headquarters"></asp:Label>
                            <asp:TextBox ID="txtHeadquaters" runat="server" CssClass="form-control"
                                placeholder="Headquaters" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            11.
                            <asp:Label for="txtBranches" runat="server" Text="Branches"></asp:Label>
                            <asp:TextBox ID="txtBranches" runat="server" CssClass="form-control" placeholder="Branches"
                                TextMode="MultiLine" Rows="3" MaxLength="300"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>B. Contact Person</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            12.
                            <asp:Label for="txtCompanyContactPerson" runat="server" Text="Contact Person"></asp:Label>
                            <asp:TextBox ID="txtCompanyContactPerson" runat="server" CssClass="form-control"
                                placeholder="Contact Person" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            13.
                            <asp:Label for="txtCompanyContactEmail" runat="server" Text="Contact Email"></asp:Label>
                            <asp:TextBox ID="txtCompanyContactEmail" runat="server" CssClass="form-control"
                                placeholder="Contact Email" MaxLength="200"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>C. Industry and Investment Type</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            14.
                            <%--<asp:TextBox ID="txtCompanyIndustryType" runat="server" CssClass="form-control"
                                placeholder="Industry Type"></asp:TextBox>--%>
                                <asp:Label for="ddlPromotedSector" runat="server" Text="Promoted Sector"></asp:Label>
                                <asp:DropDownList ID="ddlPromotedSector" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            15.
                            <asp:Label for="txtInvestmentType" runat="server" Text="Investment Type"></asp:Label>
                            <asp:DropDownList ID="ddlInvestmentType" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>D. Land and Building Requirement</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            16.
                            <asp:Label for="txtLandSizeRequired" runat="server" Text="Land Size Required (Acres)">
                            </asp:Label>
                            <asp:TextBox ID="txtLandSizeRequired" runat="server" CssClass="form-control"
                                placeholder="Land Size Required" MaxLength="8" type="number" min="0" max="10000000"
                                step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            17.
                            <asp:Label for="txtPSFRM" runat="server" Text="Ringgit Malaysia/Per Square Feet">
                            </asp:Label>
                            <asp:TextBox ID="txtPSFRM" runat="server" CssClass="form-control" placeholder="PSF/RM"
                                MaxLength="8" type="number" min="0" max="1000" step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            18.
                            <asp:Label for="txtBuildingSize" runat="server" Text="Building Size (Square Feet)">
                            </asp:Label>
                            <asp:TextBox ID="txtBuildingSize" runat="server" CssClass="form-control"
                                placeholder="Building Size" MaxLength="8" type="number" min="0" max="10000000"
                                step="0.01"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>E. Utility Requirement</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            19.
                            <asp:Label for="txtElectricity" runat="server" Text="Electricity (MW)"></asp:Label>
                            <asp:TextBox ID="txtElectricity" runat="server" CssClass="form-control"
                                placeholder="Electricity Requirement" MaxLength="8" type="number" min="0" max="10000000"
                                step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            20.
                            <asp:Label for="txtWater" runat="server" Text="Water (MLD)"></asp:Label>
                            <asp:TextBox ID="txtWater" runat="server" CssClass="form-control"
                                placeholder="Water Requirement" MaxLength="8" type="number" min="0" max="10000000"
                                step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            21.
                            <asp:Label for="txtNaturalGas" runat="server" Text="Natural Gas (sm3/hr)"></asp:Label>
                            <asp:TextBox ID="txtNaturalGas" runat="server" CssClass="form-control"
                                placeholder="Natural Gas Requirement" MaxLength="8" type="number" min="0" max="10000000"
                                step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            22.
                            <asp:Label for="txtIndustrialGas" runat="server" Text="Industrial Gas (sm3/hr)"></asp:Label>
                            <asp:TextBox ID="txtIndustrialGas" runat="server" CssClass="form-control"
                                placeholder="Industrial Gas Requirement" MaxLength="8" type="number" min="0"
                                max="10000000" step="0.01"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>F. Total Investment, Man Power, Timeline</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            23.
                            <asp:Label for="txtProposedTotalInvestment" runat="server"
                                Text="Proposed Total Investment (RM)"></asp:Label>
                            <asp:TextBox ID="txtProposedTotalInvestment" runat="server" CssClass="form-control"
                                placeholder="Proposed Total Investment" MaxLength="12" type="number" min="0"
                                max="100000000000" step="0.01"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            24.
                            <asp:Label for="txtManPowerRequirement" runat="server" Text="Manpower Requirement">
                            </asp:Label>
                            <asp:TextBox ID="txtManPowerRequirement" runat="server" CssClass="form-control"
                                placeholder="Manpower Requirement" MaxLength="7" type="number" min="0" max="1000000"
                                step="1"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            25.
                            <asp:Label for="txtProjectTimeline" runat="server" Text="Project Timeline"></asp:Label>
                            <asp:TextBox ID="txtProjectTimeline" runat="server" CssClass="form-control"
                                placeholder="Project Timeline" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            26.
                            <asp:Label for="txtStartConstructionDate" runat="server" Text="Start Construction Date">
                            </asp:Label>
                            <asp:TextBox ID="txtStartConstructionDate" runat="server" CssClass="form-control"
                                type="date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            27.
                            <asp:Label for="txtStartOperationDate" runat="server" Text="Start Operation Date">
                            </asp:Label>
                            <asp:TextBox ID="txtStartOperationDate" runat="server" CssClass="form-control" type="date">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            28.
                            <asp:Label for="txtCompanyDescription" runat="server" Text="Company Description">
                            </asp:Label>
                            <asp:TextBox ID="txtCompanyDescription" runat="server" CssClass="form-control"
                                placeholder="Company Description" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <b>G. Investor's Status</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            29.
                            <asp:Label for="ddlInvestorStatus" runat="server" Text="Investors Status"></asp:Label>
                            <%-- <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"
                                placeholder="Proposed Total Investment" MaxLength="8" type="number" min="0"
                                max="10000000" step="0.01"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlInvestorStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="Preliminary Enquiry">1. Preliminary Enquiry</asp:ListItem>
                                    <asp:ListItem Value="Due Dilligence">2. Due Dilligence</asp:ListItem>
                                    <asp:ListItem Value="RFI">3. RFI</asp:ListItem>
                                    <asp:ListItem Value="Site Visit">4. Site Visit</asp:ListItem>
                                    <asp:ListItem Value="RFP">5. RFP</asp:ListItem>
                                    <asp:ListItem Value="Decision">6. Decision</asp:ListItem>
                                    <asp:ListItem Value="Letter Of Intent">7. Letter Of Intent</asp:ListItem>
                                    <asp:ListItem Value="Lease Agreement">8. Lease Agreement</asp:ListItem>
                                    <asp:ListItem Value="Collection of First Payment">9. Collection of First Payment
                                    </asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <b>H. Leads Status</b>
                        <hr class="rounded" />
                        <div class="form-group">
                            30.
                            <asp:Label for="ddlLeadsStatus" runat="server" Text="Leads Status"></asp:Label>
                            <asp:DropDownList ID="ddlLeadsStatus" runat="server" ClientIDMode="Static"
                                CssClass="form-control" onchange="updateStatusImage()">
                                <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                <asp:ListItem Value="Cold">Cold</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <img id="statusImage" width="200" height="300" src="cold.png" alt="" />
                        </div>
                    </div>
                    <br />
                    <div class="col-sm-6">

                        <br />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnSubmit" runat="server" Text="Save"
                            OnClientClick="return validateCompanyName();" OnClick="BtnSubmit_Click"
                            CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                            OnClick="BtnCancel_Click" />
                        <br />
                        Please Click Save, to insert new Investors into Database.
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
                                        <asp:TextBox ID="txtRFPDateStart" CssClass="form-control" runat="server"
                                            placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="rfpDate">RFP Date End:</label>
                                        <asp:TextBox ID="txtRFPDateEnd" CssClass="form-control" runat="server"
                                            placeholder="dd/mm/yyyy" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtRFPDescription">RFP Comments:</label>
                                        <asp:TextBox ID="txtRFPDescription" CssClass="form-control" runat="server"
                                            TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:Button ID="btnSaveRfpDetails" runat="server" Text="Save"
                                        CssClass="btn btn-primary" OnClick="BtnSaveRFPDetails_Click" />
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
                                        <asp:Label for="ddlDecisionStatus" runat="server" Text="Decision Status">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlDecisionStatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="On Hold">1. On Hold</asp:ListItem>
                                            <asp:ListItem Value="Extension Of Timeline">2. Extension Of Timeline
                                            </asp:ListItem>
                                            <asp:ListItem Value="Proceed to Lease Agreement">3. Proceed to Lease
                                                Agreement</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDecisionDescription">Decision Comments:</label>
                                        <asp:TextBox ID="txtDecisionDescription" CssClass="form-control" runat="server"
                                            TextMode="MultiLine" Rows="3" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary"
                                        OnClick="BtnSaveDecisionDetails_Click" />
                                </div>
                            </div>
                            <!-- End Modal content Decision-->
                        </div>
                    </div>
                    <!-- End Modal Decision -->
                    <!-- End Modal Decision -->


        </form>
        <script>
            $(document).ready(function () {
                updateStatusImage();
            });
        </script>
        <hr />
        <div class="col-sm-12">
            <p>&copy; <%: DateTime.Now.Year %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 6 March 2023</p>
        </div>
    </body>

    </html>