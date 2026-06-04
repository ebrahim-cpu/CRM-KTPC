<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Investors_Activities2.aspx.cs" Inherits="CRM.Edit_Investors_Activities2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Investors Activities</title>
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

        function updateTextboxDecisionStatus() {
            var dropdown = document.getElementById("<%= ddlDecisionStatus.ClientID %>");
            var selectedValue = dropdown.value;
        }
    </script>
    <script type="text/javascript">
        function previewPhotos() {
            var previewContainer = document.getElementById('previewContainer');
            var sizeContainer = document.getElementById('sizeContainer');
            previewContainer.innerHTML = ''; // Clear previous previews
            sizeContainer.innerHTML = ''; // Clear previous size display
            var files = document.getElementById('PhotoUpload1').files;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (file.type.match('image.*')) {
                    var reader = new FileReader();
                    reader.onload = (function (currentFile) {
                        return function (event) {
                            var img = document.createElement('img');
                            img.src = event.target.result;
                            img.style.maxWidth = '100px'; // Adjust the preview image size as needed
                            previewContainer.appendChild(img);
                            // Display file size
                            var sizeDiv = document.createElement('div');
                            sizeDiv.textContent = 'Size: ' + formatBytes(currentFile.size); // Format size in bytes
                            sizeContainer.appendChild(sizeDiv);
                        };
                    })(file);
                    reader.readAsDataURL(file);
                }
            }
        }
        // Function to format file size in bytes to a more readable format
        function formatBytes(bytes) {
            if (bytes === 0) return '0 Bytes';

            var k = 1024;
            var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
            var i = Math.floor(Math.log(bytes) / Math.log(k));

            return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
        }
    </script>
    <style>
        .photo-container {
            display: inline-block;
            width: 150px;
            margin: 10px;
            text-align: center;
        }

        .photo-img {
            max-width: 100%;
            height: auto;
        }
    </style>
    <link href="lightbox/lightbox.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <%--START NAV--%>
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">CRM v1.0</a>
                </div>
                <ul class="nav navbar-nav">
                    
                            <li><a href="Dashboard.aspx">Dashboard</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">2. Investor List I</a></li>
                            <li><a href="List_Contacts.aspx">3. Contact List</a></li>
                            <%--                            <li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>--%>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Activities
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Activities.aspx">1. Add New Activity</a></li>
                            <li><a href="List_Investors_Activities2.aspx">2. Investor's Activity List</a></li>
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
            </div>
        </nav>
        <%--END NAV--%>
        <div class="container">
            <h2>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-lg"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                Edit Investors Activities</h2>
            <hr class="rounded" />
            <asp:HiddenField ID="hdnID" runat="server" />
            <asp:HiddenField ID="hdnCompanyId" runat="server" />
            <div class="form-group">
                <label for="txtCompanyName">1. Company Name:</label>
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <%--            <div class="form-group">
                <label for="txtActivityType">Activity Type:</label>
                <asp:TextBox ID="txtActivityType" runat="server" CssClass="form-control" />
            </div>--%>
            <div class="form-group">
                <label for="txtActivityDateStart">2. Activity Date:</label>
                <asp:TextBox ID="txtActivityDateStart" runat="server" CssClass="form-control" placeholder="dd/mm/yyyy" TextMode="Date" />
            </div>
            <div class="form-group">
                <asp:Label for="ddlInvestorStatus" runat="server" Text="3. Investors Status"></asp:Label>
                <%-- <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Proposed Total Investment" MaxLength="8" type="number" min="0" max="10000000" step="0.01"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlInvestorStatus" CssClass="form-control" runat="server" AutoPostBack="False">
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
                <label for="txtActivityDescription">4. Activity Description:</label>
                <asp:TextBox ID="txtActivityDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="if (!confirm('Are you sure you want to save this activity?')) return false;" CausesValidation="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
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
                        <asp:Button ID="setDateButton" runat="server" Text="Set Today's Date" CssClass="btn btn-success" OnClick="setDateButton_Click" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSaveRfpDetails" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" CausesValidation="false" />
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
                            <asp:DropDownList ID="ddlDecisionStatus" CssClass="form-control" runat="server" onchange="updateTextboxDecisionStatus()">
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
                        <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" CausesValidation="false" />
                    </div>
                </div>
                <!-- End Modal content Decision-->
            </div>
        </div>
        <!-- End Modal Decision -->
        <div class="col-sm-12">
            <br />
            <div class="alert alert-warning">
                <p>
                    This is the Investor's activities files upload section. Recommended uploads are documents such minutes meeting, any documents which are related to this investor's activities. However file uploads are limited to 50MB per files. Be sure to check the file size before uploading.
                </p>
            </div>
            <div class="container">
                <div class="form-group row">
                    <asp:Label for="DocumentUpload1" runat="server" Text="5. Attach Documents"></asp:Label>
                    <asp:FileUpload runat="server" ID="DocumentUpload1" AllowMultiple="true" />
                    <asp:Button runat="server" ID="btnUploadDocument" Text="Upload" OnClick="BtnUploadDocument_Click" />
                    <asp:Label ID="lblMessage2" runat="server" Text="."></asp:Label>
                </div>
                <h1>ATTACHED DOCUMENTS</h1>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" DataKeyNames="Document_Id"
                            OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" CssClass="table table-hover table-striped" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" CellSpacing="4">
                            <Columns>
                                <asp:BoundField DataField="Document_Id" HeaderText="ID" SortExpression="Document_Id" />
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <a href='<%# Eval("Document_Id", "DownloadFiles_Activity.aspx?Document_Id={0}") %>'><%# Eval("FileName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FileSize" HeaderText="File Size" SortExpression="FileSize" />
                                <asp:BoundField DataField="ContentType" HeaderText="Content Type" SortExpression="ContentType" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete"
                                            CommandArgument='<%# Eval("Document_Id") %>' OnClientClick="return confirm('Are you sure you want to delete this file?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div class="container">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:Label for="PhotoUpload1" runat="server" Text="6. Attach Photos"></asp:Label>
                        <asp:FileUpload runat="server" ID="PhotoUpload1" AllowMultiple="true" onchange="previewPhotos()" />
                        <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="BtnUpload_Click" />
                        <asp:Label ID="lblMessage" runat="server" Text="."></asp:Label>
                        <div>
                            <div id="previewContainer"></div>
                            <div id="sizeContainer"></div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <h1>ATTACHED PHOTOS</h1>
                    <div id="imageContainer" runat="server"></div>
                </div>
                <div class="photo-grid">
                    <asp:Repeater ID="repeaterPhotos" runat="server">
                        <ItemTemplate>
                            <div class="photo-container">
                                <a href='<%# "data:" + Eval("ContentType") + ";base64," + Convert.ToBase64String((byte[])Eval("FileData")) %>' data-lightbox="photos">
                                    <img class="photo-img" src='<%# "data:" + Eval("ContentType") + ";base64," + Convert.ToBase64String((byte[])Eval("FileData")) %>' alt="Uploaded Photo" />
                                </a>
                                <div class="delete-button-container">
                                    <asp:Panel ID="buttonPanel" runat="server">
                                        <asp:Button runat="server" ID="deleteButton" Text="Delete"
                                            PostBackUrl='<%# "ConfirmDelete_ActivityPhoto.aspx?Photo_Id=" + Eval("Document_Id") %>' />
                                    </asp:Panel>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </form>
    <hr />
    <div class="container">
        <p>&copy; <%: DateTime.Now.Year %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 16 March 2023</p>
    </div>
</body>
</html>
