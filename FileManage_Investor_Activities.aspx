<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManage_Investor_Activities.aspx.cs" Inherits="CRM.FileManage_Investor_Activities" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Manager</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
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
                    <a class="navbar-brand" href="Home.aspx">CRM v1.0</a>
                </div>
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Home</a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">Profiles
                            <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="New_Investors_Profile.aspx">1. New Investor</a></li>
                            <li><a href="List_Investors_Profile.aspx">2. Investor List I</a></li>
                            <%--<li><a href="List_Investors_Profile_2.aspx">3. Investor List II</a></li>--%>
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
                </div>
            </div>
        </nav>
        <%--END NAV--%>
        <div class="col-sm-12">
            <br />
            <h2>Investor's Activities</h2>
            <asp:GridView ID="GridView3" runat="server" CssClass="table table-condensed table-hover" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Company_Name" HeaderText="Company_Name" SortExpression="Company_Name" />
                    <asp:BoundField DataField="Activity_Type" HeaderText="Activity_Type" SortExpression="Activity_Type" />
                    <asp:BoundField DataField="Activity_Date_Start" HeaderText="Activity_Date_Start" DataFormatString="{0:dd/MM/yyyy}" SortExpression="Activity_Date_Start" />
                    <asp:BoundField DataField="Activity_Date_End" HeaderText="Activity_Date_End" DataFormatString="{0:dd/MM/yyyy}" SortExpression="Activity_Date_End" />
                    <asp:BoundField DataField="Activity_Description" HeaderText="Activity_Description" SortExpression="Activity_Description" />
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
            <hr />
            <div class="col=sm-12">
                <p>
                    This is the Investor's activities files upload section. Recommended uploads are documents such as minutes meeting, emails, incoming or outgoing correspondences, site visit photos, notes, any documents which are related to this particular activity record. However file uploads are limited to 5MB per files. Be sure to check the file size before uploading.
                </p>
            </div>
            <br />
            <hr />
        </div>
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" SelectCommand="SELECT [Id], [Company_Name], [Activity_Type], [Activity_Date_Start], [Activity_Date_End], [Activity_Description] FROM [Investors_Activities] WHERE ([Id] = @Id)">
            <SelectParameters>
                <asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
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
                    <asp:Button runat="server" ID="Button1" Text="Upload" OnClick="BtnUpload_Click" />
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
    </form>
    <script src="lightbox/lightbox-plus-jquery.js"></script>
    <hr class="solid" />
    <div class="col-sm-6">
        <p>&copy; <%: DateTime.Now.Year %> - KTPC Sdn. Bhd. CRM v1.0 | Last Update: 28 September 2023</p>
    </div>
</body>
</html>