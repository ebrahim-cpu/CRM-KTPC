<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="company_contacts.aspx.cs" Inherits="CRM.company_contacts" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Contacts</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous" />
    <style>
        .contact-card {
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
            box-shadow: 2px 2px 5px rgba(0,0,0,0.1);
            background-color: #fcfcfc;
        }
        .contact-card h4 {
            margin-top: 0;
            color: #337ab7;
            font-weight: bold;
        }
        .contact-card p {
            margin-bottom: 5px;
        }
        .contact-icon {
            margin-right: 5px;
            color: #555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 30px;">
            <h2>Company Contacts <asp:Label ID="lblCompanyName" runat="server" ForeColor="#555555"></asp:Label>
                <div class="pull-right">
                    <a href="Dashboard.aspx" class="btn btn-default btn-sm">
                        <i class="glyphicon glyphicon-home"></i> Home
                    </a>
                    <asp:HyperLink ID="lnkAddContact" runat="server" CssClass="btn btn-primary btn-sm">
                        <i class="fas fa-user-plus"></i> Add Contact
                    </asp:HyperLink>
                </div>
            </h2>
            <hr />
            <div class="row">
                <asp:Repeater ID="rptContacts" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-6">
                            <div class="contact-card">
                                <h4><i class="glyphicon glyphicon-user"></i> <%# Eval("Name") %>
                                    <a href='Edit_Contacts2.aspx?Id=<%# Eval("Id") %>' class="pull-right" title="Edit Contact">
                                        <i class="fas fa-edit"></i>
                                    </a>
                                </h4>
                                <p><i class="glyphicon glyphicon-briefcase contact-icon"></i> <strong>Position:</strong> <%# Eval("Position") %></p>
                                <p><i class="glyphicon glyphicon-phone contact-icon"></i> <strong>Mobile:</strong> <%# Eval("Mobile") %></p>
                                <p><i class="glyphicon glyphicon-info-sign contact-icon"></i> <strong>Notes:</strong> <%# Eval("Notes") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </form>
</body>
</html>
