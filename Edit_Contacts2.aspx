<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Contacts2.aspx.cs" Inherits="CRM.Edit_Contacts2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Contact</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
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
                    <a class="navbar-brand" href="#">CRM v1.0</a>
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
        <%--END NAV--%>
        <div class="container">
            <div class="col-sm-8">
                <h2>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit-icon-vector-illustration.jpg" Width="50" />&nbsp;
                    Edit Contact</h2>

                <div class="form-group row">
                    <asp:Label for="txtCompanyId" runat="server" Text="1. Company ID"></asp:Label>
                    <asp:TextBox ID="txtCompanyId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="industrialPark_ddl" runat="server" Text="0. Industrial Park"></asp:Label>
                    <asp:DropDownList ID="industrialPark_ddl" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtCompanyName" runat="server" Text="2. Company Name *"></asp:Label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name - Must fill in." MaxLength="200"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtCompanyPhone" runat="server" Text="3. Company Phone"></asp:Label>
                    <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control" placeholder="Company Name - Must fill in." MaxLength="200"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtName" runat="server" Text="4. Contact Person Name"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name Of Contact." MaxLength="100"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtPosition" runat="server" Text="5. Position"></asp:Label>
                    <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" placeholder="Position Of Contact" MaxLength="100"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtMobile" runat="server" Text="6. Mobile"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="" MaxLength="30"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <asp:Label for="txtNotes" runat="server" Text="7. Notes"></asp:Label>
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="" MaxLength="300"></asp:TextBox>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Update Contact Details" OnClick="BtnSave_Click" /><hr />
                <!-- UPLOAD DOWNLOAD SECTION START -->
                <div class="form-group row">
                    <asp:Label for="DocumentUpload1" runat="server" Text="8. Attach Documents"></asp:Label>
                    <asp:FileUpload runat="server" ID="DocumentUpload1" AllowMultiple="true" />
                    <asp:Button runat="server" ID="btnUploadDocument" Text="Upload" OnClick="BtnUploadDocument_Click" />
                    <asp:Label ID="lblMessage2" runat="server" Text="."></asp:Label>
                </div>
                <div>
                    <div class="col-sm-12">
                        <h1>ATTACHED DOCUMENTS</h1>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" DataKeyNames="Document_Id"
                                    OnSorting="GridView1_Sorting" OnRowDeleting="GridView1_RowDeleting" CssClass="table table-hover table-striped" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" CellSpacing="4">
                                    <Columns>
                                        <asp:BoundField DataField="Document_Id" HeaderText="ID" SortExpression="Document_Id" />
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <a href='<%# Eval("Document_Id", "DownloadFiles.aspx?Document_Id={0}") %>'><%# Eval("FileName") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileSize" HeaderText="File Size" SortExpression="FileSize" />
                                        <asp:BoundField DataField="ContentType" HeaderText="Content Type" SortExpression="ContentType" />
                                        <asp:TemplateField HeaderText="Contact ID">
                                            <ItemTemplate>
                                                <%# Eval("Contact_Id") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                </div>
                <br />
                <div class="form-group">
                    <asp:Label for="PhotoUpload1" runat="server" Text="9. Attach Photos"></asp:Label>
                    <asp:FileUpload runat="server" ID="PhotoUpload1" AllowMultiple="true" onchange="previewPhotos()" />
                    <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="BtnUpload_Click" />
                    <asp:Label ID="lblMessage" runat="server" Text="."></asp:Label>
                    <div>
                        <div id="previewContainer"></div>
                        <div id="sizeContainer"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <h1>ATTACHED PHOTOS</h1>
            <div id="imageContainer" runat="server"></div>
        </div>
        <asp:Repeater ID="repeaterPhotos" runat="server">
            <ItemTemplate>
                <div class="photo-container">
                    <h2><%# Eval("FileName") %></h2>
                    <img src='<%# "data:" + Eval("ContentType") + ";base64," + Convert.ToBase64String((byte[])Eval("FileData")) %>' alt="Uploaded Photo" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        <!-- UPLOAD DOWNLOAD SECTION END-->
    </form>
    <hr />
    <div class="col-sm-12">
        <p>&copy; <%: DateTime.Now.Year %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 22 August 2023</p>
    </div>
</body>
</html>
