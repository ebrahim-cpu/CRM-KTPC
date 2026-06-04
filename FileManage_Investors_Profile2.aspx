<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManage_Investors_Profile2.aspx.cs" Inherits="CRM.FileManage_Investors_Profile2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Manager</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />

    <style>
        /* Set the desired width for the column */
        .table th:nth-child(5), .table td:nth-child(5) {
            width: 100px;
        }
    </style>
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                </div>
            </div>
        </nav>
        <%--END NAV--%>
        <div class="col-sm-12">
            <table cellpadding="5">
                <tr>
                    <td colspan="3">
                        <h3><b>INVESTOR PROFILE</b></h3>
                    </td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>A. Investors Profile</b>
                    </td>
                </tr>
                <tr>
                    <td>1. </td>
                    <td><b>&nbsp;Company Name:</b></td>
                    <td>
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>2. </td>
                    <td><b>&nbsp;Address:</b></td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>3. </td>
                    <td><b>&nbsp;Country:</b></td>
                    <td>
                        <asp:Label ID="lblCountry" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>4. </td>
                    <td><b>&nbsp;Phone:</b></td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>5. </td>
                    <td><b>&nbsp;Website URL:</b></td>
                    <td>
                        <asp:Label ID="lblWebsiteURL" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>6. </td>
                    <td><b>&nbsp;Year Founded:</b></td>
                    <td>
                        <asp:Label ID="lblYearFounded" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>7. </td>
                    <td><b>&nbsp;Headquaters:</b></td>
                    <td>
                        <asp:Label ID="lblHeadquaters" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>8. </td>
                    <td><b>&nbsp;Branches:</b></td>
                    <td>
                        <asp:Label ID="lblBranches" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label1" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>B. Contact Person</b></td>
                </tr>
                <tr>
                    <td>9. </td>
                    <td><b>&nbsp;Contact Person:</b></td>
                    <td>
                        <asp:Label ID="lblContactPerson" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>10. </td>
                    <td><b>&nbsp;Contact Email:</b></td>
                    <td>
                        <asp:Label ID="lblContactEmail" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label2" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>C. Industry and Investment Type</b></td>
                </tr>
                <tr>
                    <td>11. </td>
                    <td><b>&nbsp;Promoted Sector:</b></td>
                    <td>
                        <asp:Label ID="lblIndustryType" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>12. </td>
                    <td><b>&nbsp;Investment Type:</b></td>
                    <td>
                        <asp:Label ID="lblInvestmentType" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label3" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>D. Land and Building Requirement</b></td>
                </tr>
                <tr>
                    <td>13. </td>
                    <td><b>&nbsp;Land Size Required (sf):</b></td>
                    <td>
                        <asp:Label ID="lblLandSizeRequired" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>14. </td>
                    <td><b>&nbsp;PSF (RM):</b></td>
                    <td>
                        <asp:Label ID="lblPSFRM" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>15. </td>
                    <td><b>&nbsp;Building Size (sf):</b></td>
                    <td>
                        <asp:Label ID="lblBuildingSize" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label4" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>E. Utility Requirement</b></td>
                </tr>

                <tr>
                    <td>16. </td>
                    <td><b>&nbsp;Electricity (kWh):</b></td>
                    <td>
                        <asp:Label ID="lblElectricity" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>17. </td>
                    <td><b>&nbsp;Water (m3):</b></td>
                    <td>
                        <asp:Label ID="lblWater" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>18. </td>
                    <td><b>&nbsp;Natural Gas (Nm3/hr):</b></td>
                    <td>
                        <asp:Label ID="lblNaturalGas" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>19. </td>
                    <td><b>&nbsp;Industrial Gas (Nm3/hr):</b></td>
                    <td>
                        <asp:Label ID="lblIndustrialGas" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label5" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">
                    <td colspan="3"><b>F. Total Investment, Man Power, Timeline</b></td>
                </tr>

                <tr>
                    <td>20. </td>
                    <td><b>&nbsp;Proposed Total Investment (RM):</b></td>
                    <td>
                        <asp:Label ID="lblProposedTotalInvestment" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>21. </td>
                    <td><b>&nbsp;Manpower Requirement:</b></td>
                    <td>
                        <asp:Label ID="lblManpowerRequirement" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>22. </td>
                    <td><b>&nbsp;Project Timeline:</b></td>
                    <td>
                        <asp:Label ID="lblProjectTimeline" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>23. </td>
                    <td><b>&nbsp;Start Construction Date:</b></td>
                    <td>
                        <asp:Label ID="lblStartConstruction" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>24. </td>
                    <td><b>&nbsp;Project Operation Date:</b></td>
                    <td>
                        <asp:Label ID="lblStartOperation" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>25. </td>
                    <td><b>&nbsp;Company Description:</b></td>
                    <td>
                        <asp:Label ID="lblCompanyDescription" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><b>&nbsp;</b></td>
                    <td>
                        <asp:Label ID="Label6" runat="server"></asp:Label></td>
                </tr>
                <tr class="grey-row">

                    <td colspan="3"><b>G. Investors Status</b></td>
                </tr>

                <tr>
                    <td>26. </td>
                    <td><b>&nbsp;Investors Status:</b></td>
                    <td>
                        <asp:Label ID="lblInvestorsStatus" runat="server"></asp:Label></td>
                </tr>
            </table>
            <div class="col-sm-11">
                <h3><b>INVESTOR CONTACTS</b></h3>
                <asp:GridView ID="gvContacts" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Position" HeaderText="Position" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" />
                        <%--<asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" BorderStyle="None" />
                </asp:GridView>
            </div>
            <div class="col-sm-11">
                <h3><b>INVESTOR ACTIVITIES</b></h3>
                <asp:GridView ID="gvActivities" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                        <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                        <asp:BoundField DataField="Activity_Date_Start" HeaderText="Date" DataFormatString="{0: dd/MMM/yy}" SortExpression="Activity_Date_Start" />
                        <asp:BoundField DataField="Company_Status" HeaderText="Status" SortExpression="Company_Status" />
                        <asp:BoundField DataField="Activity_Description" HeaderText="Description" />
                        <%--<asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" BorderStyle="None" />
                </asp:GridView>
            </div>
        </div>
        <div class="col-sm-12">
            <br />
            <div class="alert alert-success">
                <p>
                    This is the Investor's profile files upload section. Recommended uploads are documents such as brochures, company profiles, any documents which are related to this investor record. However file uploads are limited to 50MB per files. Be sure to check the file size before uploading.
                </p>
            </div>
            <div class="container-fluid">
                <div class="form-group row">
                    <h1>ATTACHED DOCUMENTS
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/document.png" Width="50px" />
                    </h1>
                    <asp:Label for="DocumentUpload1" runat="server" Text="28. Attach Documents."></asp:Label>
                    <asp:FileUpload runat="server" ID="DocumentUpload1" AllowMultiple="true" />
                    <asp:Button runat="server" ID="btnUploadDocument" Text="Upload" OnClick="BtnUploadDocument_Click" />
                    <asp:Label ID="lblMessage2" runat="server" Text="."></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" DataKeyNames="Document_Id"
                            OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" CssClass="table table-hover table-striped" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" CellSpacing="4">
                            <Columns>
                                <asp:BoundField DataField="Document_Id" HeaderText="ID" SortExpression="Document_Id" />
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <a href='<%# Eval("Document_Id", "DownloadFiles_Profiles.aspx?Document_Id={0}") %>'><%# Eval("FileName") %></a>
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
            <div class="col-sm-12">
                <div class="alert alert-success">
                    <p>
                        You can upload images in the format of .jpg , .png , .gif. Max size of images should be less than 50MB. 
                        You can use the camera from your handphone to direct upload. 
                        But you have to manually reduce the size by changing the camera settings to (HD quality) 1728x2304 or less.
                        Advisable you upload the images to WhatsApp, and upload the image to this System. Whatsapp have the more
                        Advanced functions to resized photos without losing the original quality of the image.
                    </p>
                </div>
                <div class="form-group">
                    <h1>ATTACHED PHOTOS
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/foto.png" Width="60px" />
                    </h1>
                    <asp:Label for="PhotoUpload1" runat="server" Text="29. Attach Photos."></asp:Label>
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
                                        PostBackUrl='<%# "ConfirmDelete_ProfilePhoto.aspx?Photo_Id=" + Eval("Document_Id") %>' />
                                </asp:Panel>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" SelectCommand="SELECT [Id], [Company_Name], [Company_Country], [Company_Phone], [Company_Contact_Email], [Land_Size_Required], [PSF_RM] FROM [Investors_Profile] WHERE ([Id] = @Id)">
            <SelectParameters>
                <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="container-fluid">
            <div class="col-sm-12">
                <div class="alert alert-success">
                    <p>
                        Research Links. You can attach unlimited links to this company profile record.
                        URL Links such as to websites eg. Yahoo Finance related to that particular company,
                        URL links to Sharepoint documents, URL links to news or anything that have
                        http:// or https:// in front.
                        Also can consider this as a bookmark. If have errors or you need to put in a longer URL,
                        update us, as soon as possible. Thank You.
                    </p>
                </div>
            <h1>RESEARCH LINKS
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/foto.png" Width="60px" />
            </h1>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group row">
                        <label for="txt1" class="col-sm-2 col-form-label">1.BookMark Text</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt1" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt2" class="col-sm-2 col-form-label">2.BookMark URL</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt2" runat="server" class="form-control" placeholder="https://"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt3" class="col-sm-2 col-form-label">3.BookMark Description</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt3" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txt4" class="col-sm-2 col-form-label">4.BookMark Keyword</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt4" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">                        
                            <div class="col-sm-6">
                                <asp:Button ID="BtnSimpan" runat="server" Text="Save" class="btn btn-success" OnClick="BtnSimpan_Click" />
                            </div>                        
                    </div>
                </div>
            </div>
            <!-- START BOOKMARK -->
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="Bookmark_Id"
                            OnRowDataBound="GridView2_RowDataBound" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                            OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnRowDeleting="GridView2_RowDeleting"
                            AllowSorting="true" OnSorting="GridView2_Sorting" CssClass="table table-condensed table-hover">
                            <Columns>
                                <asp:BoundField DataField="Bookmark_Id" HeaderText=" ID " SortExpression="Bookmark_Id" />
                                <asp:TemplateField HeaderText="Clickable Links" SortExpression="Bookmark_Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookmark_Id" runat="server" Text='<%# Bind("Bookmark_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Displayed Text" SortExpression="BookMark_Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblText" runat="server" Text='<%# Bind("BookMark_Text") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBookMark_Text" runat="server" Text='<%# Bind("BookMark_Text") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="URL" SortExpression="BookMark_URL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookMark_URL" runat="server" Text='<%# Bind("BookMark_URL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBookMark_URL" runat="server" TextMode="MultiLine" Rows="2" Text='<%# Bind("BookMark_URL") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description " SortExpression="BookMark_Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookMark_Description" runat="server" Text='<%# Bind("BookMark_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBookMark_Description" runat="server" TextMode="MultiLine" Rows="5" Text='<%# Bind("BookMark_Description") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Keyword" SortExpression="BookMark_Keyword">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookMark_Keyword" runat="server" Text='<%# Bind("BookMark_Keyword") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBookMark_Keyword" runat="server" Text='<%# Bind("BookMark_Keyword") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <!-- Edit button with pencil icon -->
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="edit-button"><i class="fa fa-pen"></i> </asp:LinkButton>
                                        <!-- Delete button with trash icon -->
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="update-button"><i class="fa fa-check"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="cancel-button"><i class="fa fa-times"></i>
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- END BOOKMARK -->
    </form>
    <script src="lightbox/lightbox-plus-jquery.js"></script>
    <!-- Lightbox ada issue dengan bootstrap popup modal -->
    <script>
        $(document).ready(function () {
            $("#statusSelect").on("change", function () {
                var selectedStatus = $(this).val();
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
                $("#hfLeadsStatus").val(selectedStatus);
            });
        });

        function openModal() {
            $('#myModalLeadsStatus').modal('show');
        }
    </script>
    <hr />
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.ToString("dd/MMM/yyyy") %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 28 September 2023</p>
    </div>
</body>
</html>

